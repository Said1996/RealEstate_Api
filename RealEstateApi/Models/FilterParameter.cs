using RealEstateApi.Models.Enums;

namespace RealEstateApi.Models
{
    public class FilterParameter
    {
        public string QueryName {  get; set; }

        public int? Price { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public double? MaxSpace { get; set; }
        public double? MinSpace { get; set; }

        public int? KitchenCount { get; set; }
        public int? RoomCount {  get; set; }
        public int? BathroomCount {  get; set; }

        public City? City { get; set; }
        public PropertyType? PropertyType { get; set; }
        public OfferType? OfferType { get; set; }

        public bool? SwimmingPool { get; set; }
        public bool? SecuritySystem { get; set; }
        public bool? Garden { get; set; }

    }
}
