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
        private readonly ILogger<ManageController> _logger;
        private readonly IGameService _gameService;
        private readonly Config _config;

        public GameBffController(
            ILogger<ManageController> logger,
            IOptions<Config> config,
            IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
            _config = config.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetByPage(int page)
        {
            var result = await _gameService.GetByPageAsync(page);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _gameService.GetByIdAsync(id);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetListGameByListId([FromBody] HashSet<int> listId)
        {
            var result = await _gameService.GetListGameByListIdAsync(listId);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GameEntity game)
        {
            var result = await _gameService.AddAsync(game);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] GameEntity game)
        {
            var result = await _gameService.UpdateAsync(game);
            return result ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var result = await _gameService.DeleteAsync(id);
            return result ? Ok(result) : BadRequest(result);
        }
    }
}