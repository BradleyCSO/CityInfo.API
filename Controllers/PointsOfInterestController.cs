using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")] // All class members have this initial route path
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _localMailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService localMailService, CitiesDataStore citiesDataStore,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with ID {cityId} wasn't found when accessing points of interest.");
                return NotFound(); // In this case we should return a 404 because if the city passed within cityId doesn't exist, because the URI itself wouldn't be pointing to a mapped resource
            }

            var pointsOfInterestForCity =
                await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
        }

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
        }

    //    [HttpPost]
    //    public ActionResult<PointOfInterestDto> CreatePointOfInterest(
    //        int cityId, 
    //        PointOfInterestForCreationDto pointOfInterest)
    //    {
    //        // For when a user sends a request for a resource URI that does not exist/trying to add a
    //        // Point of interst for a city that does not exist
    //        var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

    //        if (city == null)
    //        {
    //            return NotFound();
    //        }

    //        // Add point of interest by calculating the ID of the new point of interest
    //        var maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

    //        var finalPointOfInterest = new PointOfInterestDto()
    //        {
    //            Id = ++maxPointOfInterestId, // Take maxPointOfInterestID and add 1
    //            Name = pointOfInterest.Name,
    //            Description = pointOfInterest.Description,
    //        };

    //        city.PointsOfInterest.Add(finalPointOfInterest);

    //        return CreatedAtRoute("GetPointOfInterest", new
    //        {
    //            cityId = cityId,
    //            pointOfInterestId = finalPointOfInterest.Id
    //        },
    //        finalPointOfInterest // Add to response body
    //        );
    //    }

    //    [HttpPut("{pointofinterestid}")]
    //    public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, 
    //        PointOfInterestForUpdateDto pointOfInterest)
    //    {
    //        var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

    //        if (city == null)
    //        {
    //            return NotFound();
    //        }

    //        var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);
    //        if (pointOfInterestFromStore == null)
    //        {
    //            return NotFound();
    //        }

    //        pointOfInterestFromStore.Name = pointOfInterest.Name;
    //        pointOfInterestFromStore.Description = pointOfInterest.Description;

    //        return NoContent();
    //    }

    //    [HttpPatch("{pointofinterestid}")]
    //    public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, 
    //        JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument) // JSON patch document being the list of operations that we want to apply to the point of interest
    //    {
    //        var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

    //        if (city == null)
    //        {
    //            return NotFound();
    //        }

    //        var pointOfInterestFromStore = city.PointsOfInterest
    //            .FirstOrDefault(c => c.Id == pointOfInterestId);

    //        if (pointOfInterestFromStore == null)
    //        {
    //            return NotFound();
    //        }

    //        var pointOfInterestToPatch =
    //            new PointOfInterestForUpdateDto()
    //            {
    //                Name = pointOfInterestFromStore.Name,
    //                Description = pointOfInterestFromStore.Description
    //            };

    //        patchDocument.ApplyTo(pointOfInterestToPatch, ModelState); // Pass in model state, any errors of this type will make the model state invalid

    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        if (!TryValidateModel(pointOfInterestToPatch))
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        // Set the new values
    //        pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
    //        pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

    //        return NoContent();
    //    }

    //    [HttpDelete("{pointofinterestid}")]
    //    public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
    //    {
    //        var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

    //        if (city == null)
    //        {
    //            return NotFound();
    //        }

    //        var pointOfInterestFromStore = city.PointsOfInterest.
    //            FirstOrDefault(c => c.Id == pointOfInterestId);

    //        if (pointOfInterestFromStore == null)
    //        {
    //            return NotFound();
    //        }

    //        city.PointsOfInterest.Remove(pointOfInterestFromStore);
    //        _localMailService.Send("Point of interest deleted.",
    //            $"Point of interest {pointOfInterestFromStore.Name} " +
    //            $"with id {pointOfInterestFromStore.Id} has been deleted.");

    //        return NoContent();
    //    }
    }
}
