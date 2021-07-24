using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using GamesApi.Common.Enums;
using GamesApi.Data;
using GamesApi.Data.Entities;
using GamesApi.DataProviders.Abstractions;
using GamesApi.Models;
using GamesApi.Models.Responses;
using GamesApi.Services;
using GamesApi.Services.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GamesApi.UnitTests.Services
{
    public class GameServiceTest
    {
        private readonly IGameService _gameService;

        private readonly Mock<IGameProvider> _gameProvider;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<GamesDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<GameService>> _logger;

        private readonly PagingDataResult _pagingDataResultsSuccess = new PagingDataResult()
        {
            Games = new List<GameEntity>()
            {
                new GameEntity()
                {
                    Name = "TestName"
                }
            },
            TotalRecords = 1
        };
        private readonly PagingDataResult _pagingDataResultsFailed = new PagingDataResult()
        {
        };
        private readonly GetByPageResponse _getByPageResponseSuccess = new GetByPageResponse()
        {
            Games = new List<GameModel>()
            {
                new GameModel()
                {
                    Name = "TestName"
                }
            },
            TotalRecords = 1
        };
        private readonly GetByPageResponse _getByPageResponseFailed = new GetByPageResponse()
        {
            Games = null,
            TotalRecords = 1
        };
        private readonly GameEntity _gameEntitySuccess = new GameEntity()
        {
            Name = "TestName"
        };
        private readonly GameModel _gameModelSuccess = new GameModel()
        {
            Name = "TestName"
        };
        private readonly GameEntity _gameEntityFailed = new GameEntity()
        {
        };
        private readonly GameModel _gameModelFailed = new GameModel()
        {
        };
        private readonly string _testIdSuccess = "testIdSuccess";
        private readonly string _testIdFailed = "testIdFailed";
        private readonly string _testNameSuccess = "testNameSuccess";
        private readonly string _testNameFailed = "empty";
        private readonly string _testDeveloperSuccess = "testDeveloperSuccess";
        private readonly string _testDeveloperFailed = "empty";
        private readonly string _testPublisherSuccess = "testPublisherSuccess";
        private readonly string _testPublisherFailed = "empty";
        private readonly string _testGenreSuccess = "testGenreSuccess";
        private readonly string _testGenreFailed = "empty";
        private readonly DateTime _testDateSuccess = DateTime.Now;
        private readonly DateTime _testDateFailed = DateTime.MinValue;
        private readonly decimal _testPriceSuccess = 10.0M;
        private readonly decimal _testPriceFailed = 0.0M;

        public GameServiceTest()
        {
            _gameProvider = new Mock<IGameProvider>();
            _mapper = new Mock<IMapper>();
            _dbContextWrapper = new Mock<IDbContextWrapper<GamesDbContext>>();
            _logger = new Mock<ILogger<GameService>>();

            _gameProvider.Setup(s => s.GetByPageAsync(
                It.Is<int>(i => i == 1),
                It.Is<int>(i => i == 10),
                It.IsAny<SortedTypeEnum>())).ReturnsAsync(_pagingDataResultsSuccess);

            _gameProvider.Setup(s => s.GetByPageAsync(
                It.Is<int>(i => i == 1000),
                It.Is<int>(i => i == 10000),
                It.IsAny<SortedTypeEnum>())).ReturnsAsync(_pagingDataResultsFailed);

            _gameProvider.Setup(s => s.GetByIdAsync(
                It.Is<string>(i => i.Contains(_testIdSuccess)))).ReturnsAsync(_gameEntitySuccess);

            _gameProvider.Setup(s => s.GetByIdAsync(
                It.Is<string>(i => i.Contains(_testIdFailed)))).ReturnsAsync(_gameEntityFailed);

            _gameProvider.Setup(s => s.AddAsync(
                It.Is<string>(i => i.Contains(_testNameSuccess)),
                It.Is<string>(i => i.Contains(_testDeveloperSuccess)),
                It.Is<string>(i => i.Contains(_testPublisherSuccess)),
                It.Is<string>(i => i.Contains(_testGenreSuccess)),
                It.Is<DateTime>(i => i.Equals(_testDateSuccess)),
                It.Is<decimal>(i => i.Equals(_testPriceSuccess)))).ReturnsAsync(_gameEntitySuccess);

            _gameProvider.Setup(s => s.AddAsync(
                It.Is<string>(i => i.Contains(_testNameFailed)),
                It.Is<string>(i => i.Contains(_testDeveloperFailed)),
                It.Is<string>(i => i.Contains(_testPublisherFailed)),
                It.Is<string>(i => i.Contains(_testGenreFailed)),
                It.Is<DateTime>(i => i.Equals(_testDateFailed)),
                It.Is<decimal>(i => i.Equals(_testPriceFailed)))).ReturnsAsync(_gameEntityFailed);

            _gameProvider.Setup(s => s.DeleteAsync(
                It.Is<string>(i => i.Contains(_testIdSuccess)))).ReturnsAsync(true);

            _gameProvider.Setup(s => s.DeleteAsync(
                It.Is<string>(i => i.Contains(_testIdFailed)))).ReturnsAsync(false);

            _gameProvider.Setup(s => s.UpdateNameAsync(
                It.Is<string>(i => i.Contains(_testIdSuccess)),
                It.Is<string>(i => i.Contains(_testNameSuccess)))).ReturnsAsync(true);

            _gameProvider.Setup(s => s.UpdateNameAsync(
                It.Is<string>(i => i.Contains(_testIdFailed)),
                It.Is<string>(i => i.Contains(_testNameFailed)))).ReturnsAsync(false);

            _gameProvider.Setup(s => s.UpdateDeveloperAsync(
                It.Is<string>(i => i.Contains(_testIdSuccess)),
                It.Is<string>(i => i.Contains(_testDeveloperSuccess)))).ReturnsAsync(true);

            _gameProvider.Setup(s => s.UpdateDeveloperAsync(
                It.Is<string>(i => i.Contains(_testIdFailed)),
                It.Is<string>(i => i.Contains(_testDeveloperFailed)))).ReturnsAsync(false);

            _gameProvider.Setup(s => s.UpdatePublisherAsync(
                It.Is<string>(i => i.Contains(_testIdSuccess)),
                It.Is<string>(i => i.Contains(_testPublisherSuccess)))).ReturnsAsync(true);

            _gameProvider.Setup(s => s.UpdatePublisherAsync(
                It.Is<string>(i => i.Contains(_testIdFailed)),
                It.Is<string>(i => i.Contains(_testPublisherFailed)))).ReturnsAsync(false);

            _gameProvider.Setup(s => s.UpdateGenreAsync(
                It.Is<string>(i => i.Contains(_testIdSuccess)),
                It.Is<string>(i => i.Contains(_testGenreSuccess)))).ReturnsAsync(true);

            _gameProvider.Setup(s => s.UpdateGenreAsync(
                It.Is<string>(i => i.Contains(_testIdFailed)),
                It.Is<string>(i => i.Contains(_testGenreFailed)))).ReturnsAsync(false);

            _gameProvider.Setup(s => s.UpdateReleaseDateAsync(
                It.Is<string>(i => i.Contains(_testIdSuccess)),
                It.Is<DateTime>(i => i.Equals(_testDateSuccess)))).ReturnsAsync(true);

            _gameProvider.Setup(s => s.UpdateReleaseDateAsync(
                It.Is<string>(i => i.Contains(_testIdFailed)),
                It.Is<DateTime>(i => i.Equals(_testDateFailed)))).ReturnsAsync(false);

            _gameProvider.Setup(s => s.UpdatePriceAsync(
                It.Is<string>(i => i.Contains(_testIdSuccess)),
                It.Is<decimal>(i => i.Equals(_testPriceSuccess)))).ReturnsAsync(true);

            _gameProvider.Setup(s => s.UpdatePriceAsync(
                It.Is<string>(i => i.Contains(_testIdFailed)),
                It.Is<decimal>(i => i.Equals(_testPriceFailed)))).ReturnsAsync(false);

            _mapper.Setup(s => s.Map<GameModel>(
                It.Is<GameEntity>(i => i.Equals(_gameEntitySuccess)))).Returns(_gameModelSuccess);

            _mapper.Setup(s => s.Map<GameModel>(
                It.Is<GameEntity>(i => i.Equals(_gameEntityFailed)))).Returns(_gameModelFailed);

            _mapper.Setup(s => s.Map<GetByPageResponse>(
                It.Is<PagingDataResult>(i => i.Equals(_pagingDataResultsSuccess)))).Returns(_getByPageResponseSuccess);

            _mapper.Setup(s => s.Map<GetByPageResponse>(
                It.Is<PagingDataResult>(i => i.Equals(_pagingDataResultsFailed)))).Returns(_getByPageResponseFailed);

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransaction()).Returns(dbContextTransaction.Object);

            _gameService = new GameService(_dbContextWrapper.Object, _gameProvider.Object, _mapper.Object, _logger.Object);
        }

        [Fact]
        public async Task GetByPageAsync_Success()
        {
            // arrange
            var testPage = 1;
            var testPageSize = 10;
            var testSortedType = SortedTypeEnum.CreateDateAscending;

            // act
            var result = await _gameService.GetByPageAsync(testPage, testPageSize, testSortedType);

            // assert
            result.Should().NotBeNull();
            result.Games.Should().NotBeNull();
            result.TotalRecords.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetByPageAsync_Failed()
        {
            // arrange
            var testPage = 1000;
            var testPageSize = 10000;
            var testSortedType = SortedTypeEnum.CreateDateAscending;

            // act
            var result = await _gameService.GetByPageAsync(testPage, testPageSize, testSortedType);

            // assert
            result.Should().NotBeNull();
            result.Games.Should().BeNull();
            result.TotalRecords.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetByIdAsync_Success()
        {
            // arrange

            // act
            var result = await _gameService.GetByIdAsync(_testIdSuccess);

            // assert
            result.Should().NotBeNull();
            result.Game.Should().NotBeNull();
            result.Game.Name.Should().Be("TestName");
        }

        [Fact]
        public async Task GetByIdAsync_Failed()
        {
            // arrange

            // act
            var result = await _gameService.GetByIdAsync(_testIdFailed);

            // assert
            result.Should().NotBeNull();
            result.Game.Should().NotBeNull();
            result.Game.Name.Should().BeNull();
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange

            // act
            var result = await _gameService.AddAsync(_testNameSuccess, _testDeveloperSuccess, _testPublisherSuccess, _testGenreSuccess, _testDateSuccess, _testPriceSuccess);

            // assert
            result.Should().NotBeNull();
            result.Game.Should().NotBeNull();
            result.Game.Name.Should().Be("TestName");
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange

            // act
            var result = await _gameService.AddAsync(_testNameFailed, _testDeveloperFailed, _testPublisherFailed, _testGenreFailed, _testDateFailed, _testPriceFailed);

            // assert
            result.Should().NotBeNull();
            result.Game.Should().NotBeNull();
            result.Game.Name.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange

            // act
            var result = await _gameService.DeleteAsync(_testIdSuccess);

            // assert
            result.Should().NotBeNull();
            result.IsDeleted.Should().Be(true);
        }

        [Fact]
        public async Task DeleteAsync_Failed()
        {
            // arrange

            // act
            var result = await _gameService.DeleteAsync(_testIdFailed);

            // assert
            result.Should().NotBeNull();
            result.IsDeleted.Should().Be(false);
        }

        [Fact]
        public async Task UpdateNameAsync_Success()
        {
            // arrange

            // act
            var result = await _gameService.UpdateNameAsync(_testIdSuccess, _testNameSuccess);

            // assert
            result.Should().NotBeNull();
            result.IsUpdated.Should().Be(true);
        }

        [Fact]
        public async Task UpdateNameAsync_Failed()
        {
            // arrange

            // act
            var result = await _gameService.UpdateNameAsync(_testIdFailed, _testNameFailed);

            // assert
            result.Should().NotBeNull();
            result.IsUpdated.Should().Be(false);
        }

        [Fact]
        public async Task UpdateDeveloperAsync_Success()
        {
            // arrange

            // act
            var result = await _gameService.UpdateDeveloperAsync(_testIdSuccess, _testDeveloperSuccess);

            // assert
            result.Should().NotBeNull();
            result.IsUpdated.Should().Be(true);
        }

        [Fact]
        public async Task UpdateDeveloperAsync_Failed()
        {
            // arrange

            // act
            var result = await _gameService.UpdateDeveloperAsync(_testIdFailed, _testDeveloperFailed);

            // assert
            result.Should().NotBeNull();
            result.IsUpdated.Should().Be(false);
        }

        [Fact]
        public async Task UpdatPublisherAsync_Success()
        {
            // arrange

            // act
            var result = await _gameService.UpdatePublisherAsync(_testIdSuccess, _testPublisherSuccess);

            // assert
            result.Should().NotBeNull();
            result.IsUpdated.Should().Be(true);
        }

        [Fact]
        public async Task UpdatePublisherAsync_Failed()
        {
            // arrange

            // act
            var result = await _gameService.UpdatePublisherAsync(_testIdFailed, _testPublisherFailed);

            // assert
            result.Should().NotBeNull();
            result.IsUpdated.Should().Be(false);
        }

        [Fact]
        public async Task UpdatGenreAsync_Success()
        {
            // arrange

            // act
            var result = await _gameService.UpdateGenreAsync(_testIdSuccess, _testGenreSuccess);

            // assert
            result.Should().NotBeNull();
            result.IsUpdated.Should().Be(true);
        }

        [Fact]
        public async Task UpdateGenreAsync_Failed()
        {
            // arrange

            // act
            var result = await _gameService.UpdateGenreAsync(_testIdFailed, _testGenreFailed);

            // assert
            result.Should().NotBeNull();
            result.IsUpdated.Should().Be(false);
        }

        [Fact]
        public async Task UpdatReleaseDateAsync_Success()
        {
            // arrange

            // act
            var result = await _gameService.UpdateReleaseDateAsync(_testIdSuccess, _testDateSuccess);

            // assert
            result.Should().NotBeNull();
            result.IsUpdated.Should().Be(true);
        }

        [Fact]
        public async Task UpdateReleaseDateAsync_Failed()
        {
            // arrange

            // act
            var result = await _gameService.UpdateReleaseDateAsync(_testIdFailed, _testDateFailed);

            // assert
            result.Should().NotBeNull();
            result.IsUpdated.Should().Be(false);
        }

        [Fact]
        public async Task UpdatPriceAsync_Success()
        {
            // arrange

            // act
            var result = await _gameService.UpdatePriceAsync(_testIdSuccess, _testPriceSuccess);

            // assert
            result.Should().NotBeNull();
            result.IsUpdated.Should().Be(true);
        }

        [Fact]
        public async Task UpdatePriceAsync_Failed()
        {
            // arrange

            // act
            var result = await _gameService.UpdatePriceAsync(_testIdFailed, _testPriceFailed);

            // assert
            result.Should().NotBeNull();
            result.IsUpdated.Should().Be(false);
        }
    }
}