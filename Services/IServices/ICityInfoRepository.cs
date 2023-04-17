using CityInfo.API.Entities;
using CityInfo.API.Models.Responses;

namespace CityInfo.API.Services.IServices
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<string>> GetContinentsAsync();
        Task<IEnumerable<object>> GetCountriesAsync();
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(SearchQuery searchQuery);
        Task<City?> GetCityAsync(SearchQuery searchQuery);
        Task<bool> CityExistsAsync(SearchQuery searchQuery);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(SearchQuery searchQuery);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(SearchQuery searchQuery);
        Task AddPointOfInterestForCityAsync(SearchQuery searchQuery);
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
        Task<bool> CityNameMatchesCityId(SearchQuery searchQuery);
        Task<bool> SaveChangesAsync();
    }
}