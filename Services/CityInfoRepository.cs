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

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(SearchQuery searchQuery)
        {
            var collection = _context.Cities as IQueryable<City>;

            if (!string.IsNullOrWhiteSpace(searchQuery?.CityQuery?.Name))
            {
                searchQuery.CityQuery.Name = searchQuery.CityQuery.Name.Trim();
                collection = collection.Where(c => c.Name == searchQuery.CityQuery.Name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery?.Query))
            {
                searchQuery.Query = searchQuery.Query.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery.Query)
                || (a.Description != null && a.Description.Contains(searchQuery.Query)));
            }

            if (!string.IsNullOrWhiteSpace(searchQuery?.CityQuery?.Continent))
            {
                searchQuery.CityQuery.Continent = searchQuery.CityQuery.Continent.Trim();
                collection = collection.Where(c => c.Continent == searchQuery.CityQuery.Continent);
            }

            if (searchQuery?.CityQuery?.Countries?.Any() ?? false)
            {
                collection = collection.Where(c => searchQuery.CityQuery.Countries.Contains(c.Country));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, searchQuery?.PageSize ?? 0, searchQuery?.CurrentPage ?? 0);

            var collectionToReturn = await collection
                .OrderBy(c => c.Name)
                .Skip(1 * (searchQuery?.CurrentPage - 1 ?? 0))
                .Take(searchQuery?.PageSize ?? 0)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<bool> CityNameMatchesCityId(SearchQuery searchQuery)
        {
            return await _context.Cities.AnyAsync(c => c.Id == searchQuery.CityQuery.Id && c.Name == searchQuery.CityQuery.Name);
        }
        public async Task<bool> CityExistsAsync(SearchQuery searchQuery)
        {
            return await _context.Cities.AnyAsync(c => c.Id == searchQuery.CityQuery.Id);
        }

        public async Task<IEnumerable<string>> GetContinentsAsync()
        {
            return await _context.Cities.Select(c=> c.Continent).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<object>> GetCountriesAsync()
        {
            return await _context.Cities.Select(c => new { Country = c.Country, Continent = c.Continent }).Distinct().ToListAsync();
        }

        public async Task<City?> GetCityAsync(SearchQuery searchQuery)
        {
            if (searchQuery?.PointOfInterestQuery?.IncludePointsOfInterest ?? false)
            {
                return await _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == searchQuery.CityQuery.Id).FirstOrDefaultAsync();
            }

            return await _context.Cities
                .Where(c => c.Id == searchQuery.CityQuery.Id).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(
            SearchQuery searchQuery)
        {
            return await _context.PointsOfInterests
                .Where(p => p.CityId == searchQuery.CityQuery.Id || p.Id == searchQuery.PointOfInterestQuery.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(SearchQuery searchQuery)
        {
            return await _context.PointsOfInterests
                .Where(p => p.CityId == searchQuery.CityQuery.Id).ToListAsync();
        }

        public async Task AddPointOfInterestForCityAsync(SearchQuery searchQuery)
        {
            var city = await GetCityAsync(searchQuery);

            if (city != null && 
                searchQuery?.PointOfInterestQuery?.PointOfInterest != null)
            {
                city?.PointsOfInterest.Add(searchQuery.PointOfInterestQuery.PointOfInterest);
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