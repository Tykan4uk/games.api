using System.Threading.Tasks;
using GamesApi.Models.Responses;

namespace GamesApi.Services.Abstractions
{
    public interface IRateLimitService
    {
        Task<CheckRateLimitResponse> CheckRateLimit(string name);
    }
}
