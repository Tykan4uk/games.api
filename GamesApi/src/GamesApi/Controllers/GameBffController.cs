using System.Collections.Generic;
using System.Threading.Tasks;
using GamesApi.Configuration;
using GamesApi.Data;
using GamesApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
        public async Task<IActionResult> GetByPage(int page)
        {
            var result = await _gameService.GetByPageAsync(page);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _gameService.GetByIdAsync(id);
            return result != null ? Ok(result) : BadRequest(result);
        }
    }
}