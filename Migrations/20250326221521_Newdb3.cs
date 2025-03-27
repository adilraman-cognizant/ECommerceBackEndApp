using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceBackEndApp.Migrations
{
    /// <inheritdoc />
    public partial class Newdb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_UserProfiles_UserProfileId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserProfileId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Reviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserProfileId",
                table: "Reviews",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_UserProfiles_UserProfileId",
                table: "Reviews",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }
    }
}
