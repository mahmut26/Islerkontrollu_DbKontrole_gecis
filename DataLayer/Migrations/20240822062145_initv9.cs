using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class initv9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_resims_makales_MakaleId",
                table: "resims");

            migrationBuilder.DropTable(
                name: "KategoriKullanici");

            migrationBuilder.DropIndex(
                name: "IX_resims_MakaleId",
                table: "resims");

            migrationBuilder.DropColumn(
                name: "MakaleId",
                table: "resims");

            migrationBuilder.AddColumn<int>(
                name: "KullaniciId",
                table: "kategoris",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_kategoris_KullaniciId",
                table: "kategoris",
                column: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_kategoris_kullanicis_KullaniciId",
                table: "kategoris",
                column: "KullaniciId",
                principalTable: "kullanicis",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_kategoris_kullanicis_KullaniciId",
                table: "kategoris");

            migrationBuilder.DropIndex(
                name: "IX_kategoris_KullaniciId",
                table: "kategoris");

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "kategoris");

            migrationBuilder.AddColumn<int>(
                name: "MakaleId",
                table: "resims",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KategoriKullanici",
                columns: table => new
                {
                    KategorilerId = table.Column<int>(type: "int", nullable: false),
                    KullanicilarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategoriKullanici", x => new { x.KategorilerId, x.KullanicilarId });
                    table.ForeignKey(
                        name: "FK_KategoriKullanici_kategoris_KategorilerId",
                        column: x => x.KategorilerId,
                        principalTable: "kategoris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KategoriKullanici_kullanicis_KullanicilarId",
                        column: x => x.KullanicilarId,
                        principalTable: "kullanicis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_resims_MakaleId",
                table: "resims",
                column: "MakaleId");

            migrationBuilder.CreateIndex(
                name: "IX_KategoriKullanici_KullanicilarId",
                table: "KategoriKullanici",
                column: "KullanicilarId");

            migrationBuilder.AddForeignKey(
                name: "FK_resims_makales_MakaleId",
                table: "resims",
                column: "MakaleId",
                principalTable: "makales",
                principalColumn: "Id");
        }
    }
}
