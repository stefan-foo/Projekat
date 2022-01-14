using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Igraci_Turniri_TurnirID",
                table: "Igraci");

            migrationBuilder.DropIndex(
                name: "IX_Igraci_TurnirID",
                table: "Igraci");

            migrationBuilder.DropColumn(
                name: "TurnirID",
                table: "Igraci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TurnirID",
                table: "Igraci",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Igraci_TurnirID",
                table: "Igraci",
                column: "TurnirID");

            migrationBuilder.AddForeignKey(
                name: "FK_Igraci_Turniri_TurnirID",
                table: "Igraci",
                column: "TurnirID",
                principalTable: "Turniri",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
