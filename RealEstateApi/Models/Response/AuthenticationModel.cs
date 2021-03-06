using System;
using System.Collections.Generic;

namespace RealEstateApi.Models.Response
{
    public class AuthenticationModel
    {
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
        public string FullName { get; set; }
        public string PhotoPath { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
