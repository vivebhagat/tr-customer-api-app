namespace PropertySolutionCustomerPortal.Domain.Entities.Auth
{
    public class RefreshToken
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set;}
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
