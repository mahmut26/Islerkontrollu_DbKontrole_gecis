using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class initBlogv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_makales_kategoris_kategoriId",
                table: "makales");

            migrationBuilder.RenameColumn(
                name: "kategoriId",
                table: "makales",
                newName: "KategoriId");

            migrationBuilder.RenameIndex(
                name: "IX_makales_kategoriId",
                table: "makales",
                newName: "IX_makales_KategoriId");

            migrationBuilder.AddForeignKey(
                name: "FK_makales_kategoris_KategoriId",
                table: "makales",
                column: "KategoriId",
                principalTable: "kategoris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_makales_kategoris_KategoriId",
                table: "makales");

            migrationBuilder.RenameColumn(
                name: "KategoriId",
                table: "makales",
                newName: "kategoriId");

            migrationBuilder.RenameIndex(
                name: "IX_makales_KategoriId",
                table: "makales",
                newName: "IX_makales_kategoriId");

            migrationBuilder.AddForeignKey(
                name: "FK_makales_kategoris_kategoriId",
                table: "makales",
                column: "kategoriId",
                principalTable: "kategoris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
