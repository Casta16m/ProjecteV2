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
                name: "Artistes",
                columns: table => new
                {
                    NomArtista = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AnyNaixement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artistes", x => x.NomArtista);
                });

            migrationBuilder.CreateTable(
                name: "Extensio",
                columns: table => new
                {
                    NomExtensio = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extensio", x => x.NomExtensio);
                });

            migrationBuilder.CreateTable(
                name: "Grups",
                columns: table => new
                {
                    NomGrup = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grups", x => x.NomGrup);
                });

            migrationBuilder.CreateTable(
                name: "Instrument",
                columns: table => new
                {
                    Nom = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrument", x => x.Nom);
                });

            migrationBuilder.CreateTable(
                name: "Llista",
                columns: table => new
                {
                    Nom = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ID_MAC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Llista", x => new { x.Nom, x.ID_MAC });
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    UID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NomSong = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    SongOriginal = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Genere = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.UID);
                    table.ForeignKey(
                        name: "FK_Songs_Songs_SongOriginal",
                        column: x => x.SongOriginal,
                        principalTable: "Songs",
                        principalColumn: "UID");
                });

            migrationBuilder.CreateTable(
                name: "ArtistaGrup",
                columns: table => new
                {
                    GrupsNomGrup = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    artistesNomArtista = table.Column<string>(type: "nvarchar(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistaGrup", x => new { x.GrupsNomGrup, x.artistesNomArtista });
                    table.ForeignKey(
                        name: "FK_ArtistaGrup_Artistes_artistesNomArtista",
                        column: x => x.artistesNomArtista,
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

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    NomAlbum = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UIDSong = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => new { x.NomAlbum, x.UIDSong, x.data });
                    table.ForeignKey(
                        name: "FK_Album_Songs_UIDSong",
                        column: x => x.UIDSong,
                        principalTable: "Songs",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtensioSong",
                columns: table => new
                {
                    extensioNomExtensio = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    songsUID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtensioSong", x => new { x.extensioNomExtensio, x.songsUID });
                    table.ForeignKey(
                        name: "FK_ExtensioSong_Extensio_extensioNomExtensio",
                        column: x => x.extensioNomExtensio,
                        principalTable: "Extensio",
                        principalColumn: "NomExtensio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtensioSong_Songs_songsUID",
                        column: x => x.songsUID,
                        principalTable: "Songs",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LlistaSong",
                columns: table => new
                {
                    songsUID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    llistaNom = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    llistaID_MAC = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LlistaSong", x => new { x.songsUID, x.llistaNom, x.llistaID_MAC });
                    table.ForeignKey(
                        name: "FK_LlistaSong_Llista_llistaNom_llistaID_MAC",
                        columns: x => new { x.llistaNom, x.llistaID_MAC },
                        principalTable: "Llista",
                        principalColumns: new[] { "Nom", "ID_MAC" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LlistaSong_Songs_songsUID",
                        column: x => x.songsUID,
                        principalTable: "Songs",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participa",
                columns: table => new
                {
                    UID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomArtista = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    NomGrup = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    NomInstrument = table.Column<string>(type: "nvarchar(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participa", x => new { x.UID, x.NomArtista, x.NomGrup, x.NomInstrument });
                    table.ForeignKey(
                        name: "FK_Participa_Artistes_NomArtista",
                        column: x => x.NomArtista,
                        principalTable: "Artistes",
                        principalColumn: "NomArtista",
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
                    table.ForeignKey(
                        name: "FK_Participa_Songs_UID",
                        column: x => x.UID,
                        principalTable: "Songs",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Album_UIDSong",
                table: "Album",
                column: "UIDSong");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistaGrup_artistesNomArtista",
                table: "ArtistaGrup",
                column: "artistesNomArtista");

            migrationBuilder.CreateIndex(
                name: "IX_ExtensioSong_songsUID",
                table: "ExtensioSong",
                column: "songsUID");

            migrationBuilder.CreateIndex(
                name: "IX_LlistaSong_llistaNom_llistaID_MAC",
                table: "LlistaSong",
                columns: new[] { "llistaNom", "llistaID_MAC" });

            migrationBuilder.CreateIndex(
                name: "IX_Participa_NomArtista",
                table: "Participa",
                column: "NomArtista");

            migrationBuilder.CreateIndex(
                name: "IX_Participa_NomGrup",
                table: "Participa",
                column: "NomGrup");

            migrationBuilder.CreateIndex(
                name: "IX_Participa_NomInstrument",
                table: "Participa",
                column: "NomInstrument");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_SongOriginal",
                table: "Songs",
                column: "SongOriginal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "ArtistaGrup");

            migrationBuilder.DropTable(
                name: "ExtensioSong");

            migrationBuilder.DropTable(
                name: "LlistaSong");

            migrationBuilder.DropTable(
                name: "Participa");

            migrationBuilder.DropTable(
                name: "Extensio");

            migrationBuilder.DropTable(
                name: "Llista");

            migrationBuilder.DropTable(
                name: "Artistes");

            migrationBuilder.DropTable(
                name: "Grups");

            migrationBuilder.DropTable(
                name: "Instrument");

            migrationBuilder.DropTable(
                name: "Songs");
        }
    }
}
