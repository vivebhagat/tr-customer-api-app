namespace PropertySolutionCustomerPortal.Api.Dto.Estate
{
    public class PropertyReviewDto
    {
        public int Id { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        public int PropertyId { get; set; }
        public bool IsVisibleToAll { get; set; }
    }
}
