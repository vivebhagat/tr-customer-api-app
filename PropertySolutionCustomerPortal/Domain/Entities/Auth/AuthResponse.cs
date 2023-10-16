namespace PropertySolutionCustomerPortal.Domain.Entities.Auth
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string DataRoute { get; set; }
        public DateTime ExpireAt { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
