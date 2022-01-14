using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turniri_Drzave_DrzavaID",
                table: "Turniri");

            migrationBuilder.AlterColumn<int>(
                name: "DrzavaID",
                table: "Turniri",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Turniri_Drzave_DrzavaID",
                table: "Turniri",
                column: "DrzavaID",
                principalTable: "Drzave",
                principalColumn: "DrzavaID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turniri_Drzave_DrzavaID",
                table: "Turniri");

            migrationBuilder.AlterColumn<int>(
                name: "DrzavaID",
                table: "Turniri",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Turniri_Drzave_DrzavaID",
                table: "Turniri",
                column: "DrzavaID",
                principalTable: "Drzave",
                principalColumn: "DrzavaID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
