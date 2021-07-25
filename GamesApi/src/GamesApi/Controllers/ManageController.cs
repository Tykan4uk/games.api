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

            if (result == null)
            {
                _logger.LogInformation("(ManageController/GetByPage)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdRequest request)
        {
            var result = await _gameService.GetByIdAsync(request.Id);

            if (result == null)
            {
                _logger.LogInformation("(ManageController/GetById)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddRequest request)
        {
            var result = await _gameService.AddAsync(request.Name, request.Developer, request.Publisher, request.Genre, request.ReleaseDate, request.Price, request.ImageUrl, request.Description);

            if (result == null)
            {
                _logger.LogInformation("(ManageController/Add)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {
            var result = await _gameService.DeleteAsync(request.Id);

            if (result == null)
            {
                _logger.LogInformation("(ManageController/Delete)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutName([FromBody] PutNameRequest request)
        {
            var result = await _gameService.UpdateNameAsync(request.Id, request.Name);

            if (result == null)
            {
                _logger.LogInformation("(ManageController/PutName)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutDeveloper([FromBody] PutDeveloperRequest request)
        {
            var result = await _gameService.UpdateDeveloperAsync(request.Id, request.Developer);

            if (result == null)
            {
                _logger.LogInformation("(ManageController/PutDeveloper)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutPublisher([FromBody] PutPublisherRequest request)
        {
            var result = await _gameService.UpdatePublisherAsync(request.Id, request.Publisher);

            if (result == null)
            {
                _logger.LogInformation("(ManageController/PutPublisher)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutGenre([FromBody] PutGenreRequest request)
        {
            var result = await _gameService.UpdateGenreAsync(request.Id, request.Genre);

            if (result == null)
            {
                _logger.LogInformation("(ManageController/PutGenre)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutReleaseDate([FromBody] PutReleaseDateRequest request)
        {
            var result = await _gameService.UpdateReleaseDateAsync(request.Id, request.ReleaseDate);

            if (result == null)
            {
                _logger.LogInformation("(ManageController/PutReleaseDate)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutPrice([FromBody] PutPriceRequest request)
        {
            var result = await _gameService.UpdatePriceAsync(request.Id, request.Price);

            if (result == null)
            {
                _logger.LogInformation("(ManageController/PutPrice)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutImageUrl([FromBody] PutImageUrlRequest request)
        {
            var result = await _gameService.UpdateImageUrlAsync(request.Id, request.ImageUrl);

            if (result == null)
            {
                _logger.LogInformation("(ManageController/PutImageUrl)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutDescription([FromBody] PutDescriptionRequest request)
        {
            var result = await _gameService.UpdateDescriptionAsync(request.Id, request.Description);

            if (result == null)
            {
                _logger.LogInformation("(ManageController/PutDescription)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}