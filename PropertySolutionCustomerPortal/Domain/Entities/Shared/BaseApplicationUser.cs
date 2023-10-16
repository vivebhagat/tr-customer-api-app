namespace PropertySolutionCustomerPortal.Domain.Entities.Shared
{
    public class BaseApplicationUser : ApplicationUser
    {
        public virtual BaseApplicationUserType BaseApplicationUserType { get; set; }
        public int BaseApplicationUserTypeId { get; set; }

    }
}
