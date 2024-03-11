using AutoMapper;
using CityInfo.API.Models.DTOs;

namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest, PointOfInterestDto>();

            // PointOfInterest has more properties than PointOfInterestCreationDto, but AutoMapper ignores these
            CreateMap<PointOfInterestForCreationDto, Entities.PointOfInterest>();

            CreateMap<PointOfInterestForUpdateDto, Entities.PointOfInterest>();

            CreateMap<Entities.PointOfInterest, PointOfInterestForUpdateDto>();
        }
    }
}
