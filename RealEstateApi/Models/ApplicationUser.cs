using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RealEstateApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string PhotoPath { get; set; }

        [JsonIgnore]
        public ICollection<RealEstate> RealEstates { get; set; }

    }
}
