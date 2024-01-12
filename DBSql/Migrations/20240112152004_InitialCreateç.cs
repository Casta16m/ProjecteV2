﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBSql.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateç : Migration
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
                name: "Instrument",
                columns: table => new
                {
                    Nom = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrument", x => x.Nom);
                });

            migrationBuilder.CreateTable(
                name: "Llista",
                columns: table => new
                {
                    Nom = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ID_MAC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Dispositiu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Llista", x => new { x.Nom, x.ID_MAC });
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
                name: "ArtistaGrup",
                columns: table => new
                {
                    artistesNomArtista = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    grupsNomGrup = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistaGrup", x => new { x.artistesNomArtista, x.grupsNomGrup });
                    table.ForeignKey(
                        name: "FK_ArtistaGrup_Artistes_artistesNomArtista",
                        column: x => x.artistesNomArtista,
                        principalTable: "Artistes",
                        principalColumn: "NomArtista",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistaGrup_Grups_grupsNomGrup",
                        column: x => x.grupsNomGrup,
                        principalTable: "Grups",
                        principalColumn: "NomGrup",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participa",
                columns: table => new
                {
                    UID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomCanço = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomArtista = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomGrup = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomInstrument = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participa", x => new { x.UID, x.NomCanço, x.NomArtista, x.NomGrup, x.NomInstrument });
                    table.ForeignKey(
                        name: "FK_Participa_Artistes_NomArtista",
                        column: x => x.NomArtista,
                        principalTable: "Artistes",
                        principalColumn: "NomArtista",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participa_Cançons_NomCanço",
                        column: x => x.NomCanço,
                        principalTable: "Cançons",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participa_Grups_NomGrup",
                        column: x => x.NomGrup,
                        principalTable: "Grups",
                        principalColumn: "NomGrup",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participa_Instrument_NomInstrument",
                        column: x => x.NomInstrument,
                        principalTable: "Instrument",
                        principalColumn: "Nom",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConteLlista",
                columns: table => new
                {
                    NomLlista = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MAC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConteLlista", x => new { x.NomLlista, x.MAC, x.UID });
                    table.ForeignKey(
                        name: "FK_ConteLlista_Cançons_UID",
                        column: x => x.UID,
                        principalTable: "Cançons",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConteLlista_Llista_NomLlista_MAC",
                        columns: x => new { x.NomLlista, x.MAC },
                        principalTable: "Llista",
                        principalColumns: new[] { "Nom", "ID_MAC" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistaGrup_grupsNomGrup",
                table: "ArtistaGrup",
                column: "grupsNomGrup");

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
                name: "IX_ConteLlista_UID",
                table: "ConteLlista",
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
                name: "IX_Participa_NomArtista",
                table: "Participa",
                column: "NomArtista");

            migrationBuilder.CreateIndex(
                name: "IX_Participa_NomCanço",
                table: "Participa",
                column: "NomCanço");

            migrationBuilder.CreateIndex(
                name: "IX_Participa_NomGrup",
                table: "Participa",
                column: "NomGrup");

            migrationBuilder.CreateIndex(
                name: "IX_Participa_NomInstrument",
                table: "Participa",
                column: "NomInstrument");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistaGrup");

            migrationBuilder.DropTable(
                name: "conteAlbum");

            migrationBuilder.DropTable(
                name: "ConteLlista");

            migrationBuilder.DropTable(
                name: "Format");

            migrationBuilder.DropTable(
                name: "Participa");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "Llista");

            migrationBuilder.DropTable(
                name: "Extensio");

            migrationBuilder.DropTable(
                name: "Artistes");

            migrationBuilder.DropTable(
                name: "Cançons");

            migrationBuilder.DropTable(
                name: "Grups");

            migrationBuilder.DropTable(
                name: "Instrument");
        }
    }
}
