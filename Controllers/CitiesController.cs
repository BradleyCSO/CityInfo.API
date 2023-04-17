using AutoMapper;
using CityInfo.API.Models.DTOs;
using CityInfo.API.Models.Responses;
using CityInfo.API.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities([FromQuery] SearchQuery searchQuery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (searchQuery.PageSize == 0){ searchQuery.PageSize = maxCitiesPageSize; }

            if (searchQuery?.TotalPageCount > maxCitiesPageSize)
            {
                searchQuery.TotalPageCount = maxCitiesPageSize;
            }

            var (cityEntities, paginationMetadata) = 
                await _cityInfoRepository.GetCitiesAsync(searchQuery);
            
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities)); // No not found as an empty collection would be a valid response body
        }

        /// <summary>
        /// Get a city by id
        /// </summary>
        /// <param name="searchQuery">The search query model built via querystring</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the requested city</response>
        [Route("GetCity")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCity([FromQuery] SearchQuery searchQuery)
        {
            var city = await _cityInfoRepository.GetCityAsync(searchQuery);

            if (city == null)
            {
                return NotFound();
            }

            if (searchQuery?.PointOfInterestQuery?.IncludePointsOfInterest ?? false)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));
        }
    }
}