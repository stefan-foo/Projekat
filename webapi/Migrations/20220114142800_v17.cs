using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class v17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ucesnici_Igraci_IgracID",
                table: "Ucesnici");

            migrationBuilder.DropForeignKey(
                name: "FK_Ucesnici_Turniri_TurnirID",
                table: "Ucesnici");

            migrationBuilder.AlterColumn<int>(
                name: "TurnirID",
                table: "Ucesnici",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IgracID",
                table: "Ucesnici",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ucesnici_Igraci_IgracID",
                table: "Ucesnici",
                column: "IgracID",
                principalTable: "Igraci",
                principalColumn: "IgracID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ucesnici_Turniri_TurnirID",
                table: "Ucesnici",
                column: "TurnirID",
                principalTable: "Turniri",
                principalColumn: "TurnirID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ucesnici_Igraci_IgracID",
                table: "Ucesnici");

            migrationBuilder.DropForeignKey(
                name: "FK_Ucesnici_Turniri_TurnirID",
                table: "Ucesnici");

            migrationBuilder.AlterColumn<int>(
                name: "TurnirID",
                table: "Ucesnici",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IgracID",
                table: "Ucesnici",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ucesnici_Igraci_IgracID",
                table: "Ucesnici",
                column: "IgracID",
                principalTable: "Igraci",
                principalColumn: "IgracID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ucesnici_Turniri_TurnirID",
                table: "Ucesnici",
                column: "TurnirID",
                principalTable: "Turniri",
                principalColumn: "TurnirID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
