using CityInfo.API.Entities;
using CityInfo.API.Models.Responses;

namespace CityInfo.API.Services.IServices
{
    // Persistence logic
    public interface ICityInfoRepository
    {
        // https://stackoverflow.com/questions/2876616/returning-ienumerablet-vs-iqueryablet
        Task<IEnumerable<string>> GetContinentsAsync();
        Task<IEnumerable<object>> GetCountriesAsync();
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(SearchQuery searchQuery);
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<bool> CityExistsAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(
            int cityId, int pointOfInterestId);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(
            int cityId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
        Task<bool> CityNameMatchesCityId(int cityId, string? cityName);
        Task<bool> SaveChangesAsync();
    }
}
