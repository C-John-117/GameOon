using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Game_On.Migrations
{
    /// <inheritdoc />
    public partial class ModificationModele : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Gagnant",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gagnant",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Gagnant",
                keyColumn: "Id",
                keyValue: 3);

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

            migrationBuilder.DeleteData(
                table: "Partie",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Partie",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Partie",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Partie",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Partie",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Partie",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Partie",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Partie",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Partie",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Sudoku",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sudoku",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sudoku",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departement",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Entreprise",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<bool>(
                name: "IsTraining",
                table: "Sudoku",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Save",
                table: "Partie",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "UtilisateurId",
                table: "Partie",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTraining",
                table: "Sudoku");

            migrationBuilder.DropColumn(
                name: "Save",
                table: "Partie");

            migrationBuilder.DropColumn(
                name: "UtilisateurId",
                table: "Partie");

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
                table: "Gagnant",
                columns: new[] { "Id", "Score", "UtilisateurId" },
                values: new object[,]
                {
                    { 1, 48, 13 },
                    { 2, 46, 18 },
                    { 3, 45, 14 }
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

            migrationBuilder.InsertData(
                table: "Partie",
                columns: new[] { "Id", "DateDebut", "DateFin", "SudokuId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 28, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 9, 15, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2025, 10, 28, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 10, 48, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2025, 10, 28, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 14, 22, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, new DateTime(2025, 10, 28, 8, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 9, 5, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, new DateTime(2025, 10, 28, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 11, 42, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, new DateTime(2025, 10, 28, 13, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 14, 8, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 7, new DateTime(2025, 10, 28, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 9, 58, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 8, new DateTime(2025, 10, 28, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 14, 5, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 9, new DateTime(2025, 10, 28, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 12, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "Sudoku",
                columns: new[] { "Id", "Date", "Difficulte", "Puzzle", "Solution" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Easy", "302718690008060003000503018000091000460005980003607002000000870000936521905072030", "352718694148269753679543218827491365461325987593687142236154879784936521915872436" },
                    { 2, new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "007300900030014070000020000078160059005800047002700160750093000280000000001400000", "527386914836914572149527836478162359615839247392745168754293681283671495961458723" },
                    { 3, new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hard", "000040000610030402280610900100007030000200800030000000000950040005300007900001000", "359742681617839452284615973198567234476293815532184769721958346845326197963471528" }
                });

            migrationBuilder.InsertData(
                table: "Utilisateur",
                columns: new[] { "Id", "DepartementId", "Email", "EntrepriseId", "LoginTime", "LogoutTime", "MotDePasse", "NomUtilisateur", "PrenomUtilisateur", "Pseudo", "Score", "TempsDeJeuCumule" },
                values: new object[,]
                {
                    { 1, 1, "jean.tremblay@example.com", 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Test@1234", "Tremblay", "Jean", "jtremblay", 24, new TimeSpan(0, 0, 0, 0, 0) },
                    { 2, 1, "marie.gagnon@example.com", 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Test@1234", "Gagnon", "Marie", "mgagnon", 18, new TimeSpan(0, 0, 0, 0, 0) },
                    { 3, 1, "pierre.roy@example.com", 1, new DateTime(2025, 10, 28, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 18, 0, 0, 0, DateTimeKind.Unspecified), "Test@1234", "Roy", "Pierre", "proy", 15, new TimeSpan(0, 0, 0, 0, 0) }
                });
        }
    }
}
