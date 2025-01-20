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
                    { 1, true, "I3-6100", false, "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_98587778/mobile_786_587_png/X-X-GAMER-I3228-Gamer-PC-%28Core-i5-16GB-480-GB-SSD---2-TB-HDD-RX6750XT-12GB-NoOS%29", 512, "Gaming PC 3510", 100000, "Computer", 8, 0 },
                    { 2, true, "I5-8100", false, "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_143744896/mobile_786_587_png/SHARKGAMING-RGBeast-R900-SGRGBR900-33-4090-Gamer-PC-%28Ryzen9-32GB-2x1024-GB-SSD-Win11H%29", 1024, "Gaming PC 5000", 5000000, "Computer", 16, 0 },
                    { 3, true, "i9-14911KF", true, "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_146904591?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402", 2048, "Gamer PC A-I5642", 800000, "Computer", 16, 3 },
                    { 4, false, "i9-45500KJ", true, "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_98587780?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402", 2048, "Gamer PC A-I7689", 1000000, "Computer", 32, 5 },
                    { 5, true, "I6-2300", false, "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_138935428?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402", 1024, "Gamer PC C-I4567", 1300000, "Computer", 32, 11 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "Image", "Name", "Price", "ProductType", "TotalSold", "Wireless" },
                values: new object[,]
                {
                    { 6, true, "https://cdn.lifehack.org/wp-content/uploads/2014/12/28.jpg", "Ultra pro max Headphone 2000", 500, "Headphone", 0, false },
                    { 7, true, "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_135532150?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402", "DAH6789WH Bluetooth Headphone", 20000, "Headphone", 1, true },
                    { 8, false, "https://assets.mmsrg.com/isr/166325/c1/-/pixelboxx-mss-81231790?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402", "Kids Headphone 3000", 2000, "Headphone", 1, false },
                    { 9, true, "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_77379180?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402", "Ultra wireless Headphone", 3000, "Headphone", 9, true },
                    { 10, true, "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_140559725?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402", "Ultra Stereo BT Headset 300", 15000, "Headphone", 0, true }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "CardIndependency", "Cpu", "Image", "InternalMemory", "Name", "Price", "ProductType", "Ram", "TotalSold" },
                values: new object[,]
                {
                    { 11, true, true, "Dimensity 9400", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSELHPTLjLUf8sBWPzXg7bTDdG1nClBF5Kc4A&s", 128, "Redmi A24", 100000, "Phone", 4, 0 },
                    { 12, true, false, "Dimensity 11000", "https://www.tecnosell.com/media/catalog/product/cache/60c31028333b516fd0f8945d994bb7aa/b/l/blu1_2_7_1.jpg", 512, "iPhone 19", 5000000, "Phone", 16, 0 },
                    { 13, true, true, "Dimensity 9800", "https://cdn.tmobile.com/content/dam/t-mobile/en-p/cell-phones/apple/Apple-iPhone-15-Plus/Pink/Apple-iPhone-15-Plus-Pink-thumbnail.png", 256, "Redmi A29", 600000, "Phone", 8, 0 },
                    { 14, false, false, "Exynos 4520", "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_137998148?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402", 256, "Samsung Galaxy E100", 260000, "Phone", 8, 2 },
                    { 15, true, true, "Exynos 3434", "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_144574340?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402", 128, "HONOR AZC", 105000, "Phone", 16, 8 }
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
