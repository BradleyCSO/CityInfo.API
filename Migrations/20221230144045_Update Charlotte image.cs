using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.API.Migrations
{
    public partial class UpdateCharlotteimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://media.istockphoto.com/id/1276994658/photo/fall-in-charlotte-nc.jpg?b=1&s=170667a&w=0&k=20&c=yI0cUp8q2xSHF8szoQIv5sVpAAQekyqMtv0FBQZermY=");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://thumbs.dreamstime.com/b/charlotte-north-carolina-nc-drone-skyline-aerial-153829341.jpg");
        }
    }
}
