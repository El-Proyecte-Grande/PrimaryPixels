using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimaryPixels.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentIntentIdcolumntoorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true
            );
        }
    }
}
