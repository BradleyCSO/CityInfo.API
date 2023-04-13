using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using CityInfo.API.Models.Responses;
using CityInfo.API.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<bool> CityNameMatchesCityId(int cityId, string? cityName)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId && c.Name == cityName);
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(SearchQuery searchQuery)
        {
            // Collection to start from, for deferred execution
            var collection = _context.Cities as IQueryable<City>;
            //IQueryable<City> cityCollection = Enumerable.Empty<City>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchQuery.CityQuery.Name))
            {
                searchQuery.CityQuery.Name = searchQuery.CityQuery.Name.Trim();
                collection = collection.Where(c => c.Name == searchQuery.CityQuery.Name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery.Query))
            {
                searchQuery.Query = searchQuery.Query.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery.Query)
                || (a.Description != null && a.Description.Contains(searchQuery.Query)));
            }

            if (!string.IsNullOrWhiteSpace(searchQuery.CityQuery.Continent))
            {
                searchQuery.CityQuery.Continent = searchQuery.CityQuery.Continent.Trim();
                collection = collection.Where(c => c.Continent == searchQuery.CityQuery.Continent);
            }

            if (searchQuery.CityQuery.Countries.Any())
            {
                collection = collection.Where(c => searchQuery.CityQuery.Countries.Contains(c.Country));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, searchQuery.PageSize, searchQuery.CurrentPage);

            var collectionToReturn = await collection
                .OrderBy(c => c.Name)
                .Skip(searchQuery.PageSize * (searchQuery.CurrentPage - 1))
                .Take(searchQuery.PageSize)
                .ToListAsync();


            return (collectionToReturn, paginationMetadata);
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task<IEnumerable<string>> GetContinentsAsync()
        {
            return await _context.Cities.Select(c=> c.Continent).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<object>> GetCountriesAsync()
        {
            return await _context.Cities.Select(c => new { Country = c.Country, Continent = c.Continent }).Distinct().ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return await _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }

            return await _context.Cities
                .Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(
            int cityId, int pointOfInterestId)
        {
            return await _context.PointsOfInterests
                .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(
            int cityId)
        {
            return await _context.PointsOfInterests
                .Where(p => p.CityId == cityId).ToListAsync();
        }

        public async Task AddPointOfInterestForCityAsync(int cityId, 
            PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);

            if (city != null)
            {
                city.PointsOfInterest.Add(pointOfInterest);
            }
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterests.Remove(pointOfInterest);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0; // true when 0 or more enties have succesfully been saved
        }
    }
}
