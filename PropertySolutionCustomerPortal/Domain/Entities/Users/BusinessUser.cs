using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using PropertySolutionCustomerPortal.Domain.Entities.Shared;

namespace PropertySolutionCustomerPortal.Domain.Entities.Users
{
    public class BusinessUser : IBaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }

        public string ZipCode { get; set; }
        public string Url { get; set; }

        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
