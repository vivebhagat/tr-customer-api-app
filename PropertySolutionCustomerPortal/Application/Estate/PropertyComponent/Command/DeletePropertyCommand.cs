using MediatR;


namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command
{
    public class DeletePropertyCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
