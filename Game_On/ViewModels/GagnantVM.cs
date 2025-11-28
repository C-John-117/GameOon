using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Game_On.Data.Context;
using Game_On.Models;
using Microsoft.EntityFrameworkCore;

namespace Game_On.ViewModels
{
    public partial class GagnantVM : ObservableObject
    {
        // plus de DbContext en champ → on crée un ModelContext par méthode

        private Utilisateur? utilisateurConnecte;

        [ObservableProperty]
        private List<Departement> departements;

        [ObservableProperty]
        private Departement departementSelecttionner;

        [ObservableProperty]
        private List<Entreprise> entreprises;

        [ObservableProperty]
        private Entreprise entrepriseSelecttioner;

        [ObservableProperty]
        private List<Utilisateur> utilisateurEntreprises;

        [ObservableProperty]
        private List<Utilisateur> utilisateurDepartement;

        [ObservableProperty]
        private LiveChartEntreprise chartVM;

        // KPIs pour le dashboard
        [ObservableProperty]
        private string topEntrepriseNom;

        [ObservableProperty]
        private int topEntrepriseScore;

        [ObservableProperty]
        private double scoreMoyenGlobal;

        [ObservableProperty]
        private int nombreTotalUtilisateurs;

        public GagnantVM()
        {
            _ = InitialiserAsync();
        }

        public async Task DefinirUtilisateurConnecte(Utilisateur utilisateur)
        {
            utilisateurConnecte = utilisateur;
            // Recharger les départements filtrés par entreprise
            Departements = await ListeDepartementsAsync();
        }

        private async Task InitialiserAsync()
        {
            Entreprises = await ListeEntreprisesAsync();
            // Ne charge pas les départements ici, ils seront chargés après la connexion

            await ChargerScoreAsync();
            await MettreAJourKpiAsync();

            ChartVM = new LiveChartEntreprise(Entreprises);
        }

        public async Task<List<Utilisateur>> ClassementParDepartementAsync(int departementId)
        {
            using var context = new ModelContext();

            var requeteAuDepartement =
                from utilisateur in context.Utilisateur
                        .Include("Departement")
                        .AsNoTracking()
                where utilisateur.DepartementId == departementId
                orderby utilisateur.Score descending
                select utilisateur;

            return await requeteAuDepartement.ToListAsync();
        }

        private async Task<List<Utilisateur>> ClassementParEntrepriseAsync(int entrepriseId)
        {
            using var context = new ModelContext();

            var requeteALEntreprise =
                from utilisateur in context.Utilisateur
                        .Include("Entreprise")
                        .Include("Departement")
                        .AsNoTracking()
                where utilisateur.EntrepriseId == entrepriseId
                orderby utilisateur.Score descending
                select utilisateur;

            return await requeteALEntreprise.ToListAsync();
        }

        public async Task<List<Utilisateur>> ClassementGlobalAsync()
        {
            using var context = new ModelContext();

            var requetePourAvoirTousLesUtilisateurs =
                from utilisateur in context.Utilisateur
                        .Include("Entreprise")
                        .Include("Departement")
                        .AsNoTracking()
                orderby utilisateur.Score descending
                select utilisateur;

            return await requetePourAvoirTousLesUtilisateurs.ToListAsync();
        }

        public async Task<List<Departement>> ListeDepartementsAsync()
        {
            using var context = new ModelContext();

            var requeteAuDepartements =
                from departement in context.Departement.AsNoTracking()
                where utilisateurConnecte == null || departement.EntrepriseId == utilisateurConnecte.EntrepriseId
                orderby departement.NomDepartement
                select departement;

            return await requeteAuDepartements.ToListAsync();
        }

        public async Task<List<Entreprise>> ListeEntreprisesAsync()
        {
            using var context = new ModelContext();

            var requeteEntreprises =
                from entreprise in context.Entreprise.AsNoTracking()
                orderby entreprise.NomEntreprise
                select entreprise;

            return await requeteEntreprises.ToListAsync();
        }

        [RelayCommand]
        public async Task ChargerEntreprise(int idEntreprise)
        {
            UtilisateurEntreprises = await ClassementParEntrepriseAsync(idEntreprise);
        }

        [RelayCommand]
        public async Task ChargerDepartement(int idEntreprise)
        {
            UtilisateurDepartement = await ClassementParDepartementAsync(idEntreprise);
        }

        private async Task ChargerScoreAsync()
        {
            if (Entreprises == null || Entreprises.Count == 0)
                return;

            foreach (Entreprise entreprise in Entreprises)
            {
                var utilisateurs = await ClassementParEntrepriseAsync(entreprise.Id);

                if (utilisateurs.Count == 0)
                {
                    entreprise.Score = 0;
                    continue;
                }

                entreprise.Score = (int)utilisateurs.Average(u => u.Score);
            }
        }

        private async Task MettreAJourKpiAsync()
        {
            var classementGlobal = await ClassementGlobalAsync();

            if (classementGlobal.Count > 0)
            {
                ScoreMoyenGlobal = classementGlobal.Average(u => u.Score);
                NombreTotalUtilisateurs = classementGlobal.Count;
            }
            else
            {
                ScoreMoyenGlobal = 0;
                NombreTotalUtilisateurs = 0;
            }

            var meilleure = Entreprises?
                .OrderByDescending(e => e.Score)
                .FirstOrDefault();

            if (meilleure != null)
            {
                TopEntrepriseNom = meilleure.NomEntreprise;
                TopEntrepriseScore = meilleure.Score;
            }
            else
            {
                TopEntrepriseNom = "Aucune";
                TopEntrepriseScore = 0;
            }
        }

        // Recharge le graphe + KPIs
        [RelayCommand]
        public async Task RecalculerChartAsync()
        {
            await ChargerScoreAsync();
            await MettreAJourKpiAsync();
            ChartVM = new LiveChartEntreprise(Entreprises);
        }
    }
}
