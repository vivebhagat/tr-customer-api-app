using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;
using PropertySolutionCustomerPortal.Domain.Entities.Shared;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Infrastructure.DataAccess;
using PropertySolutionCustomerPortal.Domain.Repository.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace PropertySolutionCustomerPortal.Domain.Repository.Users
{
    public interface ICustomerRepository : IDynamicDbRepository
    {
        Task<int> CreateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int customerId);
        Task<Customer> GetCustomerById(int customerId);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> UpdateCustomer(Customer customer);
        Task<RoleAuthResponse> Login(string username, string password);
        Task<RoleAuthResponse> RoleSelect(string role, string refreshToken);
        Task<bool> Logout(string userId);
        Task<List<CustomerToRoleMap>> GetCustomerRoleByUserId(string userId);
        Task<Customer> GetCustomerByUserId(string userId);
        Task<bool> EmailConfirmation(EmailConfirmation emailConfirmation);
        Task<bool> ResetPassword(ResetPassword resetPassword);
        Task<bool> ForgotPasswowrd(string email);

    }

    public class CustomerRepository : AbstractDynamicDbRepository, ICustomerRepository
    {
        IMemoryCache _cache;
        IAuthRepository _authRepository;
        private readonly UserManager<BaseApplicationUser> _userManager;
        IAuthDbContext authDb;
        ILocalDbContext db;

        public CustomerRepository(IAuthRepository authRepository, ILocalDbContext db, UserManager<BaseApplicationUser> userManager, IAuthDbContext authDb, IMemoryCache cache) : base(cache, authDb)
        {
            _authRepository = authRepository;
            this.db = db;
            _userManager = userManager;
            this.authDb = authDb;
        }

        public void Validate(Customer customer)
        {
            ValidationHelper.CheckIsNull(customer);
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(customer.FirstName), "First name is required.");
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(customer.LastName), "Last name is required.");
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(customer.UserName), "User name is required.");

            ValidationHelper.ValidateEmail(customer.Email);

            if (!string.IsNullOrWhiteSpace(customer.Password))
                ValidationHelper.ValidatePasswordFormat(customer.Password);
        }



        public async Task<bool> ForgotPasswowrd(string email)
        {
            return await _authRepository.ForgotPassword(email);
        }


        public async Task<bool> EmailConfirmation(EmailConfirmation emailConfirmation)
        {
           return  await _authRepository.EmailConfirmation(emailConfirmation);
        }

        public async Task<bool> ResetPassword(ResetPassword resetPassword)
        {
            return await _authRepository.ResetPassword(resetPassword);
        }

        public async Task<RoleAuthResponse> Login(string username, string password)
        {
            try
            {
                BaseApplicationUser user = await ValidateUser(username, password);
                    
                if(user == null) return null;

                if(user.BaseApplicationUserTypeId != 0 && user.BaseApplicationUserType.Name.ToLower() != "customer") return null;

                string accessToken = _authRepository.GenerateJwtToken(user, "Customer");
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
                string dataroute = jwt.Claims.First(c => c.Type == "DataRoute").Value;
                string emailConfirmed = jwt.Claims.First(c => c.Type == "EmailConfirmed").Value;
                var expireat = jwt.ValidTo;

                RefreshToken refToken = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Token = accessToken,
                    UserName = user.UserName,
                    UserId = user.Id,
                    IssuedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddHours(2)
                };

                bool result = await _authRepository.AddRefreshToken(refToken);

                if (result)
                {

                    return new RoleAuthResponse
                    {
                        AccessToken = accessToken,
                        RefreshToken = refToken.Id,
                        DataRoute = dataroute,
                        ExpireAt = expireat,
                        UserId = user.Id,
                        UserName = user.UserName,
                        EmailConfirmed = emailConfirmed,
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<BaseApplicationUser> ValidateUser(string userName, string password)
        {

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return null;

            //if (!await _userManager.IsEmailConfirmedAsync(user))
              //  return null;

            if (await _userManager.CheckPasswordAsync(user, password))
                return user;

            return null;
        }

        private List<string> GetCustomerRoles(string userId)
        {
            try
            {
                var roles = db.CustomerToRoleMaps
                    .Where(m => m.Customer.UserId == userId && m.ArchiveDate == null)
                    .Select(m => m.Role.Name)
                    .ToList();

                return roles;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<RoleAuthResponse> RoleSelect(string role, string refreshToken)
        {
            try
            {
                RefreshToken existingRefreshToken = authDb.RefreshTokens.Where(m => m.Id == refreshToken).FirstOrDefault();

                if (existingRefreshToken == null)
                    return null;

                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(existingRefreshToken.Token);
                string dataroute = jwt.Claims.First(c => c.Type == "DataRoute").Value;
                string emailConfirmed = jwt.Claims.First(c => c.Type == "EmailConfirmed").Value;
                DateTime expireat = jwt.ValidTo;
                string userId = jwt.Claims.First(c => c.Type == "sub").Value;
                string userName = jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value;
                CustomerToRoleMap customerToRoleMap = db.CustomerToRoleMaps.Where(m => m.Customer.UserId == userId && m.Role.Name == role).FirstOrDefault();

                BaseApplicationUser user = new()
                {
                    UserName = userName,
                    Id = userId,
                    DataRoute = dataroute

                };

                if (customerToRoleMap == null)
                    return null;

                string accessToken = _authRepository.GenerateJwtToken(user, customerToRoleMap.Role.Id.ToString());

                RefreshToken refToken = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Token = accessToken,
                    UserName = user.UserName,
                    UserId = user.Id,
                    IssuedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddHours(2)
                };

                bool result = await _authRepository.AddRefreshToken(refToken);

                if (result)
                {
                    return new RoleAuthResponse
                    {
                        AccessToken = accessToken,
                        RefreshToken = refToken.Id,
                        DataRoute = dataroute,
                        ExpireAt = expireat,
                        UserId = user.Id,
                        UserName = user.UserName,
                        Role = customerToRoleMap.RoleId,
                        RoleName = customerToRoleMap.Role.Name,
                        Id = customerToRoleMap.CustomerId,
                        EmailConfirmed = emailConfirmed
                    };
                }

                return null;
            }
            catch (Exception e)
            {
                return null;

            }
       }

        public async Task<bool> Logout(string userId)
        {
            try
            {
                return await _authRepository.RemoveRefreshToken(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> CreateCustomer(Customer customer)
        {
            try
            {
                Validate(customer);

                BaseApplicationUser applicationUser = new()
                {
                    UserName = customer.UserName,
                    Email = customer.Email,
                    Password = customer.Password,
                    DataRoute = "Customer",
                    BaseApplicationUserTypeId = 2,

                };

                string result = await _authRepository.RegisterUser(applicationUser);

                customer.UserId = result.ToString();
                customer.CreatedDate = DateTime.Now;

                db.Customers.Add(customer);
                db.SaveChanges();

                CustomerToRoleMap customerToRoleMap = new()
                {
                    RoleId = 1,
                    CustomerId = customer.Id
                };

                db.CustomerToRoleMaps.Add(customerToRoleMap);
                db.SaveChanges();

                return customer.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding customer. " + ex.Message);
            }
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            try
            {
                Validate(customer);

                BaseApplicationUser commonApplicationUser = new BaseApplicationUser
                {
                    UserName = customer.UserName,
                    Email = customer.Email,
                    Password = customer.Password,
                };

                await _authRepository.UpdateUser(commonApplicationUser, customer.UserId);
                customer.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(customer);
                db.SaveChanges();

                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating customer. " + ex.Message);
            }
        }

        public async Task<bool> DeleteCustomer(int customerId)
        {
            try
            {
                Customer customer = db.Customers.Where(m => m.Id == customerId && m.ArchiveDate == null).FirstOrDefault() ?? throw new Exception("Customer details not found.");
                customer.ModifiedDate = DateTime.Now;
                customer.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting customer. " + ex.Message);
            }
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await db.Customers.Where(m => m.ArchiveDate == null).ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await db.Customers.Where(m => m.Id == customerId && m.ArchiveDate == null).FirstOrDefaultAsync();
        }


        public async Task<Customer> GetCustomerByUserId(string userId)
        {
            return await db.Customers.Where(m => m.UserId == userId && m.ArchiveDate == null).FirstOrDefaultAsync();
        }

        public async Task<List<CustomerToRoleMap>> GetCustomerRoleByUserId(string userId)
        {
            try
            {
                int customerId = db.Customers.Where(m => m.UserId == userId && m.ArchiveDate == null).Select(m => m.Id).FirstOrDefault();
                List<CustomerToRoleMap> roles = await db.CustomerToRoleMaps.Where(m => m.CustomerId == customerId && m.ArchiveDate == null).ToListAsync();
                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving customer roles. " + ex.Message);
            }
        }
    }
}
