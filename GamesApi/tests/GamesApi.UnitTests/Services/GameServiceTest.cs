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
        private readonly IGameService _gameService;

        private readonly Mock<IGameProvider> _gameProvider;

        private readonly GameEntity _gameTest;

        private List<GameEntity> _gameListTest;

        public GameServiceTest()
        {
            _gameTest = new GameEntity()
            {
                Id = 0,
                Name = "Name",
                Developer = "Developer",
                Publisher = "Publisher",
                Genre = "Genre",
                ReleaseDate = DateTime.Now,
                Price = 1
            };

            _gameListTest = new List<GameEntity>()
            {
                _gameTest
            };

            _gameProvider = new Mock<IGameProvider>();

            _gameProvider.Setup(s => s.GetByPageAsync(
                It.IsAny<int>())).ReturnsAsync(_gameListTest);

            _gameProvider.Setup(s => s.GetByPageAsync(
                It.IsInRange<int>(-10, -1, Moq.Range.Inclusive))).ReturnsAsync(new List<GameEntity>());

            _gameProvider.Setup(s => s.GetByIdAsync(
                It.IsAny<int>())).ReturnsAsync(_gameTest);

            _gameProvider.Setup(s => s.GetByIdAsync(
                It.IsInRange<int>(-10, -1, Moq.Range.Inclusive))).ReturnsAsync(new GameEntity());

            _gameProvider.Setup(s => s.GetListGameByListIdAsync(
                It.IsNotNull<HashSet<int>>())).ReturnsAsync(_gameListTest);

            _gameProvider.Setup(s => s.AddAsync(
                It.IsNotNull<GameEntity>())).ReturnsAsync(_gameTest);

            _gameProvider.Setup(s => s.DeleteAsync(
                It.IsAny<int>())).ReturnsAsync(true);

            _gameProvider.Setup(s => s.DeleteAsync(
                It.IsInRange<int>(-10, -1, Moq.Range.Inclusive))).ReturnsAsync(false);

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
            result.Should().BeSameAs(_gameListTest);
        }
        [Fact]
        public async void GetByPageAsyncTestFailed()
        {
            //arrange
            var pageTest = -1;

            //act
            var result = await _gameService.GetByPageAsync(pageTest);

            //assert
            result.Should().NotBeSameAs(_gameListTest);
        }

        [Fact]
        public async void GetByIdAsyncTestSuccess()
        {
            //arrange
            var idTest = 0;

            //act
            var result = await _gameService.GetByIdAsync(idTest);

            //assert
            result.Should().BeSameAs(_gameTest);
        }
        [Fact]
        public async void GetByIdAsyncTestFailed()
        {
            //arrange
            var idTest = -1;

            //act
            var result = await _gameService.GetByIdAsync(idTest);

            //assert
            result.Should().NotBeSameAs(_gameTest);
        }

        [Fact]
        public async void GetListGameByListIdAsyncTestSuccess()
        {
            //arrange
            var idListTest = new HashSet<int>() { 0 };

            //act
            var result = await _gameService.GetListGameByListIdAsync(idListTest);

            //assert
            result.Should().BeSameAs(_gameListTest);
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

            //act
            var result = await _gameService.AddAsync(_gameTest);

            //assert
            result.Should().BeSameAs(_gameTest);
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

            //act
            var result = await _gameService.UpdateAsync(_gameTest);

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
