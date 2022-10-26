﻿using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    // Persistence logic
    public interface ICityInfoRepository
    {
        // https://stackoverflow.com/questions/2876616/returning-ienumerablet-vs-iqueryablet
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(
            int cityId, int pointOfInterestId);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(
            int cityId);
    }
}