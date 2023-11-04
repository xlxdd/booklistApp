using booklistInfrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace booklistAPI
{
    public class UOW : IAsyncActionFilter
    {
        private readonly AppDbContext _dbContext;

        public UOW(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            var resultContext = await next();

            if (resultContext.Exception == null)
            {
                try
                {
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            else
            {
                await transaction.RollbackAsync();
            }
        }
    }

}
