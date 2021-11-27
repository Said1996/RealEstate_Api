using RealEstateApi.Models.Enums;

namespace RealEstateApi.Models.Response
{
    public class RealEstateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public string Description { get; set; }

        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public int Price { get; set; }
        public double Space { get; set; }

        public int BedroomNum { get; set; }
        public int BathroomNum { get; set; }
        public int KitchenNum { get; set; }


        public PropertyType PropertyType { get; set; }
        public OfferType OfferType { get; set; }

        public bool SwimmingPool { get; set; }
        public bool SecuritySystem { get; set; }
        public bool Garden { get; set; }

        public string UserName { get; set; }
        public string UserPhotoPath { get; set; }
        public string UserPhoneNumber { get; set; }

    }
}
