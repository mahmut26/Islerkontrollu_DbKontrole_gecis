using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class initBlogv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_yazars_makales_MakaleId",
                table: "yazars");

            migrationBuilder.DropIndex(
                name: "IX_yazars_MakaleId",
                table: "yazars");

            migrationBuilder.DropColumn(
                name: "MakaleId",
                table: "yazars");

            migrationBuilder.AddColumn<int>(
                name: "YazarId",
                table: "makales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_makales_YazarId",
                table: "makales",
                column: "YazarId");

            migrationBuilder.AddForeignKey(
                name: "FK_makales_yazars_YazarId",
                table: "makales",
                column: "YazarId",
                principalTable: "yazars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_makales_yazars_YazarId",
                table: "makales");

            migrationBuilder.DropIndex(
                name: "IX_makales_YazarId",
                table: "makales");

            migrationBuilder.DropColumn(
                name: "YazarId",
                table: "makales");

            migrationBuilder.AddColumn<int>(
                name: "MakaleId",
                table: "yazars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_yazars_MakaleId",
                table: "yazars",
                column: "MakaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_yazars_makales_MakaleId",
                table: "yazars",
                column: "MakaleId",
                principalTable: "makales",
                principalColumn: "Id");
        }
    }
}
