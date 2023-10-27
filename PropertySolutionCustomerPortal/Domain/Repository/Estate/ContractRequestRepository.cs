using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Infrastructure.DataAccess;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Domain.Entities.Shared;
using PropertySolutionCustomerPortal.Application.Estate.ContractRequestComponent.Command;
using Newtonsoft.Json;
using PropertySolutionCustomerPortal.Domain.Service.Email;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using System.Linq;
using Castle.Core.Resource;

namespace ContractRequestSolutionHub.Domain.Repository.Estate
{
    public interface IContractRequestRepository
    {
        Task<int> CreateContractRequest(ContractRequest contractRequest);
        Task<int> DeleteContractRequest(int Id);
        Task<ContractRequest> GetContractRequestById(int Id);
        Task<List<ContractRequest>> GetAllContractRequests();
        Task<ContractRequest> UpdateContractRequest(ContractRequest contractRequest);
        Task<bool> AddRemoteContractRequest(string postData, string domainKey, int contractRequestId);
        Task<bool> EditRemoteContractRequest(string postData, string domainKey);
        Task<dynamic> GetCustomerId(string userId, string domainKey);
        Task<List<ContractRequest>> GetContractRequestListForUser(int Id);
        Task<bool> WithdrawContractRequest(int Id);
        Task<bool> WithdrawRemoteContractRequest(int Id, string domainKey);
        Task<bool> SendNewApplicationEmail(int contractRequestId, int customerId);
    }

    public class ContractRequestRepository : IContractRequestRepository
    {
        IMemoryCache _cache;
        ILocalDbContext db;
        IAuthDbContext _authDb;
        IHttpHelper _httpHelper;
        IAzureEmailService _azureEmailService;
        IWebHostEnvironment _environment;
        ILogger<ContractRequest> _logger;

        public ContractRequestRepository(DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb, IHttpHelper httpHelper, ILocalDbContext db, IAzureEmailService azureEmailService,
        IWebHostEnvironment environment, ILogger<ContractRequest> logger)
        {
            _cache = cache;
            _authDb = authDb;
            this.db = db;
            _httpHelper = httpHelper;
            _azureEmailService = azureEmailService;
            _environment = environment;
            _logger = logger;
        }

        public void Validate(ContractRequest contractRequest)
        {
            ValidationHelper.CheckIsNull(contractRequest);
            ValidationHelper.CheckException(contractRequest.PropertyId == 0, "Property is required.");
            ValidationHelper.CheckException(contractRequest.CustomerId == 0, "Customer is required.");
            ValidationHelper.CheckException(contractRequest.ProposedPurchasePrice <= 0, "Price is should be positive value.");
        }

        public async Task<int> CreateContractRequest(ContractRequest contractRequest)
        {

            try
            {
                Validate(contractRequest);
                bool exists = db.ContractRequests.Any(m => m.CustomerId == contractRequest.CustomerId && m.PropertyId == contractRequest.PropertyId 
                && m.Status != ContractRequestStatus.Rejected && m.Status != ContractRequestStatus.Withdraw && m.ArchiveDate == null);

                if (exists)
                    throw new Exception("You have already applied for this property.");

                contractRequest.CreatedDate = DateTime.Now;
                db.ContractRequests.Add(contractRequest);
                await db.SaveChangesAsync();
                return contractRequest.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding Request. " + ex.Message);
            }
        }

        public async Task<bool> AddRemoteContractRequest(string postData, string domainKey, int contractRequestId)
        {
            ContractRequest contractRequest = db.ContractRequests.Where(m => m.Id == contractRequestId && m.ArchiveDate == null).FirstOrDefault();
            int result = await _httpHelper.PostAsync<int>(postData, domainKey, "/api/ContractRequestExternal/AddContractRequest");

            if (result != 0)
            {
                contractRequest.RemoteId = result;
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> SendNewApplicationEmail(int contractRequestId, int customerId)
        {
            try
            {
                ContractRequest contractRequest = db.ContractRequests.Where(m => m.Id == contractRequestId && m.ArchiveDate == null).FirstOrDefault();

                if (contractRequest == null)
                    return false;

                Property property = db.Property.Where(m => m.Id == contractRequest.PropertyId && m.ArchiveDate == null).FirstOrDefault();

                if (property == null)
                    return false;

                Customer customer = db.Customers.Where(m => m.Id == customerId && m.ArchiveDate == null).FirstOrDefault();

                if (customer == null)
                    return false;

                string emailBody = LoadEmailTemplate("CusApplicationTemplate.html");

                if (string.IsNullOrEmpty(emailBody))
                    return false;

                emailBody = ReplaceTokens(emailBody, customer, property, contractRequest);

                if (string.IsNullOrEmpty(emailBody))
                    return false;

                if (!await Send(customer.Email, "New Application", emailBody))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding Request. " + ex.Message);
            }
        }

        private string ReplaceTokens(string emailBody, Customer customer, Property property, ContractRequest request)
        {
            try
            {
                emailBody = emailBody.Replace("{User}", customer.FirstName);
                emailBody = emailBody.Replace("{Name}", property.Name);
                emailBody = emailBody.Replace("{Url}", property.Url);
                emailBody = emailBody.Replace("{Price}", String.Format("{0:C}", property.Price));
                emailBody = emailBody.Replace("{Description}", property.Description);
                emailBody = emailBody.Replace("{Bedrooms}", property.Bedrooms.ToString());
                emailBody = emailBody.Replace("{Bathrooms}", property.Bathrooms.ToString());
                emailBody = emailBody.Replace("{Area}", property.Area.ToString("N0"));
                emailBody = emailBody.Replace("{Status}", request.Status.ToString());

                return emailBody;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private string LoadEmailTemplate(string templateFileName)
        {
            try
            {
                string templatePath = Path.Combine(_environment.ContentRootPath, "Files", templateFileName);
                using StreamReader reader = new(templatePath);
                return reader.ReadToEnd();
            }
            catch(Exception e)
            {
                return null;
            }
        }


        private async Task<bool> Send(string email, string subject, string emailBody)
        {
            try
            {
                var emailRequest = new EmailRequest
                {
                    RecieverAddresses = new List<string>(),
                    PrimaryRecieverAddress = email,
                    Subject = subject,
                    Body = emailBody
                };

                 var resp = await _azureEmailService.SendEmail(emailRequest);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<ContractRequest> UpdateContractRequest(ContractRequest @object)
        {
            try
            {
                Validate(@object);
                ContractRequest contractRequest = db.ContractRequests.Where(m => m.RemoteId == @object.Id && m.ArchiveDate == null).FirstOrDefault();
                contractRequest.PropertyId = @object.PropertyId;
                contractRequest.Status = @object.Status;
                contractRequest.Note = @object.Note;
                contractRequest.IsApproved = @object.IsApproved;
                contractRequest.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(contractRequest);
                await db.SaveChangesAsync();
                return contractRequest;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating request. " + ex.Message);
            }
        }

        public async Task<bool> EditRemoteContractRequest(string postData, string domainKey)
        {
            int result = await _httpHelper.PostAsync<int>(postData, domainKey, "/api/ContractRequestExternal/EditContractRequest");

            if (result != 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> WithdrawRemoteContractRequest(int Id, string domainKey)
        {
            int remoteId = db.ContractRequests.Where(m => m.Id == Id && m.ArchiveDate == null).Select(m => m.RemoteId).FirstOrDefault();

            WithdrawContractRequestCommand data = new WithdrawContractRequestCommand
            {
                Id = remoteId,
                DomainKey = domainKey,
            };

            string postData = JsonConvert.SerializeObject(data);
            bool result = await _httpHelper.PostAsync<bool>(postData, domainKey, "/api/ContractRequestExternal/WithdrawContractRequest/");

            if (result)
            {
                return true;
            }

            return false;
        }

        public async Task<int> DeleteContractRequest(int Id)
        {
            try
            {
                ContractRequest contractRequest = db.ContractRequests.Where(m => m.RemoteId == Id && m.ArchiveDate == null).FirstOrDefault();

                if (contractRequest == null)
                    throw new Exception("Request does not exist.");

                contractRequest.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return contractRequest.RemoteId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Request. " + ex.Message);
            }
        }

        public async Task<bool> WithdrawContractRequest(int Id)
        {
            try
            {
                ContractRequest contractRequest = await db.ContractRequests.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefaultAsync();
                contractRequest.Status = ContractRequestStatus.Withdraw;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating application. " + ex.Message);
            }
        }

        public async Task<dynamic> GetCustomerId(string userId, string domainKey)
        {
            var result = await _httpHelper.GetAsync<dynamic>("/api/customer/getcustomerbyuserid/" + userId, domainKey);
            return result;
        }

        public async Task<List<ContractRequest>> GetAllContractRequests()
        {
            return await db.ContractRequests.Where(m => m.ArchiveDate == null).OrderByDescending(m => m.Id).ToListAsync();
        }

        public async Task<List<ContractRequest>> GetContractRequestListForUser(int Id)
        {
            return await db.ContractRequests.Where(m => m.CustomerId == Id && m.ArchiveDate == null).OrderByDescending(m => m.CreatedDate).ToListAsync();
        }


        public async Task<ContractRequest> GetContractRequestById(int Id)
        {
            return await db.ContractRequests.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefaultAsync();
        }
    }
}
