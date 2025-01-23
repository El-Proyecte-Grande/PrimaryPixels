using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimaryPixels.Migrations
{
    /// <inheritdoc />
    public partial class newimagesforproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "https://s13emagst.akamaized.net/products/71331/71330955/images/res_b75e8c737204f265c382468aca168f75.jpg?width=720&height=720&hash=90CF69FDA4FFA74B09714BE0BBA2A69A");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "https://s13emagst.akamaized.net/products/16684/16683989/images/res_45eee4b6571e6820111f638716cf3bf5.jpg?width=720&height=720&hash=1823932085ED244C3F414C27A78312AC");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "https://s13emagst.akamaized.net/products/15188/15187279/images/res_44116bf82dc775127335f7aba3509421.jpg?width=720&height=720&hash=4F2A3C176CD289E8513E4E136C00D283");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "https://s13emagst.akamaized.net/products/15188/15187279/images/res_44116bf82dc775127335f7aba3509421.jpg?width=720&height=720&hash=4F2A3C176CD289E8513E4E136C00D283");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "Image",
                value: "https://s13emagst.akamaized.net/products/16684/16683989/images/res_45eee4b6571e6820111f638716cf3bf5.jpg?width=720&height=720&hash=1823932085ED244C3F414C27A78312AC");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Image", "Name" },
                values: new object[] { "https://s13emagst.akamaized.net/products/78736/78735363/images/res_42c4ab07b083c382ac958c5abde97ccd.jpg?width=720&height=720&hash=AF0FC648A7C459193BB006D07F758597", "Ultra pro HP 5000" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Image", "Name" },
                values: new object[] { "https://s13emagst.akamaized.net/products/46154/46153291/images/res_3d60cf092b7bb976cb438aeb4788d04f.jpg?width=720&height=720&hash=27C95317E6A8947487149076F5F52C1E", "DAH6789WH Bluetooth" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "Image",
                value: "https://s13emagst.akamaized.net/products/78736/78735363/images/res_42c4ab07b083c382ac958c5abde97ccd.jpg?width=720&height=720&hash=AF0FC648A7C459193BB006D07F758597");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Image", "Name" },
                values: new object[] { "https://s13emagst.akamaized.net/products/34948/34947038/images/res_6aff00e29067abeb44a4172c9518a711.jpg?width=720&height=720&hash=150D280D7245265AAEAB81328D899B73", "Ultra wireless GP" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Image", "Name" },
                values: new object[] { "https://s13emagst.akamaized.net/products/46154/46153291/images/res_3d60cf092b7bb976cb438aeb4788d04f.jpg?width=720&height=720&hash=27C95317E6A8947487149076F5F52C1E", "Ultra Stereo BT 30" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "Image",
                value: "https://s13emagst.akamaized.net/products/49762/49761516/images/res_a3c8676c0043ba09f646d836b7fc37bb.jpg?width=720&height=720&hash=30109537CDA74ED1D82D5D87E13E5BE1");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "Image",
                value: "https://s13emagst.akamaized.net/products/62470/62469084/images/res_119d9158060ffab289ace1eb4fb5f285.jpg?width=720&height=720&hash=2025575EAA3830851B708B6666E37773");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "Image",
                value: "https://s13emagst.akamaized.net/products/57643/57642529/images/res_77d22d24346a96ca4e3f4e7a9eeea779.jpg?width=720&height=720&hash=61507AB28D44CE94F541B937B7FB239C");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "Image",
                value: "https://s13emagst.akamaized.net/products/62470/62469084/images/res_119d9158060ffab289ace1eb4fb5f285.jpg?width=720&height=720&hash=2025575EAA3830851B708B6666E37773");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "Image",
                value: "https://s13emagst.akamaized.net/products/57643/57642529/images/res_77d22d24346a96ca4e3f4e7a9eeea779.jpg?width=720&height=720&hash=61507AB28D44CE94F541B937B7FB239C");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_98587778/mobile_786_587_png/X-X-GAMER-I3228-Gamer-PC-%28Core-i5-16GB-480-GB-SSD---2-TB-HDD-RX6750XT-12GB-NoOS%29");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_143744896/mobile_786_587_png/SHARKGAMING-RGBeast-R900-SGRGBR900-33-4090-Gamer-PC-%28Ryzen9-32GB-2x1024-GB-SSD-Win11H%29");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_146904591?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_98587780?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "Image",
                value: "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_138935428?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Image", "Name" },
                values: new object[] { "https://cdn.lifehack.org/wp-content/uploads/2014/12/28.jpg", "Ultra pro max Headphone 2000" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Image", "Name" },
                values: new object[] { "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_135532150?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402", "DAH6789WH Bluetooth Headphone" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "Image",
                value: "https://assets.mmsrg.com/isr/166325/c1/-/pixelboxx-mss-81231790?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Image", "Name" },
                values: new object[] { "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_77379180?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402", "Ultra wireless Headphone" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Image", "Name" },
                values: new object[] { "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_140559725?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402", "Ultra Stereo BT Headset 300" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "Image",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSELHPTLjLUf8sBWPzXg7bTDdG1nClBF5Kc4A&s");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "Image",
                value: "https://www.tecnosell.com/media/catalog/product/cache/60c31028333b516fd0f8945d994bb7aa/b/l/blu1_2_7_1.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "Image",
                value: "https://cdn.tmobile.com/content/dam/t-mobile/en-p/cell-phones/apple/Apple-iPhone-15-Plus/Pink/Apple-iPhone-15-Plus-Pink-thumbnail.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "Image",
                value: "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_137998148?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "Image",
                value: "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_144574340?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402");
        }
    }
}
