using Dapper;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Infrastructure.DataAccess;

namespace PropertySolutionCustomerPortal.Domain.Repository.Users
{
    public interface ICustomerToRoleMapRepository
    {
        Task<int> AddCustomerToRoleMap(CustomerToRoleMap role);
        void Validate(CustomerToRoleMap role);
    }

    public class CustomerToRoleMapRepository : ICustomerToRoleMapRepository
    {
        ILocalDbContext db;
        private readonly DapperContext _dapperContext;

        public CustomerToRoleMapRepository(ILocalDbContext db, DapperContext dapperContext)
        {
            this.db = db;
            _dapperContext = dapperContext;
        }

        public void Validate(CustomerToRoleMap role)
        {
            ValidationHelper.CheckIsNull(role);
            ValidationHelper.CheckException(role.RoleId == 0, "Role is required.");
        }

        public async Task<int> AddCustomerToRoleMap(CustomerToRoleMap role)
        {
            Validate(role);

            string insertQuery = "INSERT INTO data.CustomerToRoleMaps (CustomerId, RoleId, CreatedDate) VALUES (@CustomerId, @RoleId, @CreatedDate );";
            using var connection = _dapperContext.CreateConnection();
            return await connection.ExecuteAsync(insertQuery, role);

        }
    }
}
