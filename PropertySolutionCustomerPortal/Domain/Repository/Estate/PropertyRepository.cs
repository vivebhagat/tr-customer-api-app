using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Setup;
using PropertySolutionCustomerPortal.Domain.Entities.Shared;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.EntityFilter;
using PropertySolutionCustomerPortal.Domain.EntityFilter.FilterModel;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Infrastructure.DataAccess;
using System;

namespace PropertySolutionCustomerPortal.Domain.Repository.Estate
{
    public interface IPropertyRepository
    {
        Task<int> CreateProperty(Property property);
        Task<bool> DeleteProperty(int Id);
        Task<Property> GetPropertyById(int Id);
        Task<List<Property>> GetAllProperties(PropertyFilterUIModel @object);
        Task<Property> UpdateProperty(Property property);
        Task<List<Property>> GetHomeDisplayProperties();
        Task<List<Organization>> GetOrganizationDetails(string domainKey);
        Task<BusinessUser> GetManagerDetails(int Id, string domainKey);
    }

    public class PropertyRepository : IPropertyRepository
    {
        IMemoryCache _cache;
        ILocalDbContext db;
        IAuthDbContext _authDb;
        private readonly string DomainKey = string.Empty;
        IPropertyFilter _propertyFilter;
        IHttpHelper _httpHelper;


        public PropertyRepository(DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb, ILocalDbContext db, IPropertyFilter propertyFilter, IHttpHelper httpHelper)
        {
            _cache = cache;
            _authDb = authDb;
            this.db = db;
            _propertyFilter = propertyFilter;
            _httpHelper = httpHelper;
        }
      

        public void Validate(Property property)
        {
            ValidationHelper.CheckIsNull(property);
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(property.Name), "Name is required.");
            ValidationHelper.CheckException(property.Price <= 0, "Price should be a positive value.");
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(property.UnitType), "Unit type is required.");
            ValidationHelper.ValidateEnum(property.Status, "Status");
            ValidationHelper.CheckException(property.PropertyManagerId == 0, "Manager is required.");
            ValidationHelper.CheckException(property.Area <= 0, "Area should be a positive value.");
        }

        public async Task<int> CreateProperty(Property property)
        {
            try
            {
                var context = new HttpContextAccessor();
                var domainKey =  context.HttpContext.Request.Headers["DomainKey"];
                Validate(property);
                property.CreatedDate = DateTime.Now;
                property.DomainKey = int.Parse(domainKey);
                db.Property.Add(property);
                await db.SaveChangesAsync();
                return property.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding property. " + ex.Message);
            }
        }

        public async Task<Property> UpdateProperty(Property property)
        {
            try
            {
                var context = new HttpContextAccessor();
                var domainKey = context.HttpContext.Request.Headers["DomainKey"];
                Validate(property);
                property.ModifiedDate = DateTime.Now;
                property.DomainKey = int.Parse(domainKey);
                db.SetStateAsModified(property);
                await db.SaveChangesAsync();
                return property;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating property. " + ex.Message);
            }
        }

        public async Task<bool> DeleteProperty(int Id)
        {
            try
            {
                Property property = db.Property.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault();

                if (property == null)
                    throw new Exception("Property does not exist.");

                property.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting property. " + ex.Message);
            }
        }

        public async Task<List<Organization>> GetOrganizationDetails(string domainKey)
        {
            return await _httpHelper.GetAsync<List<Organization>>("/api/organizationexternal/GetAllOrganizations", domainKey);
        }

        public async Task<BusinessUser> GetManagerDetails(int Id, string domainKey)
        {
            return await _httpHelper.GetAsync<BusinessUser>("/api/businessuserexternal/GetBusinessUserById/" + Id, domainKey);
        }

        public async Task<List<Property>> GetAllProperties(PropertyFilterUIModel @object)
        {
            IQueryable<Property> properties = db.Property;
            properties = _propertyFilter.ApplyFilter(properties, @object);
            List<Property> result = await properties.Skip((@object.PageIndex - 1) * @object.PageSize).Take(@object.PageSize).ToListAsync();
            return result;
        }

        public async Task<List<Property>> GetHomeDisplayProperties()
        {
            return await db.Property.Where(m => m.IsPublished && m.ArchiveDate == null).OrderByDescending(m => m.CreatedDate).Take(6).ToListAsync();
        }

        public async Task<Property> GetPropertyById(int Id)
        {
            return await db.Property.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefaultAsync();
        }
    }
}
