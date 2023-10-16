
using PropertySolutionCustomerPortal.Domain.Entities.Estate;

namespace PropertySolutionCustomerPortal.Api.Dto.Estate
{
    public class ContractRequestDto
    {
        public int Id { get; set; }
        public int RemoteId { get; set; }
        public int PropertyId { get; set; }
        public int CustomerId { get; set; }
        public decimal ProposedPurchasePrice { get; set; }
        public ContractStatus Status { get; set; }
        public string Note { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}
