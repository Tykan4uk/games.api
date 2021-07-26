using System.Threading.Tasks;
using GamesApi.Models.Requests;
using GamesApi.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GamesApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [Authorize(Policy = "ApiScopeBff")]
    public class GameBffController : ControllerBase
    {
        private readonly ILogger<GameBffController> _logger;
        private readonly IGameService _gameService;
        private readonly IRateLimitService _rateLimitService;

        public GameBffController(
            ILogger<GameBffController> logger,
            IGameService gameService,
            IRateLimitService rateLimitService)
        {
            _logger = logger;
            _gameService = gameService;
            _rateLimitService = rateLimitService;
        }

        [HttpPost]
        public async Task<IActionResult> GetByPage([FromBody] GetByPageRequest request)
        {
            var result = await _gameService.GetByPageAsync(request.Page, request.PageSize, request.SortedType);

            if (result == null)
            {
                _logger.LogInformation("(GameBffController/GetByPage)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetById([FromBody] GetByIdRequest request)
        {
            var ip = HttpContext.Connection.RemoteIpAddress;
            var url = HttpContext.Request.Path.ToUriComponent();

            var checkRateLimit = await _rateLimitService.CheckRateLimit($"{ip}{url}");
            if (checkRateLimit.CheckRateLimit)
            {
                var result = await _gameService.GetByIdAsync(request.Id);

                if (result == null)
                {
                    _logger.LogInformation("(GameBffController/GetById)Null result. Bad request.");
                    return BadRequest(result);
                }

                return Ok(result);
            }

            return StatusCode(429);
        }
    }
}