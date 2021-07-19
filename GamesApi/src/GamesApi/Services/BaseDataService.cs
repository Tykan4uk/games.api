using System;
using System.Threading.Tasks;
using GamesApi.Data;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.Services
{
    public class BaseDataService
    {
        private readonly GamesDbContext _gamesDbContext;

        public BaseDataService(IDbContextFactory<GamesDbContext> dbContextFactory)
        {
            _gamesDbContext = dbContextFactory.CreateDbContext();
        }

        protected async Task<T> ExecuteSafe<T>(Func<Task<T>> action)
        {
            using (var transaction = _gamesDbContext.Database.BeginTransaction())
            {
                try
                {
                    var result = await action();
                    transaction.Commit();
                    return result;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
