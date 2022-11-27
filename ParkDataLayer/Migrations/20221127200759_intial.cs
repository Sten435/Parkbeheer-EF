using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Huurder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huurder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Park",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Locatie = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Park", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Huis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Straat = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Nr = table.Column<int>(type: "int", nullable: false),
                    Actief = table.Column<bool>(type: "bit", nullable: false),
                    ParkId = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Huis_Park_ParkId",
                        column: x => x.ParkId,
                        principalTable: "Park",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HuurderContract",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    StartDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EindDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aantaldagen = table.Column<int>(type: "int", nullable: false),
                    HuurderId = table.Column<int>(type: "int", nullable: false),
                    HuisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuurderContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HuurderContract_Huis_HuisId",
                        column: x => x.HuisId,
                        principalTable: "Huis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HuurderContract_Huurder_HuurderId",
                        column: x => x.HuurderId,
                        principalTable: "Huurder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Huis_ParkId",
                table: "Huis",
                column: "ParkId");

            migrationBuilder.CreateIndex(
                name: "IX_HuurderContract_HuisId",
                table: "HuurderContract",
                column: "HuisId");

            migrationBuilder.CreateIndex(
                name: "IX_HuurderContract_HuurderId",
                table: "HuurderContract",
                column: "HuurderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HuurderContract");

            migrationBuilder.DropTable(
                name: "Huis");

            migrationBuilder.DropTable(
                name: "Huurder");

            migrationBuilder.DropTable(
                name: "Park");
        }
    }
}
