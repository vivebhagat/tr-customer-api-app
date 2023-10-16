using ContractRequestSolutionHub.Domain.Repository.Estate;
using ContractSolutionHub.Domain.Repository.Estate;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Domain.Repository.Estate;
using PropertySolutionCustomerPortal.Domain.Helper;
using PropertySolutionCustomerPortal.Domain.Repository.Auth;
using PropertySolutionCustomerPortal.Domain.Repository.Users;
using PropertySolutionCustomerPortal.Domain.EntityFilter;
using PropertySolutionCustomerPortal.Domain.Service.Email;

namespace PropertySolutionCustomerPortal.Infrastructure.DataAccess
{
    public static class AppServiceRegistry
    {
        public static void LoadServices(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<IHttpHelper, HttpHelper>();
            services.AddScoped<IAzureEmailService, AzureEmailService>();
            services.AddScoped<IPropertyFilter, PropertyFilter>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerToRoleMapRepository, CustomerToRoleMapRepository>();

            services.AddScoped<ILeaseAgreementRepository, LeaseAgreementRepository>();
            services.AddScoped<ILeaseRequestRepository, LeaseRequestRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
            services.AddScoped<IPropertyReviewRepository, PropertyReviewRepository>();

            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IContractRequestRepository, ContractRequestRepository>();
        }
    }
}
