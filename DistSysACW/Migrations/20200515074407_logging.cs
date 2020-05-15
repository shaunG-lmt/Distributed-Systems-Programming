using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DistSysACW.Migrations
{
    public partial class logging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log_Archive",
                columns: table => new
                {
                    LogArchiveID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LogString = table.Column<string>(nullable: true),
                    LogDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Archive", x => x.LogArchiveID);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    LogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LogString = table.Column<string>(nullable: true),
                    LogDateTime = table.Column<DateTime>(nullable: false),
                    UserApiKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_Logs_Users_UserApiKey",
                        column: x => x.UserApiKey,
                        principalTable: "Users",
                        principalColumn: "ApiKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserApiKey",
                table: "Logs",
                column: "UserApiKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log_Archive");

            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
