using System;
using System.Threading.Tasks;
using GamesApi.Data;
using GamesApi.Services;
using GamesApi.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace GamesApi.UnitTests.Mocks
{
    public class MockService : BaseDataService<GamesDbContext>
    {
        public MockService(
            IDbContextWrapper<GamesDbContext> dbContextWrapper,
            ILogger<MockService> logger)
            : base(dbContextWrapper, logger)
        {
        }

        public async Task RunWithException()
        {
            await ExecuteSafe<bool>(() =>
            {
                throw new Exception();
            });
        }

        public async Task RunWithoutException()
        {
            await ExecuteSafe<bool>(() =>
            {
                return Task.FromResult(true);
            });
        }
    }
}
