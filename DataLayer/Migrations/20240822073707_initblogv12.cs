using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class initblogv12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "kategoriId",
                table: "kullanicis");

            migrationBuilder.AddColumn<string>(
                name: "katname",
                table: "kullanicis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "katname",
                table: "kullanicis");

            migrationBuilder.AddColumn<int>(
                name: "kategoriId",
                table: "kullanicis",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
