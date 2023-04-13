using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Models.Responses
{
    public class SearchQuery : PaginationMetadata
    {
        public SearchQuery(int totalItemCount, int pageSize, int currentPage) : base(totalItemCount, pageSize, currentPage)
        {
        }

        public string? Query { get; set; }

        public CityQuery CityQuery { get; set; }

        public PointOfInterestQuery PointOfInterestQuery { get; set; }
    }

    // City
    //[FromQuery] string? name, string? searchQuery, string? continent, [FromQuery] string[]? countries, int pageNumber = 1, int pageSize = 10
    public class CityQuery
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Continent { get; set; }
        public string[]? Countries { get; set; }
    }
    
    public class PointOfInterestQuery
    {
        public int? Id { get; set; }
        public bool? IncludePointsOfInterest { get; set; }
    }
}