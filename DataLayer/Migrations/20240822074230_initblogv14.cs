using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class initblogv14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "katname",
                table: "kullanicis");

            migrationBuilder.AddColumn<int>(
                name: "katid",
                table: "kullanicis",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "katid",
                table: "kullanicis");

            migrationBuilder.AddColumn<string>(
                name: "katname",
                table: "kullanicis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
