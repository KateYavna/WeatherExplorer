using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherData.Migrations
{
    /// <inheritdoc />
    public partial class WeatherScreen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherScreens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeatherDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    TemperatureFeelsLike = table.Column<double>(type: "float", nullable: false),
                    TemperatureMax = table.Column<double>(type: "float", nullable: false),
                    TemperatureMin = table.Column<double>(type: "float", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    WindSpeed = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherScreens", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherScreens");
        }
    }
}
