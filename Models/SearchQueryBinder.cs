using System;
using System.Threading.Tasks;
using CityInfo.API.Models.Responses;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CityInfo.API.Models
{
    public class SearchQueryBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var searchQuery = new SearchQuery(
                totalItemCount: 0,
                pageSize: 10,
                currentPage: 1);

            // Bind the query string values to the SearchQuery model
            bindingContext.HttpContext.Request.Query.TryGetValue("query", out var queryValue);
            searchQuery.Query = queryValue;

            // Bind the CityQuery model from the query string
            var cityQuery = new CityQuery();
            bindingContext.HttpContext.Request.Query.TryGetValue("CityQuery.Id", out var cityIdValue);
            if (int.TryParse(cityIdValue, out var cityId))
            {
                cityQuery.Id = cityId;
            }
            bindingContext.HttpContext.Request.Query.TryGetValue("CityQuery.Name", out var cityNameValue);
            cityQuery.Name = cityNameValue;
            bindingContext.HttpContext.Request.Query.TryGetValue("CityQuery.Continent", out var continentValue);
            cityQuery.Continent = continentValue;
            bindingContext.HttpContext.Request.Query.TryGetValue("CityQuery.Countries", out var countriesValue);
            cityQuery.Countries = countriesValue.ToArray();
            searchQuery.CityQuery = cityQuery;

            // Bind the PointOfInterestQuery model from the query string
            var poiQuery = new PointOfInterestQuery();
            bindingContext.HttpContext.Request.Query.TryGetValue("PointOfInterestQuery.Id", out var poiIdValue);
            if (int.TryParse(poiIdValue, out var poiId))
            {
                poiQuery.Id = poiId;
            }
            bindingContext.HttpContext.Request.Query.TryGetValue("PointOfInterestQuery.IncludePointsOfInterest", out var poiIncludeValue);
            if (bool.TryParse(poiIncludeValue, out var poiInclude))
            {
                poiQuery.IncludePointsOfInterest = poiInclude;
            }
            searchQuery.PointOfInterestQuery = poiQuery;

            // Set the bound model to the ModelBindingContext
            bindingContext.Result = ModelBindingResult.Success(searchQuery);
            return Task.CompletedTask;
        }
    }
}
