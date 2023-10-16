using Dapper;
using Microsoft.EntityFrameworkCore;
using PropertySolutionCustomerPortal.Domain.Entities.Lease;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySolutionCustomerPortal.Domain.Repository.Estate
{
    public interface ILeaseRequestRepository
    {
        Task<int> CreateLeaseRequest(LeaseRequest leaseRequest);
        Task<LeaseRequest> UpdateLeaseRequest(LeaseRequest leaseRequest);
        Task<bool> DeleteLeaseRequest(int id);
        Task<LeaseRequest> GetLeaseRequestById(int id);
        Task<List<LeaseRequest>> GetAllLeaseRequests();
    }

    public class LeaseRequestRepository : ILeaseRequestRepository
    {
        ILocalDbContext db;
        private readonly DapperContext _dapperContext;

        public LeaseRequestRepository(DapperContext dapperContext, ILocalDbContext db)
        {
            _dapperContext = dapperContext;
            this.db = db;
        }

        public void ValidateLeaseRequest(LeaseRequest leaseRequest)
        {
            ValidationHelper.CheckIsNull(leaseRequest);
            ValidationHelper.CheckException(leaseRequest.DesiredLeaseTermMonths <= 0, "Desired Lease Term Months should be greater than zero.");
            ValidationHelper.CheckException(leaseRequest.ProposedMonthlyRent <= 0, "Proposed Monthly Rent should be gretaer than zero.");
        }

        public async Task<int> CreateLeaseRequest(LeaseRequest leaseRequest)
        {
            try
            {
                ValidateLeaseRequest(leaseRequest);

                string insertQuery = @"
                INSERT INTO LeaseRequests (PropertyId, CustomerId, RequestDate, DesiredStartDate, DesiredLeaseTermMonths, ProposedMonthlyRent, Note, IsApproved, ApprovalDate, CreatedDate)
                VALUES (@PropertyId, @CustomerId, @RequestDate, @DesiredStartDate, @DesiredLeaseTermMonths, @ProposedMonthlyRent, @Note, @IsApproved, @ApprovalDate, @CreatedDate);
                SELECT CAST(SCOPE_IDENTITY() as int);";

                using var connection = _dapperContext.CreateConnection();
                return await connection.ExecuteAsync(insertQuery, leaseRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding lease request. " + ex.Message);
            }
        }

        public async Task<LeaseRequest> UpdateLeaseRequest(LeaseRequest leaseRequest)
        {
            try
            {
                ValidateLeaseRequest(leaseRequest);

                string updateQuery = @"
                    UPDATE LeaseRequests SET
                    PropertyId = @PropertyId, CustomerId = @CustomerId, RequestDate = @RequestDate, DesiredStartDate = @DesiredStartDate,
                    DesiredLeaseTermMonths = @DesiredLeaseTermMonths, ProposedMonthlyRent = @ProposedMonthlyRent, Note = @Note,
                    IsApproved = @IsApproved, ApprovalDate = @ApprovalDate, ModifiedDate = @ModifiedDate
                    WHERE Id = @Id";

                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(updateQuery, leaseRequest);

                return leaseRequest;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating lease request. " + ex.Message);
            }
        }

        public async Task<bool> DeleteLeaseRequest(int id)
        {
            try
            {
                string updateQuery = "UPDATE LeaseRequests SET ArchiveDate = @ArchiveDate WHERE Id = @Id";

                using var connection = _dapperContext.CreateConnection();
                int rowsAffected = await connection.ExecuteAsync(updateQuery, new { Id = id, ArchiveDate = DateTime.Now });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting lease request. " + ex.Message);
            }
        }

        public async Task<LeaseRequest> GetLeaseRequestById(int id)
        {
            try
            {
                string sqlQuery = "SELECT * FROM LeaseRequests WHERE Id = @Id AND ArchiveDate IS NULL";

                using var connection = _dapperContext.CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<LeaseRequest>(sqlQuery, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving lease request. " + ex.Message);
            }
        }

        public async Task<List<LeaseRequest>> GetAllLeaseRequests()
        {
            return await db.LeaseRequests.Where(m => m.ArchiveDate == null).ToListAsync();
        }
    }
}
