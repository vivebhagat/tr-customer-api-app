using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Infrastructure.DataAccess
{
    public interface ILocalDbContext
    {
       
        DbSet<LeaseAgreement> Lease { get; set; }
        DbSet<LeaseRequest> LeaseRequests { get; set; }
        DbSet<Property> Property { get; set; }
        DbSet<PropertyImage> PropertyImages { get; set; }
        DbSet<PropertyReview> PropertyReview { get; set; }
        DbSet<ContractRequest> ContractRequests { get; set; }
        DbSet<Contract> Contracts { get; set; }

        DbSet<Customer> Customers { get; set; }
        DbSet<CustomerToRoleMap> CustomerToRoleMaps { get; set; }
        DbSet<CustomerRole> CustomerRole { get; set; }

        DbSet<Community> Communities { get; set; }


        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;
        void SetStateAsModified<TEntity>(TEntity @object) where TEntity : class;
        void SetStateAsAdded<TEntity>(TEntity @object) where TEntity : class;
    }
}