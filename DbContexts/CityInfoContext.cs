using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext // Property values initialised as non-null in DbContext after leaving the base ctor
    {
        public DbSet<City> Cities { get; set; } = null!; // Null-forgiving operator
        public DbSet<PointOfInterest> PointOfInterests { get; set; } = null!;

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
                    Description = "The one with that big park."
                },                
                new City("London")
                {
                    Id = 2,
                    Description = "The one with big red busses"
                },                
                new City("Paris")
                {
                    Id = 3,
                    Description = "The one with that big tower."
                });
            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Central Park")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "Highly popular tourist destination."
                },
                new PointOfInterest("Tower Bridge")
                {
                    Id = 2,
                    CityId = 2,
                    Description = "Not to be confused with London Bridge"
                },
                new PointOfInterest("Eiffel Tower")
                {
                    Id = 3,
                    CityId = 3,
                    Description = "C'est excellent!"
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
