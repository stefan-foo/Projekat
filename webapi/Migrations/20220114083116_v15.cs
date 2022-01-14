using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class v15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partije_Igraci_BeliIgracID",
                table: "Partije");

            migrationBuilder.DropForeignKey(
                name: "FK_Partije_Igraci_CrniIgracID",
                table: "Partije");

            migrationBuilder.DropIndex(
                name: "IX_Partije_CrniIgracID",
                table: "Partije");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Turniri",
                newName: "TurnirID");

            migrationBuilder.RenameColumn(
                name: "CrniIgracID",
                table: "Partije",
                newName: "DrzavaID");

            migrationBuilder.RenameColumn(
                name: "BeliIgracID",
                table: "Partije",
                newName: "CrniID");

            migrationBuilder.RenameIndex(
                name: "IX_Partije_BeliIgracID",
                table: "Partije",
                newName: "IX_Partije_CrniID");

            migrationBuilder.AlterColumn<string>(
                name: "Notacija",
                table: "Partije",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BeliID",
                table: "Partije",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partije_BeliID",
                table: "Partije",
                column: "BeliID");

            migrationBuilder.AddForeignKey(
                name: "FK_Partije_Igraci_BeliID",
                table: "Partije",
                column: "BeliID",
                principalTable: "Igraci",
                principalColumn: "IgracID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partije_Igraci_CrniID",
                table: "Partije",
                column: "CrniID",
                principalTable: "Igraci",
                principalColumn: "IgracID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partije_Igraci_BeliID",
                table: "Partije");

            migrationBuilder.DropForeignKey(
                name: "FK_Partije_Igraci_CrniID",
                table: "Partije");

            migrationBuilder.DropIndex(
                name: "IX_Partije_BeliID",
                table: "Partije");

            migrationBuilder.DropColumn(
                name: "BeliID",
                table: "Partije");

            migrationBuilder.RenameColumn(
                name: "TurnirID",
                table: "Turniri",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "DrzavaID",
                table: "Partije",
                newName: "CrniIgracID");

            migrationBuilder.RenameColumn(
                name: "CrniID",
                table: "Partije",
                newName: "BeliIgracID");

            migrationBuilder.RenameIndex(
                name: "IX_Partije_CrniID",
                table: "Partije",
                newName: "IX_Partije_BeliIgracID");

            migrationBuilder.AlterColumn<string>(
                name: "Notacija",
                table: "Partije",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.CreateIndex(
                name: "IX_Partije_CrniIgracID",
                table: "Partije",
                column: "CrniIgracID");

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
    }
}
