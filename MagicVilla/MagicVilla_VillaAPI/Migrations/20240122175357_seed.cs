using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Pool", new DateTime(2024, 1, 22, 18, 53, 57, 171, DateTimeKind.Local).AddTicks(3935), "This is a 3 bedroom villa", "https://www.villarenters.com/content/images/properties/property_1/property_1_1.jpg", "Villa 1", 6, 1000.0, 2000, new DateTime(2024, 1, 22, 18, 53, 57, 171, DateTimeKind.Local).AddTicks(4037) },
                    { 2, "Pool", new DateTime(2024, 1, 22, 18, 53, 57, 171, DateTimeKind.Local).AddTicks(4043), "This is a 4 bedroom villa", "https://www.villarenters.com/content/images/properties/property_1/property_1_1.jpg", "Villa 2", 8, 2000.0, 3000, new DateTime(2024, 1, 22, 18, 53, 57, 171, DateTimeKind.Local).AddTicks(4047) },
                    { 3, "Pool", new DateTime(2024, 1, 22, 18, 53, 57, 171, DateTimeKind.Local).AddTicks(4050), "This is a 5 bedroom villa", "https://www.villarenters.com/content/images/properties/property_1/property_1_1.jpg", "Villa 3", 10, 3000.0, 4000, new DateTime(2024, 1, 22, 18, 53, 57, 171, DateTimeKind.Local).AddTicks(4054) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
