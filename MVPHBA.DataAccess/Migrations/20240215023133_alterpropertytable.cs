using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVPHBA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class alterpropertytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyInfos_AspNetUsers_UserId",
                table: "PropertyInfos");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PropertyInfos",
                newName: "BrokerId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyInfos_UserId",
                table: "PropertyInfos",
                newName: "IX_PropertyInfos_BrokerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyInfos_AspNetUsers_BrokerId",
                table: "PropertyInfos",
                column: "BrokerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyInfos_AspNetUsers_BrokerId",
                table: "PropertyInfos");

            migrationBuilder.RenameColumn(
                name: "BrokerId",
                table: "PropertyInfos",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyInfos_BrokerId",
                table: "PropertyInfos",
                newName: "IX_PropertyInfos_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyInfos_AspNetUsers_UserId",
                table: "PropertyInfos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
