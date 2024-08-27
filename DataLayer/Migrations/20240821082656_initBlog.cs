using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class initBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kullanicis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kullanicis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "makales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icerik = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_makales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kategoris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakaleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kategoris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kategoris_makales_MakaleId",
                        column: x => x.MakaleId,
                        principalTable: "makales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "resims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakaleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_resims_makales_MakaleId",
                        column: x => x.MakaleId,
                        principalTable: "makales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "yazars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakaleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_yazars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_yazars_makales_MakaleId",
                        column: x => x.MakaleId,
                        principalTable: "makales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_kategoris_MakaleId",
                table: "kategoris",
                column: "MakaleId");

            migrationBuilder.CreateIndex(
                name: "IX_resims_MakaleId",
                table: "resims",
                column: "MakaleId");

            migrationBuilder.CreateIndex(
                name: "IX_yazars_MakaleId",
                table: "yazars",
                column: "MakaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kategoris");

            migrationBuilder.DropTable(
                name: "kullanicis");

            migrationBuilder.DropTable(
                name: "resims");

            migrationBuilder.DropTable(
                name: "yazars");

            migrationBuilder.DropTable(
                name: "makales");
        }
    }
}
