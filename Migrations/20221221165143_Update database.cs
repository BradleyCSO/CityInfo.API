using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.API.Migrations
{
    public partial class Updatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Continent",
                table: "Cities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Cities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Continent", "Country", "Description" },
                values: new object[] { "North America", "United States", "New York is composed of five boroughs – Brooklyn, the Bronx, Manhattan, Queens and Staten Island - is home to 8.4 million people who speak more than 200 languages, hail from every corner of the globe, and, together, are the heart and soul of the most dynamic city in the world." });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Continent", "Country", "Description", "Name" },
                values: new object[] { "North America", "United States", "A major city and commercial hub in North Carolina. Its modern city center (Uptown) is home to the Levine Museum of the New South, which explores post–Civil War history in the South, and hands-on science displays at Discovery Place.", "Charlotte" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Continent", "Country", "Description", "Name" },
                values: new object[] { "Europe", "United Kingdom", "Noisy and vibrant, London is a megalopolis of people, ideas and frenetic energy. The capital and largest city of England, and of the wider United Kingdom, it is also the largest city in Western Europe. Situated on the River Thames in South-East England, Greater London has an official population of a little over 8 million, but the estimate of between 12 and 14 million people in the greater metropolitan area better reflects its size and importance.", "London" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Continent", "Country", "Description", "Name" },
                values: new object[] { 4, "Europe", "France", "Paris has the reputation of being the most beautiful and romantic of all cities, brimming with historic associations and remaining vastly influential in the realms of culture, art, fashion, food and design.", "Paris" });

            migrationBuilder.UpdateData(
                table: "PointsOfInterests",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Central Park is an urban park in New York City located between the Upper West and Upper East Sides of Manhattan. It is the fifth-largest park in the city, covering 843 acres.");

            migrationBuilder.UpdateData(
                table: "PointsOfInterests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "The NASCAR Hall of Fame, located in Charlotte, North Carolina, honors drivers who have shown expert skill at NASCAR driving, all-time great crew chiefs and owners, broadcasters and other major contributors to competition within the sanctioning body. ", "NASCAR Hall of Fame" });

            migrationBuilder.UpdateData(
                table: "PointsOfInterests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Tower Bridge is a Grade I listed combined bascule and suspension bridge in London, built between 1886 and 1894, designed by Horace Jones and engineered by John Wolfe Barry with the help of Henry Marc Brunel.", "Tower Bridge" });

            migrationBuilder.InsertData(
                table: "PointsOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 4, 4, "The Eiffel Tower is a wrought-iron lattice tower on the Champ de Mars in Paris, France. It is named after the engineer Gustave Eiffel, whose company designed and built the tower. Locally nicknamed 'La dame de fer', it was constructed from 1887 to 1889 as the centerpiece of the 1889 World's Fair.", "Eiffel Tower" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PointsOfInterests",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Continent",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Cities");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "The one with that big park.");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "The one with big red busses", "London" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "The one with that big tower.", "Paris" });

            migrationBuilder.UpdateData(
                table: "PointsOfInterests",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Highly popular tourist destination.");

            migrationBuilder.UpdateData(
                table: "PointsOfInterests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Not to be confused with London Bridge", "Tower Bridge" });

            migrationBuilder.UpdateData(
                table: "PointsOfInterests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "C'est excellent!", "Eiffel Tower" });
        }
    }
}
