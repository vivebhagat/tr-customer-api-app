namespace PropertySolutionCustomerPortal.Domain.Entities.Shared
{
    public class BaseApplicationUserType : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
