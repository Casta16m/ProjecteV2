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
                    Nom = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnyNaixement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artistes", x => x.Nom);
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
                name: "Formats",
                columns: table => new
                {
                    Nom = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formats", x => x.Nom);
                });

            migrationBuilder.CreateTable(
                name: "es",
                columns: table => new
                {
                    UID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_es", x => new { x.UID, x.Nom });
                    table.ForeignKey(
                        name: "FK_es_Cançons_UID",
                        column: x => x.UID,
                        principalTable: "Cançons",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_es_Formats_Nom",
                        column: x => x.Nom,
                        principalTable: "Formats",
                        principalColumn: "Nom",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_es_Nom",
                table: "es",
                column: "Nom");

            migrationBuilder.CreateIndex(
                name: "IX_es_UID",
                table: "es",
                column: "UID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artistes");

            migrationBuilder.DropTable(
                name: "es");

            migrationBuilder.DropTable(
                name: "Cançons");

            migrationBuilder.DropTable(
                name: "Formats");
        }
    }
}
