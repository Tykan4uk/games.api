using System;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Data;
using GamesApi.Data.Entities;
using GamesApi.DataProviders.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.DataProviders
{
    public class GameProvider : IGameProvider
    {
        private readonly GamesDbContext _gamesDbContext;

        public GameProvider(IDbContextFactory<GamesDbContext> dbContextFactory)
        {
            _gamesDbContext = dbContextFactory.CreateDbContext();
        }

        public async Task<PagingDataResult> GetByPageAsync(int page, int pageSize)
        {
            var totalRecords = await _gamesDbContext.Games.CountAsync();
            var pageData = await _gamesDbContext.Games
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingDataResult() { Games = pageData, TotalRecords = totalRecords };
        }

        public async Task<GameEntity> GetByIdAsync(string id)
        {
            return await _gamesDbContext.Games.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<GameEntity> AddAsync(string name, string developer, string publisher, string genre, DateTime releaseDate, decimal price)
        {
            var id = Guid.NewGuid().ToString();
            var result = await _gamesDbContext.Games.AddAsync(
                new GameEntity()
                {
                    Id = id,
                    Name = name,
                    Developer = developer,
                    Publisher = publisher,
                    Genre = genre,
                    ReleaseDate = releaseDate,
                    Price = price
                });
            await _gamesDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<bool> DeleteAsync(string id)
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

        public async Task<bool> UpdateNameAsync(string id, string name)
        {
            var result = _gamesDbContext.Games.FirstOrDefault(f => f.Id == id);

            if (result != null)
            {
                result.Name = name;
                await _gamesDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateDeveloperAsync(string id, string developer)
        {
            var result = _gamesDbContext.Games.FirstOrDefault(f => f.Id == id);

            if (result != null)
            {
                result.Developer = developer;
                await _gamesDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdatePublisherAsync(string id, string publisher)
        {
            var result = _gamesDbContext.Games.FirstOrDefault(f => f.Id == id);

            if (result != null)
            {
                result.Publisher = publisher;
                await _gamesDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateGenreAsync(string id, string genre)
        {
            var result = _gamesDbContext.Games.FirstOrDefault(f => f.Id == id);

            if (result != null)
            {
                result.Genre = genre;
                await _gamesDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateReleaseDateAsync(string id, DateTime releaseDate)
        {
            var result = _gamesDbContext.Games.FirstOrDefault(f => f.Id == id);

            if (result != null)
            {
                result.ReleaseDate = releaseDate;
                await _gamesDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdatePriceAsync(string id, decimal price)
        {
            var result = _gamesDbContext.Games.FirstOrDefault(f => f.Id == id);

            if (result != null)
            {
                result.Price = price;
                await _gamesDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}