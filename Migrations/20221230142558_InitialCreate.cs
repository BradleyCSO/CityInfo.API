using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Continent = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    ImageAltText = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PointsOfInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    ImageAltText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointsOfInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointsOfInterests_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Continent", "Country", "Description", "ImageAltText", "ImageUrl", "Name" },
                values: new object[] { 1, "North America", "United States", "New York is composed of five boroughs – Brooklyn, the Bronx, Manhattan, Queens and Staten Island - is home to 8.4 million people who speak more than 200 languages, hail from every corner of the globe, and, together, are the heart and soul of the most dynamic city in the world.", "An image showing an aerial view of the New York skyline on a sunny afternoon.", "https://thumbs.dreamstime.com/b/new-york-skyline-sunny-afternoon-aerial-view-53728429.jpg", "New York City" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Continent", "Country", "Description", "ImageAltText", "ImageUrl", "Name" },
                values: new object[] { 2, "Europe", "United Kingdom", "Noisy and vibrant, London is a megalopolis of people, ideas and frenetic energy. The capital and largest city of England, and of the wider United Kingdom, it is also the largest city in Western Europe. Situated on the River Thames in South-East England, Greater London has an official population of a little over 8 million, but the estimate of between 12 and 14 million people in the greater metropolitan area better reflects its size and importance.", "An image showing Tower Bridge on an evening with dark clouds.", "https://thumbs.dreamstime.com/b/tower-bridge-london-uk-38138737.jpg", "London" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Continent", "Country", "Description", "ImageAltText", "ImageUrl", "Name" },
                values: new object[] { 3, "Europe", "France", "Paris has the reputation of being the most beautiful and romantic of all cities, brimming with historic associations and remaining vastly influential in the realms of culture, art, fashion, food and design.", "A picture of the Eiffel Tower at sundown.", "https://cdn.pixabay.com/photo/2018/04/25/09/26/eiffel-tower-3349075__480.jpg", "Paris" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Continent", "Country", "Description", "ImageAltText", "ImageUrl", "Name" },
                values: new object[] { 4, "North America", "United States", "A major city and commercial hub in North Carolina. Its modern city center (Uptown) is home to the Levine Museum of the New South, which explores post–Civil War history in the South, and hands-on science displays at Discovery Place.", "An image showing a series of North Carolina skylines.", "https://thumbs.dreamstime.com/b/charlotte-north-carolina-nc-drone-skyline-aerial-153829341.jpg", "Charlotte" });

            migrationBuilder.InsertData(
                table: "PointsOfInterests",
                columns: new[] { "Id", "CityId", "Description", "ImageAltText", "ImageUrl", "Name" },
                values: new object[] { 1, 1, "Central Park is an urban park in New York City located between the Upper West and Upper East Sides of Manhattan. It is the fifth-largest park in the city, covering 843 acres.", "A morning taken directly in Central Park.", "https://media.istockphoto.com/id/1309037300/photo/central-park-in-spring.jpg?b=1&s=170667a&w=0&k=20&c=nTMGmhXEwkB_s9gglwFX9qz74RbjG9caQryz36l02Sc=", "Central Park" });

            migrationBuilder.InsertData(
                table: "PointsOfInterests",
                columns: new[] { "Id", "CityId", "Description", "ImageAltText", "ImageUrl", "Name" },
                values: new object[] { 2, 2, "Tower Bridge is a Grade I listed combined bascule and suspension bridge in London, built between 1886 and 1894, designed by Horace Jones and engineered by John Wolfe Barry with the help of Henry Marc Brunel.", "An image of Tower Bridge in the evening.", "https://media.istockphoto.com/id/1337592981/photo/tower-bridge-in-the-evening-london-england-uk.jpg?b=1&s=170667a&w=0&k=20&c=W3wH2itHMImP3ZAEk_ZDk3UHopEQ7B_k4SJs-wGZEL8=", "Tower Bridge" });

            migrationBuilder.InsertData(
                table: "PointsOfInterests",
                columns: new[] { "Id", "CityId", "Description", "ImageAltText", "ImageUrl", "Name" },
                values: new object[] { 3, 3, "The Avenue des Champs-Elysees is an avenue in the 8th arrondissement of Paris, France, 1.9 kilometres long and 70 metres wide, running between the Place de la Concorde in the east and the Place Charles de Gaulle in the west, where the Arc de Triomphe is located.", "An image of the Champs-Élysées taken at night.", "https://thumbs.dreamstime.com/b/champs-elysees-arc-de-triomphe-night-paris-champs-elysees-arc-de-triomphe-night-paris-france-107377207.jpg", "Champs-Elysees" });

            migrationBuilder.InsertData(
                table: "PointsOfInterests",
                columns: new[] { "Id", "CityId", "Description", "ImageAltText", "ImageUrl", "Name" },
                values: new object[] { 4, 4, "The NASCAR Hall of Fame, located in Charlotte, North Carolina, honors drivers who have shown expert skill at NASCAR driving, all-time great crew chiefs and owners, broadcasters and other major contributors to competition within the sanctioning body.", "A picture of the NASCAR Hall of Fame building, in Charlotte, North Carolina.", "https://media.istockphoto.com/id/854657408/photo/nascar-hall-of-fame-in-charlotte-north-carolina-usa.jpg?s=612x612&w=0&k=20&c=5WNpQ-xkPjkUF8EQdVTw93fYsdmurJe4SRQf7m4kCmE=", "NASCAR Hall of Fame" });

            migrationBuilder.CreateIndex(
                name: "IX_PointsOfInterests_CityId",
                table: "PointsOfInterests",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointsOfInterests");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
