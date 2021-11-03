using RealEstateApi.Models.Enums;

namespace RealEstateApi.Models
{
    public class RealEstate
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int? Price { get; set; }
        public double? Space { get; set; }

        public int? BedroomNum { get; set; }
        public int? BathroomNum { get; set; }
        public int? KitchenNum { get; set; }

        public City? City { get; set; }
        public PropertyType? PropertyType { get; set; }
        public OfferType? OfferType { get; set; }

        public bool? SwimmingPool { get; set; }
        public bool? SecuritySystem { get; set; }
        public bool? Garden { get; set; }



    }
}
