using PropertySolutionCustomerPortal.Domain.Entities.Shared;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Domain.Entities.Estate
{
    public class ContractRequest : IBaseEntity
    {
        public int Id { get; set; }
        public int RemoteId { get; set; }
        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }
        public virtual Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime RequestDate { get; set; }
        public decimal ProposedPurchasePrice { get; set; }
        public ContractRequestStatus Status { get; set; }

        public string Note { get; set; }
        public bool IsApproved { get; set; }
        public int? DomainKey { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
