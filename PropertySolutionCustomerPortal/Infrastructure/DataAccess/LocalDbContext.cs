using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Infrastructure.DataAccess
{
    public class LocalDbContext : DbContext, ILocalDbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
        {

        }

        public LocalDbContext() : base()
        {

        }


        public DbSet<LeaseRequest> LeaseRequests { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<LeaseAgreement> Lease { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyReview> PropertyReview { get; set; }
        public DbSet<ContractRequest> ContractRequests { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerToRoleMap> CustomerToRoleMaps { get; set; }
        public DbSet<CustomerRole> CustomerRole { get; set; }

        public DbSet<Community> Communities { get; set; }



        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry(entity);
        }
        public void SetStateAsAdded<TEntity>(TEntity @object) where TEntity : class
        {
            base.Entry(@object).State = EntityState.Added;
        }

        public void SetStateAsModified<TEntity>(TEntity @object) where TEntity : class
        {
            base.Entry(@object).State = EntityState.Modified;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            base.Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            base.Update(entity);
        }


        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            base.Remove(entity);
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("data");
        }
    }
}