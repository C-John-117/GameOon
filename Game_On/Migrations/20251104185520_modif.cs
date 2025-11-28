using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Game_On.Migrations
{
    /// <inheritdoc />
    public partial class modif : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Entreprise",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Entreprise",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Entreprise",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Entreprise",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AddColumn<string>(
                name: "NomDomaine",
                table: "Entreprise",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Entreprise",
                keyColumn: "Id",
                keyValue: 1,
                column: "NomDomaine",
                value: "cchic.ca");

            migrationBuilder.UpdateData(
                table: "Entreprise",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "NomDomaine", "NomEntreprise" },
                values: new object[] { "cius.ca", "Centre des urgences" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomDomaine",
                table: "Entreprise");

            migrationBuilder.UpdateData(
                table: "Entreprise",
                keyColumn: "Id",
                keyValue: 2,
                column: "NomEntreprise",
                value: "Desjardins");

            migrationBuilder.InsertData(
                table: "Entreprise",
                columns: new[] { "Id", "NomEntreprise" },
                values: new object[,]
                {
                    { 3, "Hydro-Québec" },
                    { 4, "Videotron" },
                    { 5, "CGI" },
                    { 6, "SAQ" }
                });
        }
    }
}
