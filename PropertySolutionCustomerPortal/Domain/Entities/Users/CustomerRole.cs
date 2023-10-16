using PropertySolutionCustomerPortal.Domain.Entities.Shared;

namespace PropertySolutionCustomerPortal.Domain.Entities.Users
{
    public class CustomerRole : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
