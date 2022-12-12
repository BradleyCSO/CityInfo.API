﻿using AutoMapper;
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

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
            int cityId,
            PointOfInterestForCreationDto pointOfInterest)
        {
            // For when a user sends a request for a resource URI that does not exist/trying to add a
            // Point of interest for a city that does not exist
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPointOfInterest);

            await _cityInfoRepository.SaveChangesAsync(); // If something goes wrong at this point, it's a server-side issue

            var createdPointOfInterestToReturn = _mapper.Map<PointOfInterestDto>(finalPointOfInterest); // Persist the changes, should now have foreign keys and an id

            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = createdPointOfInterestToReturn.Id
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

        [HttpPut("{pointofinterestid}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId,
            PointOfInterestForUpdateDto pointOfInterest)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity =
                await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, pointOfInterestEntity);

            // Persist to db
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }
        }
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
    }