using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG_BACK.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdPreferenciaFromUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Preferencias_IdPreferencia",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "MinutoFin",
                table: "MarcadorVideo");

            migrationBuilder.RenameColumn(
                name: "IdPreferencia",
                table: "Usuario",
                newName: "PreferenciasIdPreferencia");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_IdPreferencia",
                table: "Usuario",
                newName: "IX_Usuario_PreferenciasIdPreferencia");

            migrationBuilder.RenameColumn(
                name: "MinutoInicio",
                table: "MarcadorVideo",
                newName: "MinutoImportante");

            migrationBuilder.AddColumn<int>(
                name: "NumReportes",
                table: "Video",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCurso",
                table: "Quiz",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReportesVideo",
                columns: table => new
                {
                    IdReporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdVideo = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Motivo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: true),
                    VideoIdVideo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportesVideo", x => x.IdReporte);
                    table.ForeignKey(
                        name: "FK_ReportesVideo_Usuario_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "FK_ReportesVideo_Video_VideoIdVideo",
                        column: x => x.VideoIdVideo,
                        principalTable: "Video",
                        principalColumn: "IdVideo");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_IdCurso",
                table: "Quiz",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_ReportesVideo_UsuarioIdUsuario",
                table: "ReportesVideo",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ReportesVideo_VideoIdVideo",
                table: "ReportesVideo",
                column: "VideoIdVideo");

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_Curso_IdCurso",
                table: "Quiz",
                column: "IdCurso",
                principalTable: "Curso",
                principalColumn: "IdCurso");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Preferencias_PreferenciasIdPreferencia",
                table: "Usuario",
                column: "PreferenciasIdPreferencia",
                principalTable: "Preferencias",
                principalColumn: "IdPreferencia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_Curso_IdCurso",
                table: "Quiz");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Preferencias_PreferenciasIdPreferencia",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "ReportesVideo");

            migrationBuilder.DropIndex(
                name: "IX_Quiz_IdCurso",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "NumReportes",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "IdCurso",
                table: "Quiz");

            migrationBuilder.RenameColumn(
                name: "PreferenciasIdPreferencia",
                table: "Usuario",
                newName: "IdPreferencia");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_PreferenciasIdPreferencia",
                table: "Usuario",
                newName: "IX_Usuario_IdPreferencia");

            migrationBuilder.RenameColumn(
                name: "MinutoImportante",
                table: "MarcadorVideo",
                newName: "MinutoInicio");

            migrationBuilder.AddColumn<decimal>(
                name: "MinutoFin",
                table: "MarcadorVideo",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Preferencias_IdPreferencia",
                table: "Usuario",
                column: "IdPreferencia",
                principalTable: "Preferencias",
                principalColumn: "IdPreferencia");
        }
    }
}
