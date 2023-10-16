using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Infrastructure.DataAccess;

namespace ContractSolutionHub.Domain.Repository.Estate
{
    public interface IContractRepository
    {
        Task<int> CreateContract(Contract contract);
        Task<bool> DeleteContract(int Id);
        Task<Contract> GetContractById(int Id);
        Task<List<Contract>> GetAllContracts();
        Task<Contract> UpdateContract(Contract contract);
    }

    public class ContractRepository : IContractRepository
    {
        IMemoryCache _cache;
        ILocalDbContext db;
        IAuthDbContext _authDb;

        public ContractRepository(DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb)
        {
            _cache = cache;
            _authDb = authDb;
            this.db = new DbFactory<LocalDbContext>(GetConnectionString()).CreateDbContext();
        }

        private static string GetCacheKey(int Id)
        {
            return $"DomainKeyDetails_{Id}";
        }

        private string GetConnectionString()
        {
            var context = new HttpContextAccessor();
            string value = context.HttpContext.Request.Headers["DomainKey"];

            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            if (int.TryParse(value, out int domainKey))
            {
                if (_cache.TryGetValue(GetCacheKey(domainKey), out DomainDetail response))
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

        public void Validate(Contract contract)
        {
            ValidationHelper.CheckIsNull(contract);
            ValidationHelper.CheckException(contract.PropertyId == 0, "Property is required.");
            ValidationHelper.CheckException(contract.CustomerId == 0, "Customer is required.");
            ValidationHelper.CheckException(contract.PurchasePrice <= 0, "Price is should be positive value.");
        }

        public async Task<int> CreateContract(Contract contract)
        {

            try
            {
                Validate(contract);
                contract.CreatedDate = DateTime.Now;
                db.Contracts.Add(contract);
                await db.SaveChangesAsync();
                return contract.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding contract. " + ex.Message);
            }
        }

        public async Task<Contract> UpdateContract(Contract contract)
        {
            try
            {
                Validate(contract);
                contract.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(contract);
                await db.SaveChangesAsync();
                return contract;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating contract. " + ex.Message);
            }
        }

        public async Task<bool> DeleteContract(int Id)
        {
            try
            {
                Contract contract = db.Contracts.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault();

                if (contract == null)
                    throw new Exception("contract does not exist.");

                contract.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting contract. " + ex.Message);
            }
        }

        public async Task<List<Contract>> GetAllContracts()
        {
            return await db.Contracts.Where(m => m.ArchiveDate == null).ToListAsync();
        }

        public async Task<Contract> GetContractById(int Id)
        {
            return await db.Contracts.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefaultAsync();
        }
    }
}
