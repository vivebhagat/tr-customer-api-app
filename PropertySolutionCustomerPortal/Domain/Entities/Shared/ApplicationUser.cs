using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertySolutionCustomerPortal.Domain.Entities.Shared
{

    public class ApplicationUser : IdentityUser
    {
        public string DataRoute { get; set; }
        public string Password { get; set; }
    }
}
