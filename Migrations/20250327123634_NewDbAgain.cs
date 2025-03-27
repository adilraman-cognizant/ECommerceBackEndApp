using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceBackEndApp.Migrations
{
    /// <inheritdoc />
    public partial class NewDbAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripeSessionId",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeSessionId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
