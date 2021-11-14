using Microsoft.AspNetCore.Identity;

namespace RealEstateApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string PhotoPath { get; set; }

    }
}
