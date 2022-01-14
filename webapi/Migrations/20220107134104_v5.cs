using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Igraci_Drzava_DrzavaID",
                table: "Igraci");

            migrationBuilder.DropForeignKey(
                name: "FK_Turniri_Drzava_DrzavaID",
                table: "Turniri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drzava",
                table: "Drzava");

            migrationBuilder.RenameTable(
                name: "Drzava",
                newName: "Drzave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drzave",
                table: "Drzave",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Igraci_Drzave_DrzavaID",
                table: "Igraci",
                column: "DrzavaID",
                principalTable: "Drzave",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turniri_Drzave_DrzavaID",
                table: "Turniri",
                column: "DrzavaID",
                principalTable: "Drzave",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Igraci_Drzave_DrzavaID",
                table: "Igraci");

            migrationBuilder.DropForeignKey(
                name: "FK_Turniri_Drzave_DrzavaID",
                table: "Turniri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drzave",
                table: "Drzave");

            migrationBuilder.RenameTable(
                name: "Drzave",
                newName: "Drzava");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drzava",
                table: "Drzava",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Igraci_Drzava_DrzavaID",
                table: "Igraci",
                column: "DrzavaID",
                principalTable: "Drzava",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turniri_Drzava_DrzavaID",
                table: "Turniri",
                column: "DrzavaID",
                principalTable: "Drzava",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
