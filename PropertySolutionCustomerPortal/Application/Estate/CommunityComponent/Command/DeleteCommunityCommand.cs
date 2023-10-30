using MediatR;


namespace PropertySolutionCustomerPortal.Application.Estate.PropertyComponent.Command
{
    public class DeleteCommunityCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
