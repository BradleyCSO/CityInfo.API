using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.API.Migrations
{
    public partial class Updateentries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Continent", "Country", "Description", "ImageAltText", "ImageUrl", "Name" },
                values: new object[] { 5, "Europe", "United Kingdom", "Huntingdon is a market town in the Huntingdonshire district in Cambridgeshire, England. The town was given its town charter by King John in 1205. It was the county town of the historic county of Huntingdonshire. Oliver Cromwell was born there in 1599 and became one of its Members of Parliament in 1628.", "An image showing Huntingdon Town Centre in the morning.", "https://www.harveyrobinson.co.uk/xml/cache/mceimages/hr-Huntingdon-Landmark.jpg", "Huntingdon" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Continent", "Country", "Description", "ImageAltText", "ImageUrl", "Name" },
                values: new object[] { 6, "Europe", "France", "Brest is a port city in Brittany, in northwestern France, bisected by the Penfeld river. It’s known for its rich maritime history and naval base. At the mouth of the Penfeld, overlooking the harbor, is the National Navy Museum, housed in the medieval Château de Brest.", "An image showing a busy part of the City.", "https://images.france.fr/zeaejvyq9bhj/417nfQFcHe0uuuoMw2AoIO/874bf93f75529a0801d33f1c5d7dff0c/resizedshutterstock_151480559_place-ste-anne-rennes.jpg", "Brest" });

            migrationBuilder.InsertData(
                table: "PointsOfInterests",
                columns: new[] { "Id", "CityId", "Description", "ImageAltText", "ImageUrl", "Name" },
                values: new object[] { 5, 5, "The Cromwell Museum in Huntingdon, England, is a museum containing collections exploring the life of Oliver Cromwell and to a lesser extent his son Richard Cromwell. Oliver Cromwell was born in Huntingdon in 1599 and lived there for more than half his life.", "A picture of the Cromwell Museum, taken outside.", "https://letsgowiththechildren.co.uk/wp-content/uploads/2022/03/cromwell-museum-915x515.jpg", "Cromwell Museum" });

            migrationBuilder.InsertData(
                table: "PointsOfInterests",
                columns: new[] { "Id", "CityId", "Description", "ImageAltText", "ImageUrl", "Name" },
                values: new object[] { 6, 6, "Aquarium with marine wildlife in tropical, temperate & polar zones, including 7 shark species.", "A picture taken from inside the aquarium, with a group of people enjoying the experience.", "https://www.brestaim-events.com/wp-content/uploads/2016/03/OCEANOPOLIS_1.jpg", "Oceanopolis" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PointsOfInterests",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PointsOfInterests",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
