using PropertySolutionCustomerPortal.Domain.Entities.Shared;

namespace PropertySolutionCustomerPortal.Domain.Entities.Users
{
    public class CustomerToRoleMap : IBaseEntity
    {
        public int Id { get; set; }
        public virtual Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public virtual CustomerRole Role { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
