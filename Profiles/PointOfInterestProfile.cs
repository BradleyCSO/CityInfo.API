using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();

            // PointOfInterest has more properties than PointOfInterestCreationDto, but AutoMapper ignores these
            CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();

            CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();

            CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDto>();
        }
    }
}
