using CityInfo.API.Entities;
using CityInfo.API.Models.DTOs;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;

namespace CityInfo.API.Models.Responses
{
    public class SearchQuery : PaginationMetadata
    {
        public string? Query { get; set; }
        public CityQuery? CityQuery { get; set; }
        public PointOfInterestQuery? PointOfInterestQuery { get; set; }
    }

    public class CityQuery
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Continent { get; set; }
        public string[]? Countries { get; set; } = new string[0];
    }
    
    public class PointOfInterestQuery
    {
        public int? Id { get; set; }
        public bool? IncludePointsOfInterest { get; set; }
        public PointOfInterest? PointOfInterest { get; set; }
        public PointOfInterestForCreationDto? PointOfInterestForCreation { get; set; }
        public PointOfInterestForUpdateDto? PointOfInterestForUpdate { get; set; }
        public JsonPatchDocument<PointOfInterestForUpdateDto>? JsonPatchDocument { get; set; } // JSON patch document being the list of operations that we want to apply to the point of interest
    }
}