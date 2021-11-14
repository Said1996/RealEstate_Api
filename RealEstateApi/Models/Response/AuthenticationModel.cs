using System.Collections.Generic;

namespace RealEstateApi.Models.Response
{
    public class AuthenticationModel
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string PhotoPath { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
