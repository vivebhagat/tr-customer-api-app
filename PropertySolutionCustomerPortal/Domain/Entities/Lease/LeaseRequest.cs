using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Shared;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Domain.Entities.Lease
{
    public class LeaseRequest : IBaseEntity
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }
        public int CustomerId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime DesiredStartDate { get; set; }
        public int DesiredLeaseTermMonths { get; set; }
        public double ProposedMonthlyRent { get; set; }
        public string Note { get; set; }
        public bool IsApproved { get; set; }
        public int DomainKey { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
