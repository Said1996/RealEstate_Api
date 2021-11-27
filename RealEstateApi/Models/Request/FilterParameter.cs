using RealEstateApi.Models.Enums;

namespace RealEstateApi.Models.Request
{
    public class FilterParameter
    {
        public string Search { get; set; }

        public int? Price { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public double? MaxSpace { get; set; }
        public double? MinSpace { get; set; }

        public int? KitchenNum { get; set; }
        public int? BedroomNum { get; set; }
        public int? BathroomNum { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }

        public PropertyType? PropertyType { get; set; }
        public OfferType? OfferType { get; set; }

        public bool? SwimmingPool { get; set; }
        public bool? SecuritySystem { get; set; }
        public bool? Garden { get; set; }



    }
}
