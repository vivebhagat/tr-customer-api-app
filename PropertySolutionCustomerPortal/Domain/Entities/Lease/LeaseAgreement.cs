using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Shared;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Domain.Entities.Lease
{
    public class LeaseAgreement : IBaseEntity
    {
        public int Id { get; set; }
        public virtual Property Property { get; set; }
        public int PropertyId { get; set; }
        public int CustomerId { get; set; }
        public DateTime LeaseStartDate { get; set; }
        public int LeaseTermMonths { get; set; }
        public double Amount { get; set; }
        public bool IsRenewable { get; set; }
        public DateTime? RenewedDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsTerminated { get; set; }
        public string TerminationReason { get; set; }
        public int DomainKey { get; set; }
        public DateTime? TerminationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
