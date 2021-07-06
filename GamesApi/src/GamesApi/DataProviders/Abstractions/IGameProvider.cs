using System.Collections.Generic;
using System.Threading.Tasks;
using GamesApi.Data;

namespace GamesApi.DataProviders.Abstractions
{
    public interface IGameProvider
    {
        Task<IEnumerable<GameEntity>> GetByPageAsync(int page);
        Task<GameEntity> GetByIdAsync(int id);
        Task<IEnumerable<GameEntity>> GetListGameByListIdAsync(HashSet<int> listId);
        Task<GameEntity> AddAsync(GameEntity game);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(GameEntity game);
    }
}