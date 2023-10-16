namespace PropertySolutionCustomerPortal.Domain.Helper
{
    public interface IAppSettingsHelper
    {
        string GetAppSettingValue(string key);
    }

    public class AppSettingsHelper : IAppSettingsHelper
    {
        private readonly IConfiguration _configuration;

        public AppSettingsHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetAppSettingValue(string key)
        {
            return _configuration[key];
        }
    }
}
