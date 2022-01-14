using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzava",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Titula",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titula", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Turniri",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DrzavaID = table.Column<int>(type: "int", nullable: true),
                    DatumOd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatumDo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turniri", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Turniri_Drzava_DrzavaID",
                        column: x => x.DrzavaID,
                        principalTable: "Drzava",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Igraci",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitulaID = table.Column<int>(type: "int", nullable: true),
                    Ime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DrzavaID = table.Column<int>(type: "int", nullable: true),
                    BlitzRating = table.Column<int>(type: "int", nullable: false),
                    ClassicalRating = table.Column<int>(type: "int", nullable: false),
                    RapidRating = table.Column<int>(type: "int", nullable: false),
                    TurnirID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igraci", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Igraci_Drzava_DrzavaID",
                        column: x => x.DrzavaID,
                        principalTable: "Drzava",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Igraci_Titula_TitulaID",
                        column: x => x.TitulaID,
                        principalTable: "Titula",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Igraci_Turniri_TurnirID",
                        column: x => x.TurnirID,
                        principalTable: "Turniri",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Partije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeliID = table.Column<int>(type: "int", nullable: true),
                    CrniID = table.Column<int>(type: "int", nullable: true),
                    Ishod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojPoteza = table.Column<int>(type: "int", nullable: false),
                    Runda = table.Column<int>(type: "int", nullable: false),
                    Notacija = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    TurnirID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partije", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Partije_Igraci_BeliID",
                        column: x => x.BeliID,
                        principalTable: "Igraci",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partije_Igraci_CrniID",
                        column: x => x.CrniID,
                        principalTable: "Igraci",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partije_Turniri_TurnirID",
                        column: x => x.TurnirID,
                        principalTable: "Turniri",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ucesnici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mesto = table.Column<int>(type: "int", nullable: false),
                    Bodovi = table.Column<int>(type: "int", nullable: false),
                    TurnirID = table.Column<int>(type: "int", nullable: true),
                    IgracID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucesnici", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ucesnici_Igraci_IgracID",
                        column: x => x.IgracID,
                        principalTable: "Igraci",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ucesnici_Turniri_TurnirID",
                        column: x => x.TurnirID,
                        principalTable: "Turniri",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Igraci_DrzavaID",
                table: "Igraci",
                column: "DrzavaID");

            migrationBuilder.CreateIndex(
                name: "IX_Igraci_TitulaID",
                table: "Igraci",
                column: "TitulaID");

            migrationBuilder.CreateIndex(
                name: "IX_Igraci_TurnirID",
                table: "Igraci",
                column: "TurnirID");

            migrationBuilder.CreateIndex(
                name: "IX_Partije_BeliID",
                table: "Partije",
                column: "BeliID");

            migrationBuilder.CreateIndex(
                name: "IX_Partije_CrniID",
                table: "Partije",
                column: "CrniID");

            migrationBuilder.CreateIndex(
                name: "IX_Partije_TurnirID",
                table: "Partije",
                column: "TurnirID");

            migrationBuilder.CreateIndex(
                name: "IX_Turniri_DrzavaID",
                table: "Turniri",
                column: "DrzavaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ucesnici_IgracID",
                table: "Ucesnici",
                column: "IgracID");

            migrationBuilder.CreateIndex(
                name: "IX_Ucesnici_TurnirID",
                table: "Ucesnici",
                column: "TurnirID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partije");

            migrationBuilder.DropTable(
                name: "Ucesnici");

            migrationBuilder.DropTable(
                name: "Igraci");

            migrationBuilder.DropTable(
                name: "Titula");

            migrationBuilder.DropTable(
                name: "Turniri");

            migrationBuilder.DropTable(
                name: "Drzava");
        }
    }
}
