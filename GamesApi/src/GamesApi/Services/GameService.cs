using System.Collections.Generic;
using System.Threading.Tasks;
using GamesApi.Data;
using GamesApi.DataProviders.Abstractions;
using GamesApi.Services.Abstractions;

namespace GamesApi.Services
{
    public class GameService : IGameService
    {
        private readonly IGameProvider _gameProvider;

        public GameService(IGameProvider gameProvider)
        {
            _gameProvider = gameProvider;
        }

        public async Task<IEnumerable<GameEntity>> GetByPageAsync(int page)
        {
            return await _gameProvider.GetByPageAsync(page);
        }

        public async Task<GameEntity> GetByIdAsync(int id)
        {
            return await _gameProvider.GetByIdAsync(id);
        }

        public async Task<GameEntity> AddAsync(GameEntity game)
        {
            return await _gameProvider.AddAsync(game);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _gameProvider.DeleteAsync(id);
        }

        public async Task<bool> UpdateAsync(GameEntity game)
        {
            return await _gameProvider.UpdateAsync(game);
        }
    }
}