using AutoMapper;
using RealEstateApi.Models;
using RealEstateApi.Models.Request;

namespace RealEstateApi.MappingConfiguration
{
    public class RealEstateProfile : Profile
    {
        public RealEstateProfile()
        {
            CreateMap<RealEstate, RealEstateModel>().ReverseMap();
        }
    }
}
