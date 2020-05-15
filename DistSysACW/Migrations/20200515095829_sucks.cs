using Microsoft.EntityFrameworkCore.Migrations;

namespace DistSysACW.Migrations
{
    public partial class sucks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_UserApiKey",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "UserApiKey",
                table: "Logs",
                newName: "ApiKey");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_UserApiKey",
                table: "Logs",
                newName: "IX_Logs_ApiKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_ApiKey",
                table: "Logs",
                column: "ApiKey",
                principalTable: "Users",
                principalColumn: "ApiKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_ApiKey",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "ApiKey",
                table: "Logs",
                newName: "UserApiKey");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_ApiKey",
                table: "Logs",
                newName: "IX_Logs_UserApiKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_UserApiKey",
                table: "Logs",
                column: "UserApiKey",
                principalTable: "Users",
                principalColumn: "ApiKey",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
