using FluentAssertions;
using GamesApi.Data;
using GamesApi.DataProviders.Abstractions;
using GamesApi.Services;
using GamesApi.Services.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace GamesApi.UnitTests.Services
{
    public class GameServiceTest
    {
        private IGameService _gameService;

        private readonly Mock<IGameProvider> _gameProvider;

        public GameServiceTest()
        {
            var game = new GameEntity()
            {
                Id = 0,
                Name = "Name",
                Developer = "Developer",
                Publisher = "Publisher",
                Genre = "Genre",
                ReleaseDate = new DateTime(),
                Price = 1
            };
            var gameList = new List<GameEntity>()
            {
                game
            };

            _gameProvider = new Mock<IGameProvider>();

            _gameProvider.Setup(s => s.GetByPageAsync(
                It.IsInRange<int>(0, 10, Moq.Range.Inclusive))).ReturnsAsync(gameList);

            _gameProvider.Setup(s => s.GetByIdAsync(
                It.IsInRange<int>(0, 10, Moq.Range.Inclusive))).ReturnsAsync(game);

            _gameProvider.Setup(s => s.GetListGameByListIdAsync(
                It.IsNotNull<HashSet<int>>())).ReturnsAsync(gameList);

            _gameProvider.Setup(s => s.AddAsync(
                It.IsNotNull<GameEntity>())).ReturnsAsync(game);

            _gameProvider.Setup(s => s.DeleteAsync(
                It.IsInRange<int>(0, 10, Moq.Range.Inclusive))).ReturnsAsync(true);

            _gameProvider.Setup(s => s.UpdateAsync(
                It.IsNotNull<GameEntity>())).ReturnsAsync(true);

            _gameService = new GameService(_gameProvider.Object);
        }

        [Fact]
        public async void GetByPageAsyncTestSuccess()
        {
            //arrange
            var pageTest = 0;

            //act
            var result = await _gameService.GetByPageAsync(pageTest);

            //assert
            result.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public async void GetByPageAsyncTestFailed()
        {
            //arrange
            var pageTest = -1;

            //act
            var result = await _gameService.GetByPageAsync(pageTest);

            //assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async void GetByIdAsyncTestSuccess()
        {
            //arrange
            var idTest = 0;

            //act
            var result = await _gameService.GetByIdAsync(idTest);

            //assert
            result.Should().BeOfType<GameEntity>();
        }
        [Fact]
        public async void GetByIdAsyncTestFailed()
        {
            //arrange
            var idTest = -1;

            //act
            var result = await _gameService.GetByIdAsync(idTest);

            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async void GetListGameByListIdAsyncTestSuccess()
        {
            //arrange
            var idListTest = new HashSet<int>() { 0 };

            //act
            var result = await _gameService.GetListGameByListIdAsync(idListTest);

            //assert
            result.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public async void GetListGameByListIdAsyncTestFailed()
        {
            //arrange

            //act
            var result = await _gameService.GetListGameByListIdAsync(null);

            //assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async void AddAsyncTestSuccess()
        {
            //arrange
            var gameTest = new GameEntity()
            {
                Id = 0,
                Name = "Name",
                Developer = "Developer",
                Publisher = "Publisher",
                Genre = "Genre",
                ReleaseDate = new DateTime(),
                Price = 1
            };

            //act
            var result = await _gameService.AddAsync(gameTest);

            //assert
            result.Should().BeOfType<GameEntity>();
        }
        [Fact]
        public async void AddAsyncTestFailed()
        {
            //arrange

            //act
            var result = await _gameService.AddAsync(null);

            //assert
            result.Should().BeNull();
        }

        [Fact]
        public async void DeleteAsyncTestSuccess()
        {
            //arrange
            var idTest = 0;

            //act
            var result = await _gameService.DeleteAsync(idTest);

            //assert
            result.Should().BeTrue();
        }
        [Fact]
        public async void DeleteAsyncTestFailed()
        {
            //arrange
            var idTest = -1;

            //act
            var result = await _gameService.DeleteAsync(idTest);

            //assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void UpdateAsyncTestSuccess()
        {
            //arrange
            var gameTest = new GameEntity()
            {
                Id = 0,
                Name = "Name",
                Developer = "Developer",
                Publisher = "Publisher",
                Genre = "Genre",
                ReleaseDate = new DateTime(),
                Price = 1
            };

            //act
            var result = await _gameService.UpdateAsync(gameTest);

            //assert
            result.Should().BeTrue();
        }
        [Fact]
        public async void UpdateAsyncTestFailed()
        {
            //arrange

            //act
            var result = await _gameService.UpdateAsync(null);

            //assert
            result.Should().BeFalse();
        }
    }
}
