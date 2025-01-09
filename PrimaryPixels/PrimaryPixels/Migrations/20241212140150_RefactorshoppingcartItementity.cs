using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimaryPixels.Migrations
{
    /// <inheritdoc />
    public partial class RefactorshoppingcartItementity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitPrice",
                table: "ShoppingCartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ShoppingCartItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "UnitPrice",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShoppingCartItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "UnitPrice",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShoppingCartItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "UnitPrice",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShoppingCartItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "UnitPrice",
                value: 0);
        }
    }
}
