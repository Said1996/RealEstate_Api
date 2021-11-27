using System.ComponentModel.DataAnnotations;

namespace RealEstateApi.Models.Request
{
    public class RegisterModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

}
