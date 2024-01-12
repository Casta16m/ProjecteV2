using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBSql.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    Nom = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArtistaNom = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => new { x.Nom, x.data, x.ArtistaNom });
                });

            migrationBuilder.CreateTable(
                name: "Artistes",
                columns: table => new
                {
                    NomArtista = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnyNaixement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artistes", x => x.NomArtista);
                });

            migrationBuilder.CreateTable(
                name: "Cançons",
                columns: table => new
                {
                    UID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cançons", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "Extensio",
                columns: table => new
                {
                    Nom = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extensio", x => x.Nom);
                });

            migrationBuilder.CreateTable(
                name: "Grups",
                columns: table => new
                {
                    NomGrup = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grups", x => x.NomGrup);
                });

            migrationBuilder.CreateTable(
                name: "conteAlbum",
                columns: table => new
                {
                    UID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArtistaNom = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conteAlbum", x => new { x.UID, x.Nom, x.data });
                    table.ForeignKey(
                        name: "FK_conteAlbum_Album_Nom_data_ArtistaNom",
                        columns: x => new { x.Nom, x.data, x.ArtistaNom },
                        principalTable: "Album",
                        principalColumns: new[] { "Nom", "data", "ArtistaNom" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_conteAlbum_Cançons_UID",
                        column: x => x.UID,
                        principalTable: "Cançons",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Format",
                columns: table => new
                {
                    UID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Format", x => new { x.UID, x.Nom });
                    table.ForeignKey(
                        name: "FK_Format_Cançons_UID",
                        column: x => x.UID,
                        principalTable: "Cançons",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Format_Extensio_Nom",
                        column: x => x.Nom,
                        principalTable: "Extensio",
                        principalColumn: "Nom",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistaGrup",
                columns: table => new
                {
                    ArtistasNomArtista = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GrupsNomGrup = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistaGrup", x => new { x.ArtistasNomArtista, x.GrupsNomGrup });
                    table.ForeignKey(
                        name: "FK_ArtistaGrup_Artistes_ArtistasNomArtista",
                        column: x => x.ArtistasNomArtista,
                        principalTable: "Artistes",
                        principalColumn: "NomArtista",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistaGrup_Grups_GrupsNomGrup",
                        column: x => x.GrupsNomGrup,
                        principalTable: "Grups",
                        principalColumn: "NomGrup",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistaGrup_GrupsNomGrup",
                table: "ArtistaGrup",
                column: "GrupsNomGrup");

            migrationBuilder.CreateIndex(
                name: "IX_conteAlbum_data",
                table: "conteAlbum",
                column: "data");

            migrationBuilder.CreateIndex(
                name: "IX_conteAlbum_Nom",
                table: "conteAlbum",
                column: "Nom");

            migrationBuilder.CreateIndex(
                name: "IX_conteAlbum_Nom_data_ArtistaNom",
                table: "conteAlbum",
                columns: new[] { "Nom", "data", "ArtistaNom" });

            migrationBuilder.CreateIndex(
                name: "IX_conteAlbum_UID",
                table: "conteAlbum",
                column: "UID");

            migrationBuilder.CreateIndex(
                name: "IX_Format_Nom",
                table: "Format",
                column: "Nom");

            migrationBuilder.CreateIndex(
                name: "IX_Format_UID",
                table: "Format",
                column: "UID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistaGrup");

            migrationBuilder.DropTable(
                name: "conteAlbum");

            migrationBuilder.DropTable(
                name: "Format");

            migrationBuilder.DropTable(
                name: "Artistes");

            migrationBuilder.DropTable(
                name: "Grups");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "Cançons");

            migrationBuilder.DropTable(
                name: "Extensio");
        }
    }
}
