using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Game_On.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Departement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomDepartement = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntrepriseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departement", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Entreprise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomEntreprise = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entreprise", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Gagnant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gagnant", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GameCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomGameCategory = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCategory", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Jeu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomJeu = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GameCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jeu", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Partie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateDebut = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SudokuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partie", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sudoku",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Puzzle = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Solution = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Difficulte = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sudoku", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomUtilisateur = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrenomUtilisateur = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntrepriseId = table.Column<int>(type: "int", nullable: false),
                    DepartementId = table.Column<int>(type: "int", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    LogoutTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilisateur_Departement_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Utilisateur_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                columns: new[] { "Id", "NomEntreprise" },
                values: new object[,]
                {
                    { 1, "Hourglass Unlimited" },
                    { 2, "Desjardins" },
                    { 3, "Hydro-Québec" },
                    { 4, "Videotron" },
                    { 5, "CGI" },
                    { 6, "SAQ" }
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
                columns: new[] { "Id", "DepartementId", "EntrepriseId", "LoginTime", "LogoutTime", "NomUtilisateur", "PrenomUtilisateur", "Score" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Tremblay", "Jean", 24 },
                    { 2, 1, 1, new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), "Gagnon", "Marie", 18 },
                    { 3, 1, 1, new DateTime(2025, 10, 28, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 18, 0, 0, 0, DateTimeKind.Unspecified), "Roy", "Pierre", 15 },
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

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateur_DepartementId",
                table: "Utilisateur",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateur_EntrepriseId",
                table: "Utilisateur",
                column: "EntrepriseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gagnant");

            migrationBuilder.DropTable(
                name: "GameCategory");

            migrationBuilder.DropTable(
                name: "Jeu");

            migrationBuilder.DropTable(
                name: "Partie");

            migrationBuilder.DropTable(
                name: "Sudoku");

            migrationBuilder.DropTable(
                name: "Utilisateur");

            migrationBuilder.DropTable(
                name: "Departement");

            migrationBuilder.DropTable(
                name: "Entreprise");
        }
    }
}
