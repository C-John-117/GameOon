using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Game_On.Migrations
{
    /// <inheritdoc />
    public partial class AjoutCodeJeu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeJeu",
                table: "Jeu",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Jeu",
                keyColumn: "Id",
                keyValue: 1,
                column: "CodeJeu",
                value: "SUDOKU");

            migrationBuilder.UpdateData(
                table: "Jeu",
                keyColumn: "Id",
                keyValue: 2,
                column: "CodeJeu",
                value: "POKER");

            migrationBuilder.UpdateData(
                table: "Jeu",
                keyColumn: "Id",
                keyValue: 3,
                column: "CodeJeu",
                value: "QUIZ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeJeu",
                table: "Jeu");
        }
    }
}
