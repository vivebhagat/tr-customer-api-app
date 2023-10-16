namespace PropertySolutionCustomerPortal.Domain.Entities.Shared
{
    public class BaseApplicationUserToDomainKeyMap : IBaseEntity
    {
        public int Id { get; set; }
        public string BaseApplicationUserId { get; set; }
        public virtual DomainKey DomainKey { get; set; }
        public int DomainKeyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
