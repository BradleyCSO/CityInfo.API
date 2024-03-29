﻿using CityInfo.API.Entities;
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
                    Country = "United States",
                    ImageUrl = "https://thumbs.dreamstime.com/b/new-york-skyline-sunny-afternoon-aerial-view-53728429.jpg",
                    ImageAltText = "An image showing an aerial view of the New York skyline on a sunny afternoon."
                },
                new City("London")
                {
                    Id = 2,
                    Description = "Noisy and vibrant, London is a megalopolis of people, ideas and frenetic energy. The capital and largest city of England, and of the wider United Kingdom, it is also the largest city in Western Europe. Situated on the River Thames in South-East England, Greater London has an official population of a little over 8 million, but the estimate of between 12 and 14 million people in the greater metropolitan area better reflects its size and importance.",
                    Continent = "Europe",
                    Country = "United Kingdom",
                    ImageUrl = "https://thumbs.dreamstime.com/b/tower-bridge-london-uk-38138737.jpg",
                    ImageAltText = "An image showing Tower Bridge on an evening with dark clouds."
                },
                new City("Paris")
                {
                    Id = 3,
                    Description = "Paris has the reputation of being the most beautiful and romantic of all cities, brimming with historic associations and remaining vastly influential in the realms of culture, art, fashion, food and design.",
                    Continent = "Europe",
                    Country = "France",
                    ImageUrl = "https://cdn.pixabay.com/photo/2018/04/25/09/26/eiffel-tower-3349075__480.jpg",
                    ImageAltText = "A picture of the Eiffel Tower at sundown."
                },
                new City("Charlotte")
                {
                    Id = 4,
                    Description = "A major city and commercial hub in North Carolina. Its modern city center (Uptown) is home to the Levine Museum of the New South, which explores post–Civil War history in the South, and hands-on science displays at Discovery Place.",
                    Continent = "North America",
                    Country = "United States",
                    ImageUrl = "https://media.istockphoto.com/id/1276994658/photo/fall-in-charlotte-nc.jpg?b=1&s=170667a&w=0&k=20&c=yI0cUp8q2xSHF8szoQIv5sVpAAQekyqMtv0FBQZermY=",
                    ImageAltText = "An image showing a series of North Carolina skylines."
                },
                new City("Huntingdon")
                {
                    Id = 5,
                    Description = "Huntingdon is a market town in the Huntingdonshire district in Cambridgeshire, England. The town was given its town charter by King John in 1205. It was the county town of the historic county of Huntingdonshire. Oliver Cromwell was born there in 1599 and became one of its Members of Parliament in 1628.",
                    Continent = "Europe",
                    Country = "United Kingdom",
                    ImageUrl = "https://www.harveyrobinson.co.uk/xml/cache/mceimages/hr-Huntingdon-Landmark.jpg",
                    ImageAltText = "An image showing Huntingdon Town Centre in the morning."
                },
                new City("Brest")
                {
                    Id = 6,
                    Description = "Brest is a port city in Brittany, in northwestern France, bisected by the Penfeld river. It’s known for its rich maritime history and naval base. At the mouth of the Penfeld, overlooking the harbor, is the National Navy Museum, housed in the medieval Château de Brest.",
                    Continent = "Europe",
                    Country = "France",
                    ImageUrl = "https://images.france.fr/zeaejvyq9bhj/417nfQFcHe0uuuoMw2AoIO/874bf93f75529a0801d33f1c5d7dff0c/resizedshutterstock_151480559_place-ste-anne-rennes.jpg",
                    ImageAltText = "An image showing a busy part of the City."
                });
            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Central Park")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "Central Park is an urban park in New York City located between the Upper West and Upper East Sides of Manhattan. It is the fifth-largest park in the city, covering 843 acres.",
                    ImageUrl = "https://media.istockphoto.com/id/1309037300/photo/central-park-in-spring.jpg?b=1&s=170667a&w=0&k=20&c=nTMGmhXEwkB_s9gglwFX9qz74RbjG9caQryz36l02Sc=",
                    ImageAltText = "A morning taken directly in Central Park.",
                },
                new PointOfInterest("Tower Bridge")
                {
                    Id = 2,
                    CityId = 2,
                    Description = "Tower Bridge is a Grade I listed combined bascule and suspension bridge in London, built between 1886 and 1894, designed by Horace Jones and engineered by John Wolfe Barry with the help of Henry Marc Brunel.",
                    ImageUrl = "https://media.istockphoto.com/id/1337592981/photo/tower-bridge-in-the-evening-london-england-uk.jpg?b=1&s=170667a&w=0&k=20&c=W3wH2itHMImP3ZAEk_ZDk3UHopEQ7B_k4SJs-wGZEL8=",
                    ImageAltText = "An image of Tower Bridge in the evening.",
                },
                new PointOfInterest("Champs-Elysees")
                {
                    Id = 3,
                    CityId = 3,
                    Description = "The Avenue des Champs-Elysees is an avenue in the 8th arrondissement of Paris, France, 1.9 kilometres long and 70 metres wide, running between the Place de la Concorde in the east and the Place Charles de Gaulle in the west, where the Arc de Triomphe is located.",
                    ImageUrl = "https://thumbs.dreamstime.com/b/champs-elysees-arc-de-triomphe-night-paris-champs-elysees-arc-de-triomphe-night-paris-france-107377207.jpg",
                    ImageAltText = "An image of the Champs-Élysées taken at night.",
                },
                new PointOfInterest("NASCAR Hall of Fame")
                {
                    Id = 4,
                    CityId = 4,
                    Description = "The NASCAR Hall of Fame, located in Charlotte, North Carolina, honors drivers who have shown expert skill at NASCAR driving, all-time great crew chiefs and owners, broadcasters and other major contributors to competition within the sanctioning body.",
                    ImageUrl = "https://media.istockphoto.com/id/854657408/photo/nascar-hall-of-fame-in-charlotte-north-carolina-usa.jpg?s=612x612&w=0&k=20&c=5WNpQ-xkPjkUF8EQdVTw93fYsdmurJe4SRQf7m4kCmE=",
                    ImageAltText = "A picture of the NASCAR Hall of Fame building, in Charlotte, North Carolina.",
                },
                new PointOfInterest("Cromwell Museum")
                {
                    Id = 5,
                    CityId = 5,
                    Description = "The Cromwell Museum in Huntingdon, England, is a museum containing collections exploring the life of Oliver Cromwell and to a lesser extent his son Richard Cromwell. Oliver Cromwell was born in Huntingdon in 1599 and lived there for more than half his life.",
                    ImageUrl = "https://letsgowiththechildren.co.uk/wp-content/uploads/2022/03/cromwell-museum-915x515.jpg",
                    ImageAltText = "A picture of the Cromwell Museum, taken outside.",
                },
                new PointOfInterest("Oceanopolis")
                {
                    Id = 6,
                    CityId = 6,
                    Description = "Aquarium with marine wildlife in tropical, temperate & polar zones, including 7 shark species.",
                    ImageUrl = "https://www.brestaim-events.com/wp-content/uploads/2016/03/OCEANOPOLIS_1.jpg",
                    ImageAltText = "A picture taken from inside the aquarium, with a group of people enjoying the experience.",
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
