using PropertySolutionCustomerPortal.Domain.Entities.Shared;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Domain.Entities.Estate
{
    public class Contract : IBaseEntity
    {
        public int Id { get; set; }
        public int RemoteId { get; set; }
        public virtual Property Property { get; set; }
        public int PropertyId { get; set; }
        public virtual Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public double PurchasePrice { get; set; }
        public ContractStatus Status { get; set; }
        public DateTime SaleDate { get; set; }
        public int? DomainKey { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }

    }
}
