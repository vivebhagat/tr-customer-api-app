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
    public interface ILeaseAgreementRepository
    {
        Task<int> CreateLeaseAgreement(LeaseAgreement leaseAgreement);
        Task<LeaseAgreement> UpdateLeaseAgreement(LeaseAgreement leaseAgreement);
        Task<bool> DeleteLeaseAgreement(int id);
        Task<LeaseAgreement> GetLeaseAgreementById(int id);
        Task<List<LeaseAgreement>> GetAllLeaseAgreements();
    }

    public class LeaseAgreementRepository : ILeaseAgreementRepository
    {
        ILocalDbContext db;
        private readonly DapperContext _dapperContext;

        public LeaseAgreementRepository(DapperContext dapperContext, ILocalDbContext db)
        {
            _dapperContext = dapperContext;
            this.db = db;
        }

        public void Valdiate(LeaseAgreement leaseAggreement)
        {
            ValidationHelper.CheckIsNull(leaseAggreement);
            ValidationHelper.CheckException(leaseAggreement.PropertyId == 0, "Property is required");
            ValidationHelper.CheckException(leaseAggreement.CustomerId == 0, "Customer is required");
            ValidationHelper.CheckException(leaseAggreement.LeaseTermMonths == 0, "Lease term months should be gretaer than zero.");
            ValidationHelper.CheckException(leaseAggreement.Amount <= 0, "Amount should be a positive value.");
        }

        public async Task<int> CreateLeaseAgreement(LeaseAgreement leaseAgreement)
        {
            try
            {
                Valdiate(leaseAgreement);

                string insertQuery = @"
                INSERT INTO LeaseAgreements (PropertyId, CustomerId, LeaseStartDate, LeaseTermMonths, Amount, IsRenewable, RenewedDate, IsApproved, IsTerminated, TerminationReason, TerminationDate, CreatedDate)
                VALUES (@PropertyId, @CustomerId, @LeaseStartDate, @LeaseTermMonths, @Amount, @IsRenewable, @RenewedDate, @IsApproved, @IsTerminated, @TerminationReason, @TerminationDate, @CreatedDate);
                SELECT CAST(SCOPE_IDENTITY() as int);";

                using var connection = _dapperContext.CreateConnection();
                return await connection.ExecuteAsync(insertQuery, leaseAgreement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding lease agreement. " + ex.Message);
            }
        }

        public async Task<LeaseAgreement> UpdateLeaseAgreement(LeaseAgreement leaseAgreement)
        {
            try
            {
                Valdiate(leaseAgreement);

                string updateQuery = @"
                    UPDATE LeaseAgreements SET
                    PropertyId = @PropertyId, CustomerId = @CustomerId, LeaseStartDate = @LeaseStartDate, LeaseTermMonths = @LeaseTermMonths,
                    Amount = @Amount, IsRenewable = @IsRenewable, RenewedDate = @RenewedDate, IsApproved = @IsApproved, IsTerminated = @IsTerminated,
                    TerminationReason = @TerminationReason, TerminationDate = @TerminationDate, ModifiedDate = @ModifiedDate
                    WHERE Id = @Id";

                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(updateQuery, leaseAgreement);

                return leaseAgreement;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating lease agreement. " + ex.Message);
            }
        }

        public async Task<bool> DeleteLeaseAgreement(int id)
        {
            try
            {
                string updateQuery = "UPDATE LeaseAgreements SET ArchiveDate = @ArchiveDate WHERE Id = @Id";

                using var connection = _dapperContext.CreateConnection();
                int rowsAffected = await connection.ExecuteAsync(updateQuery, new { Id = id, ArchiveDate = DateTime.Now });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting lease agreement. " + ex.Message);
            }
        }

        public async Task<LeaseAgreement> GetLeaseAgreementById(int id)
        {
            try
            {
                string sqlQuery = "SELECT * FROM LeaseAgreements WHERE Id = @Id AND ArchiveDate IS NULL";

                using var connection = _dapperContext.CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<LeaseAgreement>(sqlQuery, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving lease agreement. " + ex.Message);
            }
        }

        public async Task<List<LeaseAgreement>> GetAllLeaseAgreements()
        {
            return await db.Lease.Where(m => m.ArchiveDate == null).ToListAsync();
        }
    }
}
