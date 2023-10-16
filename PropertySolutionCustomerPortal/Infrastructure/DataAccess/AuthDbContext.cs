using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;
using PropertySolutionCustomerPortal.Domain.Entities.Shared;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Infrastructure.DataAccess
{
    public class AuthDbContext : IdentityDbContext<BaseApplicationUser>, IAuthDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<DomainKey> DomainKeys { get; set; }
        public DbSet<BaseApplicationUserType> BaseApplicationUserTypes { get; set; }
        public DbSet<BaseApplicationUserToDomainKeyMap> BaseApplicationUserToDomainKeyMaps { get; set; }

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
            builder.HasDefaultSchema("auth");

            /* ApplicationUser adminUser = new()
             {
                 UserName = "admin@demo.com",
                 Email = "admin@demo.com",
                 Password = "admin@123",
                 NormalizedUserName = "admin@demo.com".ToUpper().Normalize(),
                 NormalizedEmail = "admin@demo.com".ToUpper().Normalize(),
                 DataRoute = "Admin"
             };

             var passwordHasher = new PasswordHasher<ApplicationUser>();
             adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin@123");
             builder.Entity<ApplicationUser>().HasData(adminUser);*/
        }
    }
}
