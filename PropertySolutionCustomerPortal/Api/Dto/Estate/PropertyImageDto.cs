namespace PropertySolutionCustomerPortal.Api.Dto.Estate
{
    public class PropertyImageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int PropertyId { get; set; }
        public int DomainKeyId { get; set; }
    }
}
