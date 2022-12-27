﻿using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    // Persistence logic
    public interface ICityInfoRepository
    {
        // https://stackoverflow.com/questions/2876616/returning-ienumerablet-vs-iqueryablet
        Task<IEnumerable<string>> GetCountriesAsync();
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, string? continent, string?[] countries, int pageNumber, int pageSize);
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
