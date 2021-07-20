using System;
using System.Threading.Tasks;
using GamesApi.Common.Enums;
using GamesApi.Models.Responses;

namespace GamesApi.Services.Abstractions
{
    public interface IGameService
    {
        Task<GetByPageResponse> GetByPageAsync(int page, int pageSize, SortedTypeEnum sortedType);
        Task<GetByIdResponse> GetByIdAsync(string id);
        Task<AddResponse> AddAsync(string name, string developer, string publisher, string genre, DateTime releaseDate, decimal price);
        Task<DeleteResponse> DeleteAsync(string id);
        Task<UpdateResponse> UpdateNameAsync(string id, string name);
        Task<UpdateResponse> UpdateDeveloperAsync(string id, string developer);
        Task<UpdateResponse> UpdatePublisherAsync(string id, string publisher);
        Task<UpdateResponse> UpdateGenreAsync(string id, string genre);
        Task<UpdateResponse> UpdateReleaseDateAsync(string id, DateTime releaseDate);
        Task<UpdateResponse> UpdatePriceAsync(string id, decimal price);
    }
}