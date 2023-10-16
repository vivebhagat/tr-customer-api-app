namespace PropertySolutionCustomerPortal.Domain.Entities.Auth
{
    public class RoleAuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string DataRoute { get; set; }
        public DateTime ExpireAt { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Role { get; set; }
        public string RoleName { get; set; }
        public int Id { get; set; }
        public string EmailConfirmed { get; set; }
    }
}
