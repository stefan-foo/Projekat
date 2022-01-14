using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partije_Igraci_BeliID",
                table: "Partije");

            migrationBuilder.DropForeignKey(
                name: "FK_Partije_Igraci_CrniID",
                table: "Partije");

            migrationBuilder.RenameColumn(
                name: "CrniID",
                table: "Partije",
                newName: "CrniIgracID");

            migrationBuilder.RenameColumn(
                name: "BeliID",
                table: "Partije",
                newName: "BeliIgracID");

            migrationBuilder.RenameIndex(
                name: "IX_Partije_CrniID",
                table: "Partije",
                newName: "IX_Partije_CrniIgracID");

            migrationBuilder.RenameIndex(
                name: "IX_Partije_BeliID",
                table: "Partije",
                newName: "IX_Partije_BeliIgracID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Igraci",
                newName: "IgracID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Drzave",
                newName: "DrzavaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Partije_Igraci_BeliIgracID",
                table: "Partije",
                column: "BeliIgracID",
                principalTable: "Igraci",
                principalColumn: "IgracID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partije_Igraci_CrniIgracID",
                table: "Partije",
                column: "CrniIgracID",
                principalTable: "Igraci",
                principalColumn: "IgracID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partije_Igraci_BeliIgracID",
                table: "Partije");

            migrationBuilder.DropForeignKey(
                name: "FK_Partije_Igraci_CrniIgracID",
                table: "Partije");

            migrationBuilder.RenameColumn(
                name: "CrniIgracID",
                table: "Partije",
                newName: "CrniID");

            migrationBuilder.RenameColumn(
                name: "BeliIgracID",
                table: "Partije",
                newName: "BeliID");

            migrationBuilder.RenameIndex(
                name: "IX_Partije_CrniIgracID",
                table: "Partije",
                newName: "IX_Partije_CrniID");

            migrationBuilder.RenameIndex(
                name: "IX_Partije_BeliIgracID",
                table: "Partije",
                newName: "IX_Partije_BeliID");

            migrationBuilder.RenameColumn(
                name: "IgracID",
                table: "Igraci",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "DrzavaID",
                table: "Drzave",
                newName: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Partije_Igraci_BeliID",
                table: "Partije",
                column: "BeliID",
                principalTable: "Igraci",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partije_Igraci_CrniID",
                table: "Partije",
                column: "CrniID",
                principalTable: "Igraci",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
