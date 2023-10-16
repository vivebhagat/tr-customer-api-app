namespace PropertySolutionCustomerPortal.Domain.Service.Email
{
    public class EmailRequest
    {
        public string Source { get; set; }
        public string PrimaryRecieverAddress { get; set; }
        public List<string> RecieverAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
