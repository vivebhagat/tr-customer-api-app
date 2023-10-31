using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Infrastructure.DataAccess;

namespace PropertySolutionCustomerPortal.Domain.Repository.Estate
{
    public interface ICommunityRepository
    {
        Task<int> CreateCommunity(Community community);
        Task<bool> DeleteCommunity(int Id);
        Task<Community> GetCommunityById(int Id);
        Task<List<Community>> GetAllCommunities();
        Task<Community> UpdateCommunity(Community community);
        Task<List<Community>> GetHomeDisplayCommunitites();
    }

    public class CommunityRepository : ICommunityRepository
    {
        IMemoryCache _cache;
        ILocalDbContext db;
        IAuthDbContext _authDb;
        private readonly string DomainKey = string.Empty;
        IHttpHelper _httpHelper;


        public CommunityRepository(DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb, ILocalDbContext db, IHttpHelper httpHelper)
        {
            _cache = cache;
            _authDb = authDb;
            this.db = db;
            _httpHelper = httpHelper;
        }


        public void Validate(Community community)
        {
            ValidationHelper.CheckIsNull(community);
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(community.Name), "Name is required.");
        }

        public async Task<int> CreateCommunity(Community community)
        {

            try
            {
                var context = new HttpContextAccessor();
                var domainKey = context.HttpContext.Request.Headers["DomainKey"];
                Validate(community);
                community.CreatedDate = DateTime.Now;
                community.DomainKey = int.Parse(domainKey);
                db.Communities.Add(community);
                await db.SaveChangesAsync();
                return community.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding community. " + ex.Message);
            }
        }

        public async Task<Community> UpdateCommunity(Community community)
        {
            try
            {
                var context = new HttpContextAccessor();
                var domainKey = context.HttpContext.Request.Headers["DomainKey"];
                Validate(community);
                community.ModifiedDate = DateTime.Now;
                community.DomainKey = int.Parse(domainKey);
                db.SetStateAsModified(community);
                await db.SaveChangesAsync();
                return community;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating community. " + ex.Message);
            }
        }

        public async Task<bool> DeleteCommunity(int Id)
        {
            try
            {
                Community community = db.Communities.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault();

                if (community == null)
                    throw new Exception("Community does not exist.");

                community.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting community. " + ex.Message);
            }
        }

        public async Task<List<Community>> GetAllCommunities()
        {
            return await db.Communities.Where(m => m.IsPublished && m.ArchiveDate == null).ToListAsync();
        }

        public async Task<List<Community>> GetHomeDisplayCommunitites()
        {
            return await db.Communities.Where(m => m.IsPublished && m.ArchiveDate == null).OrderByDescending(m => m.Id).Take(6).ToListAsync();
        }

        public async Task<Community> GetCommunityById(int Id)
        {
            return await db.Communities.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefaultAsync();
        }
    }
}
