using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrimaryPixels.Migrations
{
    /// <inheritdoc />
    public partial class seedingusersproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "Name", "Price", "ProductType", "Sold", "TotalSold", "Wireless" },
                values: new object[] { 1, true, "Ultra pro max Headphone 2000", 500, "Headphone", 0, 0, false });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "Cpu", "DvdPlayer", "InternalMemory", "Name", "Price", "ProductType", "Ram", "Sold", "TotalSold" },
                values: new object[] { 2, true, "I3-6100", false, 512, "Gaming PC 3510", 100000, "Computer", 8, 0, 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "CardIndependency", "Cpu", "InternalMemory", "Name", "Price", "ProductType", "Ram", "Sold", "TotalSold" },
                values: new object[] { 3, true, true, "Dimensity 9400", 128, "Redmi A24", 100000, "Phone", 4, 0, 0 });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

        }
    }
}
