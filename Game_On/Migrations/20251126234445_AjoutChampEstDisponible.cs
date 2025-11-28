using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Game_On.Migrations
{
    /// <inheritdoc />
    public partial class AjoutChampEstDisponible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstDisponible",
                table: "Jeu",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Jeu",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EstDisponible", "NomJeu" },
                values: new object[] { true, "Sudoku Master" });

            migrationBuilder.InsertData(
                table: "Jeu",
                columns: new[] { "Id", "EstDisponible", "GameCategoryId", "NomJeu" },
                values: new object[,]
                {
                    { 2, false, 2, "Poker Pro" },
                    { 3, false, 2, "Quiz Mania" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jeu",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Jeu",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "EstDisponible",
                table: "Jeu");

            migrationBuilder.UpdateData(
                table: "Jeu",
                keyColumn: "Id",
                keyValue: 1,
                column: "NomJeu",
                value: "Sudoku");
        }
    }
}
