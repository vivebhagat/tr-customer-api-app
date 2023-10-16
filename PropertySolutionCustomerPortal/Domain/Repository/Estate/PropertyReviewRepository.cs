using Dapper;
using Microsoft.EntityFrameworkCore;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySolutionCustomerPortal.Domain.Repository.Estate
{
    public interface IPropertyReviewRepository
    {
        Task<int> CreatePropertyReview(PropertyReview propertyReview);
        Task<bool> DeletePropertyReview(int id);
        Task<PropertyReview> GetPropertyReviewById(int id);
        Task<List<PropertyReview>> GetAllPropertyReviews();
        Task<PropertyReview> UpdatePropertyReview(PropertyReview propertyReview);
    }

    public class PropertyReviewRepository : IPropertyReviewRepository
    {
        private readonly DapperContext _dapperContext;

        public PropertyReviewRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public void Validate(PropertyReview propertyReview)
        {
            ValidationHelper.CheckIsNull(propertyReview);
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(propertyReview.ReviewText), "Review text is required.");
        }

        public async Task<int> CreatePropertyReview(PropertyReview propertyReview)
        {
            try
            {
                Validate(propertyReview);

                string insertQuery = @"
                    INSERT INTO PropertyReviews (ReviewText, Rating, ReviewDate, PropertyId, IsVisibleToAll, CreatedDate)
                    VALUES (@ReviewText, @Rating, @ReviewDate, @PropertyId, @IsVisibleToAll, @CreatedDate);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                using var connection = _dapperContext.CreateConnection();
                return await connection.ExecuteAsync(insertQuery, propertyReview);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding property review. " + ex.Message);
            }
        }

        public async Task<PropertyReview> UpdatePropertyReview(PropertyReview propertyReview)
        {
            try
            {
                Validate(propertyReview);

                string updateQuery = @"
                    UPDATE PropertyReviews SET
                    ReviewText = @ReviewText, Rating = @Rating, ReviewDate = @ReviewDate, PropertyId = @PropertyId,
                    IsVisibleToAll = @IsVisibleToAll, ModifiedDate = @ModifiedDate
                    WHERE Id = @Id";

                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(updateQuery, propertyReview);

                return propertyReview;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating property review. " + ex.Message);
            }
        }

        public async Task<bool> DeletePropertyReview(int id)
        {
            try
            {
                string updateQuery = "UPDATE PropertyReviews SET ArchiveDate = @ArchiveDate WHERE Id = @Id";

                using var connection = _dapperContext.CreateConnection();
                int rowsAffected = await connection.ExecuteAsync(updateQuery, new { Id = id, ArchiveDate = DateTime.Now });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting property review. " + ex.Message);
            }
        }

        public async Task<List<PropertyReview>> GetAllPropertyReviews()
        {
            try
            {
                string sqlQuery = "SELECT * FROM PropertyReviews WHERE ArchiveDate IS NULL";

                using var connection = _dapperContext.CreateConnection();
                var propertyReviews = await connection.QueryAsync<PropertyReview>(sqlQuery);
                return propertyReviews.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving property reviews. " + ex.Message);
            }
        }

        public async Task<PropertyReview> GetPropertyReviewById(int id)
        {
            try
            {
                string sqlQuery = "SELECT * FROM PropertyReviews WHERE Id = @Id AND ArchiveDate IS NULL";

                using var connection = _dapperContext.CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<PropertyReview>(sqlQuery, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving property review. " + ex.Message);
            }
        }
    }
}
