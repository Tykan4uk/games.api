using System;
using System.Threading.Tasks;
using GamesApi.Data;
using GamesApi.Data.Entities;

namespace GamesApi.DataProviders.Abstractions
{
    public interface IGameProvider
    {
        Task<PagingDataResult> GetByPageAsync(int page, int pageSize);
        Task<GameEntity> GetByIdAsync(string id);
        Task<GameEntity> AddAsync(string name, string developer, string publisher, string genre, DateTime releaseDate, decimal price);
        Task<bool> DeleteAsync(string id);
        Task<bool> UpdateNameAsync(string id, string name);
        Task<bool> UpdateDeveloperAsync(string id, string developer);
        Task<bool> UpdatePublisherAsync(string id, string publisher);
        Task<bool> UpdateGenreAsync(string id, string genre);
        Task<bool> UpdateReleaseDateAsync(string id, DateTime releaseDate);
        Task<bool> UpdatePriceAsync(string id, decimal price);
    }
}