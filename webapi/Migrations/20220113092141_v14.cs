using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partije_Turniri_TurnirID",
                table: "Partije");

            migrationBuilder.AddForeignKey(
                name: "FK_Partije_Turniri_TurnirID",
                table: "Partije",
                column: "TurnirID",
                principalTable: "Turniri",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partije_Turniri_TurnirID",
                table: "Partije");

            migrationBuilder.AddForeignKey(
                name: "FK_Partije_Turniri_TurnirID",
                table: "Partije",
                column: "TurnirID",
                principalTable: "Turniri",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
