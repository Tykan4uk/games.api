using System.Collections.Generic;
using System.Threading.Tasks;
using GamesApi.Data;

namespace GamesApi.Services.Abstractions
{
    public interface IGameService
    {
        Task<IEnumerable<GameEntity>> GetByPageAsync(int page);
        Task<GameEntity> GetByIdAsync(int id);
        Task<GameEntity> AddAsync(GameEntity game);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(GameEntity game);
    }
}