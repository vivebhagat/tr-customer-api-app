using Microsoft.Extensions.Caching.Memory;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;
using PropertySolutionCustomerPortal.Infrastructure.DataAccess;

namespace PropertySolutionCustomerPortal.Domain.Repository
{
    public interface IDynamicDbRepository
    {
        string GetConnectionString();
        string GetDomainKey();
    }

    public abstract class AbstractDynamicDbRepository : IDynamicDbRepository
    {
        IMemoryCache _cache;
        IAuthDbContext _authDb;
        protected AbstractDynamicDbRepository(IMemoryCache cache, IAuthDbContext authDb)
        {
            this._cache = cache;
            _authDb = authDb;
        }
        public string GetDomainKey()
        {
            var context = new HttpContextAccessor();
            return context.HttpContext.Request.Headers["DomainKey"];
        }

        public string GetConnectionString()
        {
            string value = GetDomainKey();

            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            if (int.TryParse(value, out int domainKey))
            {
                if (_cache.TryGetValue($"DomainKeyDetails_{domainKey}", out DomainDetail response))
                    return response.ConnectionString;
                else
                {
                    string connectionString = _authDb.BaseApplicationUserToDomainKeyMaps.Where(m => m.DomainKey.Value == domainKey && m.ArchiveDate == null).Select(m => m.DomainKey.ConnectionString).FirstOrDefault();

                    if (string.IsNullOrWhiteSpace(connectionString))
                        return string.Empty;

                    return connectionString;
                }
            }

            return string.Empty;
        }
    }

}
