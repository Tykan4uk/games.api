using System;
using System.Threading.Tasks;
using AutoMapper;
using GamesApi.Data;
using GamesApi.DataProviders.Abstractions;
using GamesApi.Models;
using GamesApi.Models.Responses;
using GamesApi.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.Services
{
    public class GameService : BaseDataService, IGameService
    {
        private readonly IGameProvider _gameProvider;
        private readonly IMapper _mapper;

        public GameService(
            IDbContextFactory<GamesDbContext> factory,
            IGameProvider gameProvider,
            IMapper mapper)
            : base(factory)
        {
            _gameProvider = gameProvider;
            _mapper = mapper;
        }

        public async Task<GetByPageResponse> GetByPageAsync(int page, int pageSize)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _gameProvider.GetByPageAsync(page, pageSize);

                return _mapper.Map<GetByPageResponse>(result);
            });
        }

        public async Task<GetByIdResponse> GetByIdAsync(string id)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _gameProvider.GetByIdAsync(id);

                var game = _mapper.Map<GameModel>(result);

                return new GetByIdResponse() { Game = game };
            });
        }

        public async Task<AddResponse> AddAsync(string name, string developer, string publisher, string genre, DateTime releaseDate, decimal price)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _gameProvider.AddAsync(name, developer, publisher, genre, releaseDate, price);

                var game = _mapper.Map<GameModel>(result);

                return new AddResponse() { Game = game };
            });
        }

        public async Task<DeleteResponse> DeleteAsync(string id)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _gameProvider.DeleteAsync(id);

                return new DeleteResponse() { IsDeleted = result };
            });
        }

        public async Task<UpdateResponse> UpdateNameAsync(string id, string name)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _gameProvider.UpdateNameAsync(id, name);

                return new UpdateResponse() { IsUpdated = result };
            });
        }

        public async Task<UpdateResponse> UpdateDeveloperAsync(string id, string developer)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _gameProvider.UpdateDeveloperAsync(id, developer);

                return new UpdateResponse() { IsUpdated = result };
            });
        }

        public async Task<UpdateResponse> UpdatePublisherAsync(string id, string publisher)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _gameProvider.UpdatePublisherAsync(id, publisher);

                return new UpdateResponse() { IsUpdated = result };
            });
        }

        public async Task<UpdateResponse> UpdateGenreAsync(string id, string genre)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _gameProvider.UpdateGenreAsync(id, genre);

                return new UpdateResponse() { IsUpdated = result };
            });
        }

        public async Task<UpdateResponse> UpdateReleaseDateAsync(string id, DateTime releaseDate)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _gameProvider.UpdateReleaseDateAsync(id, releaseDate);

                return new UpdateResponse() { IsUpdated = result };
            });
        }

        public async Task<UpdateResponse> UpdatePriceAsync(string id, decimal price)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _gameProvider.UpdatePriceAsync(id, price);

                return new UpdateResponse() { IsUpdated = result };
            });
        }
    }
}