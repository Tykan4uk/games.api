using System;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Common.Enums;
using GamesApi.Common.Exceptions;
using GamesApi.Data;
using GamesApi.Data.Entities;
using GamesApi.DataProviders.Abstractions;
using GamesApi.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GamesApi.DataProviders
{
    public class GameProvider : IGameProvider
    {
        private readonly GamesDbContext _gamesDbContext;
        private readonly ILogger<GameProvider> _logger;

        public GameProvider(
            IDbContextWrapper<GamesDbContext> dbContextWrapper,
            ILogger<GameProvider> logger)
        {
            _gamesDbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<PagingDataResult> GetByPageAsync(int page, int pageSize, SortedTypeEnum sortedType)
        {
            if (page <= 0 || pageSize <= 0)
            {
                _logger.LogError("(GameProvider/GetByPageAsync)Page or page size error!");
                throw new BusinessException("Page or page size error!");
            }

            IQueryable<GameEntity> query = _gamesDbContext.Games;
            switch (sortedType)
            {
                case SortedTypeEnum.CreateDateAscending:
                    query = query.OrderBy(o => o.CreateDate);
                    break;
                case SortedTypeEnum.CreateDateDescending:
                    query = query.OrderByDescending(o => o.CreateDate);
                    break;
                case SortedTypeEnum.PriceAscending:
                    query = query.OrderBy(o => o.Price);
                    break;
                case SortedTypeEnum.PriceDescending:
                    query = query.OrderByDescending(o => o.Price);
                    break;
                default:
                    query = query.OrderBy(o => o.CreateDate);
                    break;
            }

            var totalRecords = await _gamesDbContext.Games.CountAsync();
            var pageData = await query
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
            var createDate = DateTime.Now;
            var result = await _gamesDbContext.Games.AddAsync(
                new GameEntity()
                {
                    Id = id,
                    Name = name,
                    Developer = developer,
                    Publisher = publisher,
                    Genre = genre,
                    ReleaseDate = releaseDate,
                    Price = price,
                    CreateDate = createDate
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