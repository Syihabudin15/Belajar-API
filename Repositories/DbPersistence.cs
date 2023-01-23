using Microsoft.EntityFrameworkCore;

namespace BelajarRestApi.Repositories
{
    public class DbPersistence : IPersistence
    {
        private readonly AppDbContext _appDbContext;
        public DbPersistence(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func)
        {
            var strategy = _appDbContext.Database.CreateExecutionStrategy();
            var result = await strategy.ExecuteAsync(async () =>
            {
                var transaction = await _appDbContext.Database.BeginTransactionAsync();
                try
                {
                    var result = await func();
                    await transaction.CommitAsync();
                    return result;

                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
            return result;
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
