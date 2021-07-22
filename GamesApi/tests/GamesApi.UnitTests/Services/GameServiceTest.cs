using AutoMapper;
using FluentAssertions;
using GamesApi.Data;
using GamesApi.Data.Entities;
using GamesApi.DataProviders.Abstractions;
using GamesApi.Models;
using GamesApi.Models.Responses;
using GamesApi.Services;
using GamesApi.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GamesApi.UnitTests.Services
{
    public class GameServiceTest
    {
        private readonly IGameService _gameService;

        private readonly Mock<IGameProvider> _gameProvider;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextFactory<GamesDbContext>> _dbContextFactory;

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
        private readonly string _testIdFail = "testIdFail";

        public GameServiceTest()
        {
            _gameProvider = new Mock<IGameProvider>();
            _mapper = new Mock<IMapper>();
            _dbContextFactory = new Mock<IDbContextFactory<GamesDbContext>>();

            _gameProvider.Setup(s => s.GetByIdAsync(
                It.Is<string>(i => i.Contains(_testIdSuccess)))).ReturnsAsync(_gameEntitySuccess);

            _gameProvider.Setup(s => s.GetByIdAsync(
                It.Is<string>(i => i.Contains(_testIdFail)))).ReturnsAsync(_gameEntityFailed);

            _mapper.Setup(s => s.Map<GameModel>(
                It.Is<GameEntity>(i => i.Equals(_gameEntitySuccess)))).Returns(_gameModelSuccess);

            _mapper.Setup(s => s.Map<GameModel>(
                It.Is<GameEntity>(i => i.Equals(_gameEntityFailed)))).Returns(_gameModelFailed);

            _gameService = new GameService(_dbContextFactory.Object, _gameProvider.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetByIdAsync_Success()
        {
            //arrange

            //act
            var result = await _gameService.GetByIdAsync(_testIdSuccess);

            //assert
            result.Should().NotBeNull();
            result.Game.Should().NotBeNull();
            result.Game.Name.Should().Be("TestName");
        }

        [Fact]
        public async Task GetByIdAsync_Failed()
        {
            //arrange

            //act
            var result = await _gameService.GetByIdAsync(_testIdFail);

            //assert
            result.Should().NotBeNull();
            result.Game.Should().NotBeNull();
            result.Game.Name.Should().BeNull();
        }
    }
}