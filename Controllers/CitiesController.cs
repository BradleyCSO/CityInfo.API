using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController] // Not necessary but improves development experience when building APIs, this should be documented as we go on
    [Route("api/cities")]
    public class CitiesController : ControllerBase // Using controller base here as a derived class. If we inherited from the Controller class, it has additional helper methods for when returning views1q
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, 
            IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities()
        {
            var cityEntities = await _cityInfoRepository.GetCitiesAsync();
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));

            //var results = new List<CityWithoutPointsOfInterestDto>();

            //foreach (var cityEntity in cityEntities)
            //{
            //    results.Add(new CityWithoutPointsOfInterestDto
            //    {
            //        Id = cityEntity.Id,
            //        Name = cityEntity.Name,
            //        Description = cityEntity.Description,
            //    });
            //}

            //return Ok(results);
            //return Ok(_citiesDataStore.Cities); // No not found as an empty collection would be a valid response body
        }

        //[HttpGet("{id}")]
        //public ActionResult<CityDto> GetCity(int id)
        //{
        //    //var cityToReturn = _citiesDataStore.Cities.FirstOrDefault(get => get.Id == id);

        //    //if (cityToReturn == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //return Ok(cityToReturn);
        //}
    }
}
