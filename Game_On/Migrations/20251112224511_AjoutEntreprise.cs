using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Game_On.Migrations
{
    /// <inheritdoc />
    public partial class AjoutEntreprise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departement",
                columns: new[] { "Id", "EntrepriseId", "NomDepartement" },
                values: new object[,]
                {
                    { 1, 1, "Gestionnaires de projet" },
                    { 2, 1, "Analystes d'affaires" },
                    { 3, 1, "Analystes d'applications" },
                    { 4, 1, "Développeurs" },
                    { 5, 1, "Spécialistes en assurance qualité" },
                    { 6, 2, "Technologies" },
                    { 7, 2, "Services bancaires" },
                    { 8, 3, "Informatique" },
                    { 9, 3, "Ingénierie" },
                    { 10, 4, "Développement" },
                    { 11, 4, "Support technique" },
                    { 12, 5, "Consultation" },
                    { 13, 5, "Infrastructure" },
                    { 14, 6, "Systèmes d'information" },
                    { 15, 6, "Commerce électronique" }
                });

            migrationBuilder.InsertData(
                table: "Entreprise",
                columns: new[] { "Id", "NomDomaine", "NomEntreprise" },
                values: new object[,]
                {
                    { 1, "cchic.ca", "Hourglass Unlimited" },
                    { 2, "cius.ca", "Centre des urgences" }
                });

            migrationBuilder.InsertData(
                table: "GameCategory",
                columns: new[] { "Id", "NomGameCategory" },
                values: new object[,]
                {
                    { 1, "Puzzle" },
                    { 2, "Logique" }
                });

            migrationBuilder.InsertData(
                table: "Jeu",
                columns: new[] { "Id", "GameCategoryId", "NomJeu" },
                values: new object[] { 1, 1, "Sudoku" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Entreprise",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Entreprise",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GameCategory",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GameCategory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Jeu",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
