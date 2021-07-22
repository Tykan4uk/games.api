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
    [Authorize(Policy = "ApiScope")]
    public class ManageController : ControllerBase
    {
        private readonly ILogger<ManageController> _logger;
        private readonly IGameService _gameService;

        public ManageController(
            ILogger<ManageController> logger,
            IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByPage([FromQuery] GetByPageRequest request)
        {
            var result = await _gameService.GetByPageAsync(request.Page, request.PageSize, request.SortedType);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdRequest request)
        {
            var result = await _gameService.GetByIdAsync(request.Id);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddRequest request)
        {
            var result = await _gameService.AddAsync(request.Name, request.Developer, request.Publisher, request.Genre, request.ReleaseDate, request.Price);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {
            var result = await _gameService.DeleteAsync(request.Id);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutName([FromBody] PutNameRequest request)
        {
            var result = await _gameService.UpdateNameAsync(request.Id, request.Name);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutDeveloper([FromBody] PutDeveloperRequest request)
        {
            var result = await _gameService.UpdateDeveloperAsync(request.Id, request.Developer);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutPublisher([FromBody] PutPublisherRequest request)
        {
            var result = await _gameService.UpdatePublisherAsync(request.Id, request.Publisher);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutGenre([FromBody] PutGenreRequest request)
        {
            var result = await _gameService.UpdateGenreAsync(request.Id, request.Genre);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutReleaseDate([FromBody] PutReleaseDateRequest request)
        {
            var result = await _gameService.UpdateReleaseDateAsync(request.Id, request.ReleaseDate);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutPrice([FromBody] PutPriceRequest request)
        {
            var result = await _gameService.UpdatePriceAsync(request.Id, request.Price);
            return result != null ? Ok(result) : BadRequest(result);
        }
    }
}