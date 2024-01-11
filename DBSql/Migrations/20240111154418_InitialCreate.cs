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
                    NomAlbum = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArtistaNom = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => new { x.NomAlbum, x.data, x.ArtistaNom });
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
                    NomExtensio = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extensio", x => x.NomExtensio);
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
                    NomAlbum = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArtistaNom = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conteAlbum", x => new { x.UID, x.NomAlbum, x.data });
                    table.ForeignKey(
                        name: "FK_conteAlbum_Album_NomAlbum_data_ArtistaNom",
                        columns: x => new { x.NomAlbum, x.data, x.ArtistaNom },
                        principalTable: "Album",
                        principalColumns: new[] { "NomAlbum", "data", "ArtistaNom" },
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
                    NomFormat = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Format", x => new { x.UID, x.NomFormat });
                    table.ForeignKey(
                        name: "FK_Format_Cançons_UID",
                        column: x => x.UID,
                        principalTable: "Cançons",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Format_Extensio_NomFormat",
                        column: x => x.NomFormat,
                        principalTable: "Extensio",
                        principalColumn: "NomExtensio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pertany",
                columns: table => new
                {
                    NomGrup = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomArtista = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataInici = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pertany", x => new { x.NomGrup, x.NomArtista });
                    table.ForeignKey(
                        name: "FK_Pertany_Artistes_NomArtista",
                        column: x => x.NomArtista,
                        principalTable: "Artistes",
                        principalColumn: "NomArtista",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pertany_Grups_NomGrup",
                        column: x => x.NomGrup,
                        principalTable: "Grups",
                        principalColumn: "NomGrup",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_conteAlbum_data",
                table: "conteAlbum",
                column: "data");

            migrationBuilder.CreateIndex(
                name: "IX_conteAlbum_NomAlbum",
                table: "conteAlbum",
                column: "NomAlbum");

            migrationBuilder.CreateIndex(
                name: "IX_conteAlbum_NomAlbum_data_ArtistaNom",
                table: "conteAlbum",
                columns: new[] { "NomAlbum", "data", "ArtistaNom" });

            migrationBuilder.CreateIndex(
                name: "IX_conteAlbum_UID",
                table: "conteAlbum",
                column: "UID");

            migrationBuilder.CreateIndex(
                name: "IX_Format_NomFormat",
                table: "Format",
                column: "NomFormat");

            migrationBuilder.CreateIndex(
                name: "IX_Format_UID",
                table: "Format",
                column: "UID");

            migrationBuilder.CreateIndex(
                name: "IX_Pertany_NomArtista",
                table: "Pertany",
                column: "NomArtista");

            migrationBuilder.CreateIndex(
                name: "IX_Pertany_NomGrup",
                table: "Pertany",
                column: "NomGrup");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "conteAlbum");

            migrationBuilder.DropTable(
                name: "Format");

            migrationBuilder.DropTable(
                name: "Pertany");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "Cançons");

            migrationBuilder.DropTable(
                name: "Extensio");

            migrationBuilder.DropTable(
                name: "Artistes");

            migrationBuilder.DropTable(
                name: "Grups");
        }
    }
}
