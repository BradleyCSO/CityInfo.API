using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Models.Responses
{
    public class SearchQuery : PaginationMetadata
    {
        public SearchQuery(int totalItemCount, int pageSize, int currentPage) : base(totalItemCount, pageSize, currentPage)
        {
        }

        [FromQuery(Name = "query")]
        public string? Query { get; set; }

        public CityQuery? CityQuery { get; set; }

        [FromQuery(Name = "pointOfInterestQuery")]
        public PointOfInterestQuery? PointOfInterestQuery { get; set; }
    }

    // City
    //[FromQuery] string? name, string? searchQuery, string? continent, [FromQuery] string[]? countries, int pageNumber = 1, int pageSize = 10
    public class CityQuery
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
        [FromQuery(Name = "name")]
        public string? Name { get; set; }
        [FromQuery(Name = "continent")]
        public string? Continent { get; set; }
        [FromQuery(Name = "countries")]
        public string[]? Countries { get; set; }
    }
    
    public class PointOfInterestQuery
    {
        public int? Id { get; set; }
        public bool? IncludePointsOfInterest { get; set; }
    }
}