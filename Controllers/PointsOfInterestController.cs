using AutoMapper;
using CityInfo.API.Models.DTOs;
using CityInfo.API.Models.Responses;
using CityInfo.API.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/cities/{cityId}/pointsofinterest")] // All class members have this initial route path
    //[Authorize(Policy = "MustBeFromLondon")]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _localMailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService localMailService,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(SearchQuery searchQuery)
        {
            //var cityName = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value; // ControllerBase exposes User (Claims)

            //if (!await _cityInfoRepository.CityNameMatchesCityId(cityId, cityName))
            //{
            //    return Forbid(); // Returns 403: User is authenticated but doesn't have access
            //}

            if (!await _cityInfoRepository.CityExistsAsync(searchQuery))
            {
                _logger.LogInformation($"City with ID {searchQuery.CityQuery.Id} wasn't found when accessing points of interest.");
                return NotFound(); // In this case we should return a 404 because if the city passed within cityId doesn't exist, because the URI itself wouldn't be pointing to a mapped resource
            }

            var pointsOfInterestForCity =
                await _cityInfoRepository.GetPointsOfInterestForCityAsync(searchQuery);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
        }

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(SearchQuery searchQuery)
        {
            if (!await _cityInfoRepository.CityExistsAsync(searchQuery))
            {
                return NotFound();
            }

            var pointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(searchQuery);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
        }

        // Here we are creating a new db entry hence the need for PointOfInterestCreationDto/DB object
        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(SearchQuery searchQuery)
        {
            // For when a user sends a request for a resource URI that does not exist/trying to add a
            // Point of interest for a city that does not exist
            if (!await _cityInfoRepository.CityExistsAsync(searchQuery))
            {
                return NotFound();
            }

            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(searchQuery?.PointOfInterestQuery?.PointOfInterestForCreation);

            await _cityInfoRepository.AddPointOfInterestForCityAsync(searchQuery);

            await _cityInfoRepository.SaveChangesAsync();

            var createdPointOfInterestToReturn = _mapper.Map<PointOfInterestDto>(finalPointOfInterest); // Persist the changes, should now have foreign keys and an id

            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = searchQuery?.CityQuery?.Id,
                    pointOfInterestId = createdPointOfInterestToReturn?.Id
                }, createdPointOfInterestToReturn); // Add to response body

            //    city.PointsOfInterest.Add(finalPointOfInterest);

            //    return CreatedAtRoute("GetPointOfInterest", new
            //    {
            //        cityId = cityId,
            //        pointOfInterestId = finalPointOfInterest.Id
            //    },
            //    finalPointOfInterest // Add to response body
            //    );
            //}
        }

        // Here we are creating a new db entry hence the need for PointOfInterestForUpdateDto/DB object
        [HttpPut("{pointofinterestid}")]
        public async Task<ActionResult> UpdatePointOfInterest(SearchQuery searchQuery)
        {
            if (!await _cityInfoRepository.CityExistsAsync(searchQuery))
            {
                return NotFound();
            }

            var pointOfInterestEntity =
                await _cityInfoRepository.GetPointOfInterestForCityAsync(searchQuery);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(searchQuery.PointOfInterestQuery.PointOfInterestForUpdate, pointOfInterestEntity);

            // Persist to db
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        // Here we are creating a new db entry hence the need for PointOfInterestForUpdateDto/DB object
        [HttpPatch("{pointofinterestid}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(SearchQuery searchQuery)
        {
            if (!await _cityInfoRepository.CityExistsAsync(searchQuery))
            {
                return NotFound();
            }

            var pointOfInterestEntity =
                await _cityInfoRepository.GetPointOfInterestForCityAsync(searchQuery);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch =
                _mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

            searchQuery?.PointOfInterestQuery?.JsonPatchDocument.ApplyTo(pointOfInterestToPatch, ModelState); // Pass in model state, any errors of this type will make the model state invalid

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity); // Map changes back to entity

            await _cityInfoRepository.SaveChangesAsync(); // Persist the changes to db

            return NoContent();
        }

        [HttpDelete("{pointofinterestid}")]
        public async Task<ActionResult> DeletePointOfInterest(SearchQuery searchQuery)
        {
            if (!await _cityInfoRepository.CityExistsAsync(searchQuery))
            {
                return NotFound();
            }

            var pointOfInterestEntity =
                await _cityInfoRepository.GetPointOfInterestForCityAsync(searchQuery);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity); // Non-async because it is an in-memory operation, not I/O

            await _cityInfoRepository.SaveChangesAsync();

            _localMailService.Send("Point of interest deleted.",
            $"Point of interest {pointOfInterestEntity.Name} " +
            $"with id {pointOfInterestEntity.Id} has been deleted.");

            return NoContent();
        }
    }
}