using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PropertySolutionCustomerPortal.Infrastructure.Attribute
{
    public class ExternalAuthFilterAttribute : TypeFilterAttribute
    {
        public ExternalAuthFilterAttribute() : base(typeof(ExternalAuthFilter))
        {
        }
    }

    public class ExternalAuthFilter : IAuthorizationFilter
    {
        private readonly string _expectedApiKey = string.Empty;
        private readonly IConfiguration _configuration;

        public ExternalAuthFilter(IConfiguration configuration)
        {
            _configuration = configuration;
            _expectedApiKey = _configuration["AuthKey"]; // "322cb423-3e7d-416e-9c5b-8c515aadb12e";
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (!context.HttpContext.Request.Headers.TryGetValue("AuthKey", out var apiKeyValue))
            {
                context.HttpContext.Response.StatusCode = 401;
                context.HttpContext.Response.WriteAsync("Auth Key not found. Unauthorized access.");
                return;
            }

            if (string.IsNullOrEmpty(_expectedApiKey) || string.IsNullOrEmpty(apiKeyValue) || !apiKeyValue.Equals(_expectedApiKey))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
