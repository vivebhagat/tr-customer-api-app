using PropertySolutionCustomerPortal.Domain.Entities.Shared;

namespace PropertySolutionCustomerPortal.Domain.Entities.Estate
{
    public class PropertyReview : IBaseEntity
    {
        public int Id { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        public virtual Property Property { get; set; }
        public int PropertyId { get; set; }
        public int DomainKey { get; set; }
        public bool IsVisibleToAll { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
