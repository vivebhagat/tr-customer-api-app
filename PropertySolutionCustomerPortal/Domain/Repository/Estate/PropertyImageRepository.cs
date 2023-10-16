using Dapper;
using Microsoft.EntityFrameworkCore;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Shared;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySolutionCustomerPortal.Domain.Repository.Estate
{
    public interface IPropertyImageRepository
    {
        Task<int> CreatePropertyImage(PropertyImage propertyImage);
        Task<bool> DeletePropertyImage(int id);
        Task<List<PropertyImage>> GetPropertyImageListByPropertyId(int Id);
        Task<List<PropertyImage>> GetAllPropertyImages();
        Task<PropertyImage> UpdatePropertyImage(PropertyImage propertyImage);
    }

    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly DapperContext _dapperContext;
        ILocalDbContext _db;
        IHttpHelper _httpHelper;

        public PropertyImageRepository(DapperContext dapperContext , ILocalDbContext db, IHttpHelper httpHelper)
        {
            _dapperContext = dapperContext;
            _db = db;
            _httpHelper = httpHelper;
        }

        public void Validate(PropertyImage propertyImage)
        {
            ValidationHelper.CheckIsNull(propertyImage);
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(propertyImage.Name), "Image name is required.");
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(propertyImage.ImageUrl), "Image URL is required.");
        }

        public async Task<int> CreatePropertyImage(PropertyImage propertyImage)
        {
            try
            {
                Validate(propertyImage);

                string insertQuery = @"
                    INSERT INTO PropertyImages (Name, ImageUrl, PropertyId, CreatedDate)
                    VALUES (@Name, @ImageUrl, @PropertyId, @CreatedDate);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                using var connection = _dapperContext.CreateConnection();
                return await connection.ExecuteAsync(insertQuery, propertyImage);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding property image. " + ex.Message);
            }
        }

        public async Task<PropertyImage> UpdatePropertyImage(PropertyImage propertyImage)
        {
            try
            {
                Validate(propertyImage);

                string updateQuery = @"
                    UPDATE PropertyImages SET
                    Name = @Name, ImageUrl = @ImageUrl, PropertyId = @PropertyId, ModifiedDate = @ModifiedDate
                    WHERE Id = @Id";

                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(updateQuery, propertyImage);

                return propertyImage;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating property image. " + ex.Message);
            }
        }

        public async Task<bool> DeletePropertyImage(int id)
        {
            try
            {
                string updateQuery = "UPDATE PropertyImages SET ArchiveDate = @ArchiveDate WHERE Id = @Id";

                using var connection = _dapperContext.CreateConnection();
                int rowsAffected = await connection.ExecuteAsync(updateQuery, new { Id = id, ArchiveDate = DateTime.Now });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting property image. " + ex.Message);
            }
        }

        public async Task<List<PropertyImage>> GetAllPropertyImages()
        {
            try
            {
                string sqlQuery = "SELECT * FROM PropertyImages WHERE ArchiveDate IS NULL";

                using var connection = _dapperContext.CreateConnection();
                var propertyImages = await connection.QueryAsync<PropertyImage>(sqlQuery);
                return propertyImages.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving property images. " + ex.Message);
            }
        }

        public async Task<List<PropertyImage>> GetPropertyImageListByPropertyId(int Id)
        {
            try
            {
                int domainKey = _db.Property.Where(m => m.Id == Id && m.ArchiveDate == null).Select(m => m.DomainKey).FirstOrDefault();

                return await _httpHelper.GetAsync<List<PropertyImage>>("/api/PropertyImageExternal/GetPropertyImages/" + Id, domainKey.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving property image. " + ex.Message);
            }
        }
    }
}
