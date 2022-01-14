using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrojRundi",
                table: "Turniri",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxIgraca",
                table: "Turniri",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TimeControl",
                table: "Turniri",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojRundi",
                table: "Turniri");

            migrationBuilder.DropColumn(
                name: "MaxIgraca",
                table: "Turniri");

            migrationBuilder.DropColumn(
                name: "TimeControl",
                table: "Turniri");
        }
    }
}
