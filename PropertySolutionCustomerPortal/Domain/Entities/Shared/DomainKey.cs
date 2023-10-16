namespace PropertySolutionCustomerPortal.Domain.Entities.Shared
{
    public class DomainKey : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubDomain { get; set; }
        public string ConnectionString { get; set; }
        public int Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }

}