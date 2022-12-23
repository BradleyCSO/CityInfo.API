using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext // Property values initialised as non-null in DbContext after leaving the base ctor
    {
        public DbSet<City> Cities { get; set; } = null!; // Null-forgiving operator
        public DbSet<PointOfInterest> PointsOfInterests { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options) :
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("New York City")
                {
                    Id = 1,
                    Description = "New York is composed of five boroughs – Brooklyn, the Bronx, Manhattan, Queens and Staten Island - is home to 8.4 million people who speak more than 200 languages, hail from every corner of the globe, and, together, are the heart and soul of the most dynamic city in the world.",
                    Continent = "North America",
                    Country = "United States"
                },                
                new City("Charlotte")
                {
                    Id = 2,
                    Description = "A major city and commercial hub in North Carolina. Its modern city center (Uptown) is home to the Levine Museum of the New South, which explores post–Civil War history in the South, and hands-on science displays at Discovery Place.",
                    Continent = "North America",
                    Country = "United States"
                },
                new City("London")
                {
                    Id = 3,
                    Description = "Noisy and vibrant, London is a megalopolis of people, ideas and frenetic energy. The capital and largest city of England, and of the wider United Kingdom, it is also the largest city in Western Europe. Situated on the River Thames in South-East England, Greater London has an official population of a little over 8 million, but the estimate of between 12 and 14 million people in the greater metropolitan area better reflects its size and importance.",
                    Continent = "Europe",
                    Country = "United Kingdom"

                },                
                new City("Paris")
                {
                    Id = 4,
                    Description = "Paris has the reputation of being the most beautiful and romantic of all cities, brimming with historic associations and remaining vastly influential in the realms of culture, art, fashion, food and design.",
                    Continent = "Europe",
                    Country = "France"
                });
            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Central Park")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "Central Park is an urban park in New York City located between the Upper West and Upper East Sides of Manhattan. It is the fifth-largest park in the city, covering 843 acres."
                },
                new PointOfInterest("NASCAR Hall of Fame")
                {
                    Id = 2,
                    CityId = 2,
                    Description = "The NASCAR Hall of Fame, located in Charlotte, North Carolina, honors drivers who have shown expert skill at NASCAR driving, all-time great crew chiefs and owners, broadcasters and other major contributors to competition within the sanctioning body. "
                },
                new PointOfInterest("Tower Bridge")
                {
                    Id = 3,
                    CityId = 3,
                    Description = "Tower Bridge is a Grade I listed combined bascule and suspension bridge in London, built between 1886 and 1894, designed by Horace Jones and engineered by John Wolfe Barry with the help of Henry Marc Brunel."
                },
                new PointOfInterest("Eiffel Tower")
                {
                    Id = 4,
                    CityId = 4,
                    Description = "The Eiffel Tower is a wrought-iron lattice tower on the Champ de Mars in Paris, France. It is named after the engineer Gustave Eiffel, whose company designed and built the tower. Locally nicknamed 'La dame de fer', it was constructed from 1887 to 1889 as the centerpiece of the 1889 World's Fair."
                });
            base.OnModelCreating(modelBuilder);
        }

        // One way to configure db context
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
