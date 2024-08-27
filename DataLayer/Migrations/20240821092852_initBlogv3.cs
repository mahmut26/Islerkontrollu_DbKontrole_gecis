using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class initBlogv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_kategoris_makales_MakaleId",
                table: "kategoris");

            migrationBuilder.DropIndex(
                name: "IX_kategoris_MakaleId",
                table: "kategoris");

            migrationBuilder.DropColumn(
                name: "MakaleId",
                table: "kategoris");

            migrationBuilder.AddColumn<int>(
                name: "kategoriId",
                table: "makales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_makales_kategoriId",
                table: "makales",
                column: "kategoriId");

            migrationBuilder.AddForeignKey(
                name: "FK_makales_kategoris_kategoriId",
                table: "makales",
                column: "kategoriId",
                principalTable: "kategoris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_makales_kategoris_kategoriId",
                table: "makales");

            migrationBuilder.DropIndex(
                name: "IX_makales_kategoriId",
                table: "makales");

            migrationBuilder.DropColumn(
                name: "kategoriId",
                table: "makales");

            migrationBuilder.AddColumn<int>(
                name: "MakaleId",
                table: "kategoris",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_kategoris_MakaleId",
                table: "kategoris",
                column: "MakaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_kategoris_makales_MakaleId",
                table: "kategoris",
                column: "MakaleId",
                principalTable: "makales",
                principalColumn: "Id");
        }
    }
}
