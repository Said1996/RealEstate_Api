using RealEstateApi.Models.Request;
using System.Collections.Generic;

namespace RealEstateApi.Models.Response
{
    public class UserModel
    {
        public string FullName { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoPath { get; set; }
        public ICollection<RealEstateModel> UserRealEstatesOffer { get; set; }
    }
}
