using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Game_On.Migrations
{
    /// <inheritdoc />
    public partial class Classement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeJeu",
                table: "Jeu");

            migrationBuilder.CreateTable(
                name: "ClassementDate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    EntrepriseId = table.Column<int>(type: "int", nullable: true),
                    DepartementId = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    DateClassement = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassementDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassementDate_Departement_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departement",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassementDate_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassementDate_Utilisateur_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ClassementDate_DepartementId",
                table: "ClassementDate",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassementDate_EntrepriseId",
                table: "ClassementDate",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassementDate_UtilisateurId",
                table: "ClassementDate",
                column: "UtilisateurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassementDate");

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
    }
}
