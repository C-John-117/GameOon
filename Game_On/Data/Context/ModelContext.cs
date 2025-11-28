using Game_On.Models;
using Microsoft.EntityFrameworkCore;

namespace Game_On.Data.Context
{
    public class ModelContext : DbContext
    {
        public DbSet<Departement> Departement { get; set; }
        public DbSet<Entreprise> Entreprise { get; set; }
        public DbSet<GameCategory> GameCategory { get; set; }
        public DbSet<Jeu> Jeu { get; set; }
        public DbSet<Partie> Partie { get; set; }
        public DbSet<Sudoku> Sudoku { get; set; }
        public DbSet<Utilisateur> Utilisateur { get; set; }
        public DbSet<Gagnant> Gagnant { get; set; }

        public ModelContext()
        {
        }
        public ModelContext(DbContextOptions<ModelContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseMySql("Server=sql.decinfo-cchic.ca;Port=33306;Database=a25_e80_2310197_dev;User=dev-2310197;Password=Zazou2468;",
                //  ServerVersion.AutoDetect("Server=sql.decinfo-cchic.ca;Port=33306;Database=a25_e80_2310197_dev;User=dev-2310197;Password=Zazou2468;"));
                optionsBuilder.UseMySql("Server=sql.decinfo-cchic.ca;Port=33306;Database=a25_e80_2331055_dev;User=dev-2331055;Password=Nnjo2004;",
                   ServerVersion.AutoDetect("Server=sql.decinfo-cchic.ca;Port=33306;Database=a25_e80_2331055_dev;User=dev-2331055;Password=Nnjo2004;"));
                //optionsBuilder.UseMySql("Server=127.0.0.1;Port=3306;Database=a25_dev_deploiement;User=root;Password=MySql;",
                //  ServerVersion.AutoDetect("Server=127.0.0.1;Port=3306;Database=a25_dev_deploiement;User=root;Password=MySql;"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entreprise>().HasData(
                new Entreprise { Id = 1, NomEntreprise = "Hourglass Unlimited", NomDomaine = "cchic.ca" },
                new Entreprise { Id = 2, NomEntreprise = "Centre des urgences", NomDomaine = "cius.ca" }
                //new Entreprise { Id = 3, NomEntreprise = "Hydro-Québec" },
                //new Entreprise { Id = 4, NomEntreprise = "Videotron" },
                //new Entreprise { Id = 5, NomEntreprise = "CGI" },
                //new Entreprise { Id = 6, NomEntreprise = "SAQ" }
            );

            modelBuilder.Entity<Departement>().HasData(

                new Departement { Id = 1, NomDepartement = "Gestionnaires de projet", EntrepriseId = 1 },
                new Departement { Id = 2, NomDepartement = "Analystes d'affaires", EntrepriseId = 1 },
                new Departement { Id = 3, NomDepartement = "Analystes d'applications", EntrepriseId = 1 },
                new Departement { Id = 4, NomDepartement = "Développeurs", EntrepriseId = 1 },
                new Departement { Id = 5, NomDepartement = "Spécialistes en assurance qualité", EntrepriseId = 1 },

                new Departement { Id = 6, NomDepartement = "Technologies", EntrepriseId = 2 },
                new Departement { Id = 7, NomDepartement = "Services bancaires", EntrepriseId = 2 },

                new Departement { Id = 8, NomDepartement = "Informatique", EntrepriseId = 3 },
                new Departement { Id = 9, NomDepartement = "Ingénierie", EntrepriseId = 3 },

                new Departement { Id = 10, NomDepartement = "Développement", EntrepriseId = 4 },
                new Departement { Id = 11, NomDepartement = "Support technique", EntrepriseId = 4 },

                new Departement { Id = 12, NomDepartement = "Consultation", EntrepriseId = 5 },
                new Departement { Id = 13, NomDepartement = "Infrastructure", EntrepriseId = 5 },

                new Departement { Id = 14, NomDepartement = "Systèmes d'information", EntrepriseId = 6 },
                new Departement { Id = 15, NomDepartement = "Commerce électronique", EntrepriseId = 6 }
            );

            modelBuilder.Entity<Jeu>().HasData(
                new Jeu { Id = 1, NomJeu = "Sudoku Master", GameCategoryId = 1, EstDisponible = true },
                new Jeu { Id = 2, NomJeu = "Poker Pro", GameCategoryId = 2, EstDisponible = false },
                new Jeu { Id = 3, NomJeu = "Quiz Mania", GameCategoryId = 2, EstDisponible = false }
            );

            modelBuilder.Entity<GameCategory>().HasData(
                new GameCategory { Id = 1, NomGameCategory = "Puzzle" },
                new GameCategory { Id = 2, NomGameCategory = "Logique" }
            );

            //    modelBuilder.Entity<Sudoku>().HasData(
            //        new Sudoku
            //        {
            //            Id = 1,
            //            Date = new DateTime(2025, 10, 28),
            //            Difficulte = "Easy",
            //            Puzzle = "302718690008060003000503018000091000460005980003607002000000870000936521905072030",
            //            Solution = "352718694148269753679543218827491365461325987593687142236154879784936521915872436"
            //        },
            //        new Sudoku
            //        {
            //            Id = 2,
            //            Date = new DateTime(2025, 10, 28),
            //            Difficulte = "Medium",
            //            Puzzle = "007300900030014070000020000078160059005800047002700160750093000280000000001400000",
            //            Solution = "527386914836914572149527836478162359615839247392745168754293681283671495961458723"
            //        },
            //        new Sudoku
            //        {
            //            Id = 3,
            //            Date = new DateTime(2025, 10, 28),
            //            Difficulte = "Hard",
            //            Puzzle = "000040000610030402280610900100007030000200800030000000000950040005300007900001000",
            //            Solution = "359742681617839452284615973198567234476293815532184769721958346845326197963471528"
            //        }
            //    );

            //    modelBuilder.Entity<Utilisateur>().HasData(
            //        new Utilisateur
            //        {
            //            Id = 1,
            //            NomUtilisateur = "Tremblay",
            //            PrenomUtilisateur = "Jean",
            //            EntrepriseId = 1,
            //            DepartementId = 1,
            //            LoginTime = new DateTime(2025, 10, 28, 8, 0, 0),
            //            LogoutTime = new DateTime(2025, 10, 28, 17, 0, 0),
            //            Score = 24,
            //            Email = "jean.tremblay@example.com",
            //            Pseudo = "jtremblay",
            //            MotDePasse = "Test@1234",
            //            TempsDeJeuCumule = TimeSpan.Zero
            //        },
            //        new Utilisateur
            //        {
            //            Id = 2,
            //            NomUtilisateur = "Gagnon",
            //            PrenomUtilisateur = "Marie",
            //            EntrepriseId = 1,
            //            DepartementId = 1,
            //            LoginTime = new DateTime(2025, 10, 28, 8, 0, 0),
            //            LogoutTime = new DateTime(2025, 10, 28, 17, 0, 0),
            //            Score = 18,
            //            Email = "marie.gagnon@example.com",
            //            Pseudo = "mgagnon",
            //            MotDePasse = "Test@1234",
            //            TempsDeJeuCumule = TimeSpan.Zero
            //        },
            //        new Utilisateur
            //        {
            //            Id = 3,
            //            NomUtilisateur = "Roy",
            //            PrenomUtilisateur = "Pierre",
            //            EntrepriseId = 1,
            //            DepartementId = 1,
            //            LoginTime = new DateTime(2025, 10, 28, 9, 0, 0),
            //            LogoutTime = new DateTime(2025, 10, 28, 18, 0, 0),
            //            Score = 15,
            //            Email = "pierre.roy@example.com",
            //            Pseudo = "proy",
            //            MotDePasse = "Test@1234",
            //            TempsDeJeuCumule = TimeSpan.Zero
            //        }

            //    //new Utilisateur { Id = 4, NomUtilisateur = "Côté", PrenomUtilisateur = "Sophie", EntrepriseId = 1, DepartementId = 2, LoginTime = new DateTime(2025, 10, 28, 8, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 16, 30, 0), Score = 28 },
            //    //new Utilisateur { Id = 5, NomUtilisateur = "Bouchard", PrenomUtilisateur = "Luc", EntrepriseId = 1, DepartementId = 2, LoginTime = new DateTime(2025, 10, 28, 7, 30, 0), LogoutTime = new DateTime(2025, 10, 28, 16, 0, 0), Score = 25 },
            //    //new Utilisateur { Id = 6, NomUtilisateur = "Gauthier", PrenomUtilisateur = "Isabelle", EntrepriseId = 1, DepartementId = 2, LoginTime = new DateTime(2025, 10, 28, 8, 30, 0), LogoutTime = new DateTime(2025, 10, 28, 17, 0, 0), Score = 32 },
            //    //new Utilisateur { Id = 7, NomUtilisateur = "Morin", PrenomUtilisateur = "François", EntrepriseId = 1, DepartementId = 2, LoginTime = new DateTime(2025, 10, 28, 8, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 17, 30, 0), Score = 26 },

            //    //new Utilisateur { Id = 8, NomUtilisateur = "Lavoie", PrenomUtilisateur = "Caroline", EntrepriseId = 1, DepartementId = 3, LoginTime = new DateTime(2025, 10, 28, 8, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 17, 0, 0), Score = 35 },
            //    //new Utilisateur { Id = 9, NomUtilisateur = "Fortin", PrenomUtilisateur = "Marc", EntrepriseId = 1, DepartementId = 3, LoginTime = new DateTime(2025, 10, 28, 9, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 18, 0, 0), Score = 31 },
            //    //new Utilisateur { Id = 10, NomUtilisateur = "Gagné", PrenomUtilisateur = "Julie", EntrepriseId = 1, DepartementId = 3, LoginTime = new DateTime(2025, 10, 28, 8, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 16, 0, 0), Score = 29 },
            //    //new Utilisateur { Id = 11, NomUtilisateur = "Ouellet", PrenomUtilisateur = "Daniel", EntrepriseId = 1, DepartementId = 3, LoginTime = new DateTime(2025, 10, 28, 8, 30, 0), LogoutTime = new DateTime(2025, 10, 28, 17, 0, 0), Score = 33 },

            //    //new Utilisateur { Id = 12, NomUtilisateur = "Pelletier", PrenomUtilisateur = "Alexandre", EntrepriseId = 1, DepartementId = 4, LoginTime = new DateTime(2025, 10, 28, 8, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 17, 0, 0), Score = 42 },
            //    //new Utilisateur { Id = 13, NomUtilisateur = "Bélanger", PrenomUtilisateur = "Nathalie", EntrepriseId = 1, DepartementId = 4, LoginTime = new DateTime(2025, 10, 28, 7, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 16, 0, 0), Score = 48 },
            //    //new Utilisateur { Id = 14, NomUtilisateur = "Lévesque", PrenomUtilisateur = "Martin", EntrepriseId = 1, DepartementId = 4, LoginTime = new DateTime(2025, 10, 28, 8, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 17, 0, 0), Score = 45 },
            //    //new Utilisateur { Id = 15, NomUtilisateur = "Bergeron", PrenomUtilisateur = "Stéphanie", EntrepriseId = 1, DepartementId = 4, LoginTime = new DateTime(2025, 10, 28, 9, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 18, 0, 0), Score = 38 },
            //    //new Utilisateur { Id = 16, NomUtilisateur = "Paquette", PrenomUtilisateur = "Éric", EntrepriseId = 1, DepartementId = 4, LoginTime = new DateTime(2025, 10, 28, 8, 30, 0), LogoutTime = new DateTime(2025, 10, 28, 17, 30, 0), Score = 41 },
            //    //new Utilisateur { Id = 17, NomUtilisateur = "Girard", PrenomUtilisateur = "Valérie", EntrepriseId = 1, DepartementId = 4, LoginTime = new DateTime(2025, 10, 28, 8, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 17, 0, 0), Score = 37 },
            //    //new Utilisateur { Id = 18, NomUtilisateur = "Simard", PrenomUtilisateur = "Patrick", EntrepriseId = 1, DepartementId = 4, LoginTime = new DateTime(2025, 10, 28, 8, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 16, 30, 0), Score = 46 },
            //    //new Utilisateur { Id = 19, NomUtilisateur = "Boucher", PrenomUtilisateur = "Annie", EntrepriseId = 1, DepartementId = 4, LoginTime = new DateTime(2025, 10, 28, 7, 30, 0), LogoutTime = new DateTime(2025, 10, 28, 16, 0, 0), Score = 43 },
            //    //new Utilisateur { Id = 20, NomUtilisateur = "Leblanc", PrenomUtilisateur = "Sébastien", EntrepriseId = 1, DepartementId = 4, LoginTime = new DateTime(2025, 10, 28, 8, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 17, 0, 0), Score = 39 },
            //    //new Utilisateur { Id = 21, NomUtilisateur = "Poirier", PrenomUtilisateur = "Mélanie", EntrepriseId = 1, DepartementId = 4, LoginTime = new DateTime(2025, 10, 28, 9, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 18, 0, 0), Score = 40 },

            //    //new Utilisateur { Id = 22, NomUtilisateur = "Richard", PrenomUtilisateur = "Vincent", EntrepriseId = 1, DepartementId = 5, LoginTime = new DateTime(2025, 10, 28, 8, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 17, 0, 0), Score = 30 },
            //    //new Utilisateur { Id = 23, NomUtilisateur = "Cloutier", PrenomUtilisateur = "Chantal", EntrepriseId = 1, DepartementId = 5, LoginTime = new DateTime(2025, 10, 28, 8, 30, 0), LogoutTime = new DateTime(2025, 10, 28, 17, 0, 0), Score = 28 },
            //    //new Utilisateur { Id = 24, NomUtilisateur = "Leclerc", PrenomUtilisateur = "Yves", EntrepriseId = 1, DepartementId = 5, LoginTime = new DateTime(2025, 10, 28, 8, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 16, 30, 0), Score = 32 },
            //    //new Utilisateur { Id = 25, NomUtilisateur = "Fournier", PrenomUtilisateur = "Sylvie", EntrepriseId = 1, DepartementId = 5, LoginTime = new DateTime(2025, 10, 28, 9, 0, 0), LogoutTime = new DateTime(2025, 10, 28, 17, 30, 0), Score = 26 }
            //    );

            //    modelBuilder.Entity<Partie>().HasData(

            //        new Partie { Id = 1, DateDebut = new DateTime(2025, 10, 28, 9, 0, 0), DateFin = new DateTime(2025, 10, 28, 9, 15, 0), SudokuId = 1 },
            //        new Partie { Id = 2, DateDebut = new DateTime(2025, 10, 28, 10, 30, 0), DateFin = new DateTime(2025, 10, 28, 10, 48, 0), SudokuId = 1 },
            //        new Partie { Id = 3, DateDebut = new DateTime(2025, 10, 28, 14, 0, 0), DateFin = new DateTime(2025, 10, 28, 14, 22, 0), SudokuId = 1 },

            //        new Partie { Id = 4, DateDebut = new DateTime(2025, 10, 28, 8, 30, 0), DateFin = new DateTime(2025, 10, 28, 9, 5, 0), SudokuId = 2 },
            //        new Partie { Id = 5, DateDebut = new DateTime(2025, 10, 28, 11, 0, 0), DateFin = new DateTime(2025, 10, 28, 11, 42, 0), SudokuId = 2 },
            //        new Partie { Id = 6, DateDebut = new DateTime(2025, 10, 28, 13, 30, 0), DateFin = new DateTime(2025, 10, 28, 14, 8, 0), SudokuId = 2 },

            //        new Partie { Id = 7, DateDebut = new DateTime(2025, 10, 28, 9, 0, 0), DateFin = new DateTime(2025, 10, 28, 9, 58, 0), SudokuId = 3 },
            //        new Partie { Id = 8, DateDebut = new DateTime(2025, 10, 28, 13, 0, 0), DateFin = new DateTime(2025, 10, 28, 14, 5, 0), SudokuId = 3 },
            //        new Partie { Id = 9, DateDebut = new DateTime(2025, 10, 28, 16, 0, 0), DateFin = new DateTime(2025, 10, 28, 17, 12, 0), SudokuId = 3 }
            //    );

            //    modelBuilder.Entity<Gagnant>().HasData(
            //        new Gagnant { Id = 1, UtilisateurId = 13, Score = 48 },
            //        new Gagnant { Id = 2, UtilisateurId = 18, Score = 46 },
            //        new Gagnant { Id = 3, UtilisateurId = 14, Score = 45 }
            //    );
        }
    }
}
