using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Game_On.Migrations
{
    /// <inheritdoc />
    public partial class ModificationTableUtilisateur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Utilisateur",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MotDePasse",
                table: "Utilisateur",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Pseudo",
                table: "Utilisateur",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "MotDePasse", "Pseudo" },
                values: new object[] { "jean.tremblay@example.com", "Test@1234", "jtremblay" });

            migrationBuilder.UpdateData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "MotDePasse", "Pseudo" },
                values: new object[] { "marie.gagnon@example.com", "Test@1234", "mgagnon" });

            migrationBuilder.UpdateData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "MotDePasse", "Pseudo" },
                values: new object[] { "pierre.roy@example.com", "Test@1234", "proy" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "MotDePasse",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "Pseudo",
                table: "Utilisateur");

            migrationBuilder.InsertData(
                table: "Utilisateur",
                columns: new[] { "Id", "DepartementId", "EntrepriseId", "LoginTime", "LogoutTime", "NomUtilisateur", "PrenomUtilisateur", "Score" },
                values: new object[,]
                {
                    { 4, 2, 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 16, 30, 0, 0, DateTimeKind.Unspecified), "Côté", "Sophie", 28 },
                    { 5, 2, 1, new DateTime(2025, 10, 28, 7, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 16, 0, 0, 0, DateTimeKind.Unspecified), "Bouchard", "Luc", 25 },
                    { 6, 2, 1, new DateTime(2025, 10, 28, 8, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Gauthier", "Isabelle", 32 },
                    { 7, 2, 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 30, 0, 0, DateTimeKind.Unspecified), "Morin", "François", 26 },
                    { 8, 3, 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Lavoie", "Caroline", 35 },
                    { 9, 3, 1, new DateTime(2025, 10, 28, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 18, 0, 0, 0, DateTimeKind.Unspecified), "Fortin", "Marc", 31 },
                    { 10, 3, 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 16, 0, 0, 0, DateTimeKind.Unspecified), "Gagné", "Julie", 29 },
                    { 11, 3, 1, new DateTime(2025, 10, 28, 8, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Ouellet", "Daniel", 33 },
                    { 12, 4, 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Pelletier", "Alexandre", 42 },
                    { 13, 4, 1, new DateTime(2025, 10, 28, 7, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 16, 0, 0, 0, DateTimeKind.Unspecified), "Bélanger", "Nathalie", 48 },
                    { 14, 4, 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Lévesque", "Martin", 45 },
                    { 15, 4, 1, new DateTime(2025, 10, 28, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 18, 0, 0, 0, DateTimeKind.Unspecified), "Bergeron", "Stéphanie", 38 },
                    { 16, 4, 1, new DateTime(2025, 10, 28, 8, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 30, 0, 0, DateTimeKind.Unspecified), "Paquette", "Éric", 41 },
                    { 17, 4, 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Girard", "Valérie", 37 },
                    { 18, 4, 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 16, 30, 0, 0, DateTimeKind.Unspecified), "Simard", "Patrick", 46 },
                    { 19, 4, 1, new DateTime(2025, 10, 28, 7, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 16, 0, 0, 0, DateTimeKind.Unspecified), "Boucher", "Annie", 43 },
                    { 20, 4, 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Leblanc", "Sébastien", 39 },
                    { 21, 4, 1, new DateTime(2025, 10, 28, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 18, 0, 0, 0, DateTimeKind.Unspecified), "Poirier", "Mélanie", 40 },
                    { 22, 5, 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Richard", "Vincent", 30 },
                    { 23, 5, 1, new DateTime(2025, 10, 28, 8, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Cloutier", "Chantal", 28 },
                    { 24, 5, 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 16, 30, 0, 0, DateTimeKind.Unspecified), "Leclerc", "Yves", 32 },
                    { 25, 5, 1, new DateTime(2025, 10, 28, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 30, 0, 0, DateTimeKind.Unspecified), "Fournier", "Sylvie", 26 }
                });
        }
    }
}
