using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; } = new CitiesDataStore(); // Singleton pattern

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "NYC",
                    Description = "The Big Apple..."
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "London",
                    Description = "Big ben and big red busses."
                },
            };
        }
    }
}
