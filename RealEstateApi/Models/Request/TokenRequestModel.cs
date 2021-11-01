using System.ComponentModel.DataAnnotations;

namespace RealEstateApi.Models.Request
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
