using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceBackEndApp.Migrations
{
    /// <inheritdoc />
    public partial class NewWishlist2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_WishLists_WishListId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_WishListId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "WishListId",
                table: "UserProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WishListId",
                table: "UserProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_WishListId",
                table: "UserProfiles",
                column: "WishListId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_WishLists_WishListId",
                table: "UserProfiles",
                column: "WishListId",
                principalTable: "WishLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
