using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Async_Inn.Migrations
{
    /// <inheritdoc />
    public partial class HotelSeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "City", "Name", "PhoneNumber", "State" },
                values: new object[,]
                {
                    { 1, "Address A", "City A", "Hotel A", "07998883014", "State A" },
                    { 2, "Address B", "City B", "Hotel B", "0799783014", "State B" },
                    { 3, "Address C", "City C", "Hotel C", "07978883014", "State C" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
