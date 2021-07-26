using System.Net.Http;
using System.Threading.Tasks;
using GamesApi.Models.Responses;
using GamesApi.Services.Abstractions;

namespace GamesApi.Services
{
    public class RateLimitService : IRateLimitService
    {
        private readonly IHttpClientService _httpClientService;

        public RateLimitService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<CheckRateLimitResponse> CheckRateLimit(string name)
        {
            string url = $"http://192.168.1.120:5004/api/v1/Manage/CheckRateLimit?name={name}";
            var response = await _httpClientService.SendAsync<CheckRateLimitResponse>(url, HttpMethod.Get);
            return response;
        }
    }
}
