using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DistSysACW.Migrations
{
    public partial class LoggingWithID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs_Archives",
                table: "Logs_Archives");

            migrationBuilder.AlterColumn<string>(
                name: "LogApiKey",
                table: "Logs_Archives",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "LogID",
                table: "Logs_Archives",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs_Archives",
                table: "Logs_Archives",
                column: "LogID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs_Archives",
                table: "Logs_Archives");

            migrationBuilder.DropColumn(
                name: "LogID",
                table: "Logs_Archives");

            migrationBuilder.AlterColumn<string>(
                name: "LogApiKey",
                table: "Logs_Archives",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs_Archives",
                table: "Logs_Archives",
                column: "LogApiKey");
        }
    }
}
