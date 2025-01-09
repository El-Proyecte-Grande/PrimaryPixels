using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrimaryPixels.Migrations
{
    /// <inheritdoc />
    public partial class userIdtostringinShoppingCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShoppingCartItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShoppingCartItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ShoppingCartItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ShoppingCartItems",
                keyColumn: "Id",
                keyValue: 4);


            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ShoppingCartItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ShoppingCartItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "ShoppingCartItems",
                columns: new[] { "Id", "ProductId", "Quantity", "UserId" },
                values: new object[,]
                {
                    { 1, 4, 2, 3 },
                    { 2, 2, 4, 1 },
                    { 3, 2, 1, 2 },
                    { 4, 3, 2, 2 }
                });

           
        }
    }
}
