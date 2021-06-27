using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Configuration;
using GamesApi.Data;
using GamesApi.DataProviders.Abstractions;
using Microsoft.Extensions.Options;

namespace GamesApi.DataProviders
{
    public class GameProvider : IGameProvider
    {
        private readonly GamesDbContext _gamesDbContext;
        private readonly int _pageSize;

        public GameProvider(GamesDbContext gamesDbContext, IOptions<GamesApiConfig> options)
        {
            _gamesDbContext = gamesDbContext;
            _pageSize = options.Value.PageSize;
        }

        public async Task<IEnumerable<GameEntity>> GetByPageAsync(int page)
        {
            return await Task.Run(() =>
            {
                var skipNumber = page * _pageSize;
                var takeNumber = _pageSize;
                return _gamesDbContext.Games.Skip(skipNumber).Take(takeNumber);
            });
        }

        public async Task<GameEntity> GetByIdAsync(int id)
        {
            return await Task.Run(() =>
            {
                return _gamesDbContext.Games.FirstOrDefault(f => f.Id == id);
            });
        }

        public async Task<GameEntity> AddAsync(GameEntity game)
        {
            var result = await _gamesDbContext.Games.AddAsync(game);
            await _gamesDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = _gamesDbContext.Games.FirstOrDefault(f => f.Id == id);

            if (result != null)
            {
                _gamesDbContext.Games.Remove(result);
                await _gamesDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(GameEntity game)
        {
            var result = _gamesDbContext.Games.FirstOrDefault(f => f.Id == game.Id);

            if (result != null)
            {
                _gamesDbContext.Games.Update(result);
                await _gamesDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}