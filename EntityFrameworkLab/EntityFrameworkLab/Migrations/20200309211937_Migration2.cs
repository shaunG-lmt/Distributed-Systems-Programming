using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkLab.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Middle_Name",
                table: "People",
                newName: "Middle_Names");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Middle_Names",
                table: "People",
                newName: "Middle_Name");
        }

        // Labs say that it should of tried to drop the column. NOTE this for coursework.
    }
}
