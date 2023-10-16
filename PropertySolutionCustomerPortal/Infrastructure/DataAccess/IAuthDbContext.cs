using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;
using PropertySolutionCustomerPortal.Domain.Entities.Shared;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Infrastructure.DataAccess
{
    public interface IAuthDbContext
    {
        DbSet<BaseApplicationUserToDomainKeyMap> BaseApplicationUserToDomainKeyMaps { get; set; }
        DbSet<BaseApplicationUserType> BaseApplicationUserTypes { get; set; }
        DbSet<DomainKey> DomainKeys { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }

        void Add<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void SetStateAsAdded<TEntity>(TEntity @object) where TEntity : class;
        void SetStateAsModified<TEntity>(TEntity @object) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
    }
}