using Microsoft.EntityFrameworkCore;

namespace PropertySolutionCustomerPortal.Infrastructure.DataAccess
{
    public class DbFactory<TContext> : IDbContextFactory<TContext> where TContext : DbContext
    {
        private readonly string _connectionString;

        public DbFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(_connectionString);
            return (TContext)Activator.CreateInstance(typeof(TContext), optionsBuilder.Options);
        }
    }
}
