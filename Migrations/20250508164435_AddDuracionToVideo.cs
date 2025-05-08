using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG_BACK.Migrations
{
    /// <inheritdoc />
    public partial class AddDuracionToVideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Duracion",
                table: "Video",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duracion",
                table: "Video");
        }
    }
}
