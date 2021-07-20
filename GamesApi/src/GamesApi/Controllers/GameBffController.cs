using System.Threading.Tasks;
using GamesApi.Models.Requests;
using GamesApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GamesApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class GameBffController : ControllerBase
    {
        private readonly ILogger<GameBffController> _logger;
        private readonly IGameService _gameService;

        public GameBffController(
            ILogger<GameBffController> logger,
            IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        [HttpPost]
        public async Task<IActionResult> GetByPage([FromBody] GetByPageRequest request)
        {
            var result = await _gameService.GetByPageAsync(request.Page, request.PageSize, request.SortedType);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetById([FromBody] GetByIdRequest request)
        {
            var result = await _gameService.GetByIdAsync(request.Id);
            return result != null ? Ok(result) : BadRequest(result);
        }
    }
}