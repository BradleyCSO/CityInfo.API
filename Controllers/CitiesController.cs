using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Models.DTOs;
using CityInfo.API.Models.Responses;
using CityInfo.API.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CityInfo.API.Controllers
{
    [ApiController] // Not necessary but improves development experience when building APIs, this should be documented as we go on
    //[Authorize]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/cities")]
    public class CitiesController : ControllerBase // Using controller base here as a derived class. If we inherited from the Controller class, it has additional helper methods for when returning views1q
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 2;

        public CitiesController(ICityInfoRepository cityInfoRepository, 
            IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("GetContinents")]
        [HttpGet]
        public async Task<IActionResult> GetContinentsAsync()
        {
            var continents = await _cityInfoRepository.GetContinentsAsync();

            if (continents == null)
            {
                return NotFound();
            }

            return Ok(continents);
        }

        [Route("GetCountries")]
        [HttpGet]
        public async Task<IActionResult> GetCountriesAsync()
        {
            var countries = await _cityInfoRepository.GetCountriesAsync();

            if (countries == null)
            {
                return NotFound();
            }

            return Ok(countries);
        }


        // Route same as [Route("api/v{version:apiVersion}/cities")]
        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities([ModelBinder(BinderType = typeof(SearchQueryBinder))] SearchQuery searchQuery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (searchQuery.TotalPageCount > maxCitiesPageSize)
            {
                searchQuery.TotalPageCount = maxCitiesPageSize;
            }

            var (cityEntities, paginationMetadata) = 
                await _cityInfoRepository.GetCitiesAsync(searchQuery);
            
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

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

        /// <summary>
        /// Get a city by id
        /// </summary>
        /// <param name="id">The id of the city to get</param>
        /// <param name="includePointsOfInterest">Whether or not to include the points of interest</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the requested city</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city)); // Map cities WITH points of interest
            }

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city)); // Otherwise map cities without points of interest

            //var cityToReturn = _citiesDataStore.Cities.FirstOrDefault(get => get.Id == id);

            //if (cityToReturn == null)
            //{
            //    return NotFound();
            //}

            //return Ok(cityToReturn);
        }
    }
}
