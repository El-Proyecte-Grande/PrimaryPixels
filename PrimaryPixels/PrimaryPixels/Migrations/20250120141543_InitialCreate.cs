using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrimaryPixels.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Availability = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalSold = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Cpu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ram = table.Column<int>(type: "int", nullable: true),
                    InternalMemory = table.Column<int>(type: "int", nullable: true),
                    DvdPlayer = table.Column<bool>(type: "bit", nullable: true),
                    CardIndependency = table.Column<bool>(type: "bit", nullable: true),
                    Wireless = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "Cpu", "DvdPlayer", "Image", "InternalMemory", "Name", "Price", "ProductType", "Ram", "TotalSold" },
                values: new object[,]
                {
                    { 1, true, "I3-6100", false, "https://s13emagst.akamaized.net/products/71331/71330955/images/res_b75e8c737204f265c382468aca168f75.jpg?width=720&height=720&hash=90CF69FDA4FFA74B09714BE0BBA2A69A", 512, "Gaming PC 3510", 100000, "Computer", 8, 0 },
                    { 2, true, "I5-8100", false, "https://s13emagst.akamaized.net/products/16684/16683989/images/res_45eee4b6571e6820111f638716cf3bf5.jpg?width=720&height=720&hash=1823932085ED244C3F414C27A78312AC", 1024, "Gaming PC 5000", 5000000, "Computer", 16, 0 },
                    { 3, true, "i9-14911KF", true, "https://s13emagst.akamaized.net/products/15188/15187279/images/res_44116bf82dc775127335f7aba3509421.jpg?width=720&height=720&hash=4F2A3C176CD289E8513E4E136C00D283", 1024, "Gaming PC 5000", 5000000, "Computer", 16, 2},
                    { 4, false, "i9-45500KJ", true, "https://s13emagst.akamaized.net/products/15188/15187279/images/res_44116bf82dc775127335f7aba3509421.jpg?width=720&height=720&hash=4F2A3C176CD289E8513E4E136C00D283", 2048, "Gamer PC A-I7689", 1000000, "Computer", 32, 5 },
                    { 5, true, "I6-2300", false, "https://s13emagst.akamaized.net/products/16684/16683989/images/res_45eee4b6571e6820111f638716cf3bf5.jpg?width=720&height=720&hash=1823932085ED244C3F414C27A78312AC", 1024, "Gamer PC C-I4567", 1300000, "Computer", 32, 11 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "Image", "Name", "Price", "ProductType", "TotalSold", "Wireless" },
                values: new object[,]
                {
                    { 6, true, "https://s13emagst.akamaized.net/products/78736/78735363/images/res_42c4ab07b083c382ac958c5abde97ccd.jpg?width=720&height=720&hash=AF0FC648A7C459193BB006D07F758597", "Ultra pro HP 5000", 500, "Headphone", 0, false },
                    { 7, true, "https://s13emagst.akamaized.net/products/46154/46153291/images/res_3d60cf092b7bb976cb438aeb4788d04f.jpg?width=720&height=720&hash=27C95317E6A8947487149076F5F52C1E", "DAH6789WH Bluetooth", 20000, "Headphone", 1, true },
                    { 8, false, "https://s13emagst.akamaized.net/products/78736/78735363/images/res_42c4ab07b083c382ac958c5abde97ccd.jpg?width=720&height=720&hash=AF0FC648A7C459193BB006D07F758597", "Kids Headphone 3000", 2000, "Headphone", 1, false },
                    { 9, true, "https://s13emagst.akamaized.net/products/34948/34947038/images/res_6aff00e29067abeb44a4172c9518a711.jpg?width=720&height=720&hash=150D280D7245265AAEAB81328D899B73", "Ultra wireless GP", 3000, "Headphone", 9, true },
                    { 10, true, "https://s13emagst.akamaized.net/products/46154/46153291/images/res_3d60cf092b7bb976cb438aeb4788d04f.jpg?width=720&height=720&hash=27C95317E6A8947487149076F5F52C1E", "Ultra Stereo BT 30", 15000, "Headphone", 0, true }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "CardIndependency", "Cpu", "Image", "InternalMemory", "Name", "Price", "ProductType", "Ram", "TotalSold" },
                values: new object[,]
                {
                    { 11, true, true, "Dimensity 9400", "https://s13emagst.akamaized.net/products/49762/49761516/images/res_a3c8676c0043ba09f646d836b7fc37bb.jpg?width=720&height=720&hash=30109537CDA74ED1D82D5D87E13E5BE1", 128, "Redmi A24", 100000, "Phone", 4, 0 },
                    { 12, true, false, "Dimensity 11000", "https://s13emagst.akamaized.net/products/62470/62469084/images/res_119d9158060ffab289ace1eb4fb5f285.jpg?width=720&height=720&hash=2025575EAA3830851B708B6666E37773", 512, "iPhone 19", 5000000, "Phone", 16, 0 },
                    { 13, true, true, "Dimensity 9800", "https://s13emagst.akamaized.net/products/57643/57642529/images/res_77d22d24346a96ca4e3f4e7a9eeea779.jpg?width=720&height=720&hash=61507AB28D44CE94F541B937B7FB239C", 256, "Redmi A29", 600000, "Phone", 8, 0 },
                    { 14, false, false, "Exynos 4520", "https://s13emagst.akamaized.net/products/62470/62469084/images/res_119d9158060ffab289ace1eb4fb5f285.jpg?width=720&height=720&hash=2025575EAA3830851B708B6666E37773", 256, "Samsung Galaxy E100", 260000, "Phone", 8, 2 },
                    { 15, true, true, "Exynos 3434", "https://s13emagst.akamaized.net/products/57643/57642529/images/res_77d22d24346a96ca4e3f4e7a9eeea779.jpg?width=720&height=720&hash=61507AB28D44CE94F541B937B7FB239C", 128, "HONOR AZC", 105000, "Phone", 16, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ProductId",
                table: "ShoppingCartItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
