using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Game_On.Data.Context;
using Game_On.Models;
using Game_On.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;


namespace Game_On.ViewModels
{
    public partial class ClassementDateVM : ObservableObject
    {
        [ObservableProperty]
        private DateTime dateSemaine = DateTime.UtcNow.Date;

        public DateTime DebutSemaine
        {
            get
            {
                return DateSemaine.Date;
            }
        }

        public DateTime FinSemaine
        {
            get
            {
                return DebutSemaine + TimeSpan.FromDays(6);
            }
        }

        [ObservableProperty]
        private ObservableCollection<ClassementDate> classementsEntrepriseSemaine = new ObservableCollection<ClassementDate>();

        [ObservableProperty]
        private ObservableCollection<ClassementDate> classementsDepartementSemaine = new ObservableCollection<ClassementDate>();

        [RelayCommand]
        public async Task<int> EnregistrerClassementParEntrepriseAsync()
        {
            return await EnregistrerClassementAsync(true);
        }

        [RelayCommand]
        public async Task<int> EnregistrerClassementParDepartementAsync()
        {
            return await EnregistrerClassementAsync(false);
        }

        [RelayCommand]
        public async Task ChargerClassementEntrepriseAsync()
        {
            await ChargerClassementsAsync(true);
        }

        [RelayCommand]
        public async Task ChargerClassementDepartementAsync()
        {
            await ChargerClassementsAsync(false);
        }

        [RelayCommand]
        public async Task SemainePrecedenteAsync()
        {
            dateSemaine = dateSemaine - TimeSpan.FromDays(7);
            OnPropertyChanged(nameof(DateSemaine));
            OnPropertyChanged(nameof(DebutSemaine));
            OnPropertyChanged(nameof(FinSemaine));
            await ActualiserClassementsAsync();
        }
        

        [RelayCommand]
        public async Task SemaineSuivanteAsync()
        {
            dateSemaine = dateSemaine + TimeSpan.FromDays(7);
            OnPropertyChanged(nameof(DateSemaine));
            OnPropertyChanged(nameof(DebutSemaine));
            OnPropertyChanged(nameof(FinSemaine));
            await ActualiserClassementsAsync();
        }

        private async Task<int> EnregistrerClassementAsync(bool parEntreprise)
        {
            ModelContext context = new ModelContext();

            System.Collections.Generic.List<Utilisateur> utilisateurs = await context.Utilisateur.AsNoTracking().ToListAsync();

            foreach (Utilisateur utilisateur in utilisateurs)
            {
                if (utilisateur.Score > 0)
                {
                    ClassementDate classement = new ClassementDate
                    {
                        UtilisateurId = utilisateur.Id,
                        EntrepriseId = parEntreprise ? utilisateur.EntrepriseId : null,
                        DepartementId = parEntreprise ? null : utilisateur.DepartementId,
                        Score = utilisateur.Score,
                        DateClassement = DateSemaine
                    };

                    context.ClassementDate.Add(classement);
                }
            }

            return await context.SaveChangesAsync();
        }

        private async Task ChargerClassementsAsync(bool parEntreprise)
        {
            ModelContext context = new ModelContext();

            DateTime debut = DebutSemaine;
            DateTime fin = FinSemaine;

            IQueryable<ClassementDate> requete = from classement in context.ClassementDate.AsNoTracking()
                                                 where classement.DateClassement >= debut && classement.DateClassement <= fin
                                                 select classement;

            if (parEntreprise)
            {
                requete = from classement in requete
                          where classement.EntrepriseId != null
                          select classement;
            }
            else
            {
                requete = from classement in requete
                          where classement.DepartementId != null
                          select classement;
            }

            System.Collections.Generic.List<ClassementDate> classementsTrouves = await requete.ToListAsync();
            ObservableCollection<ClassementDate> cible = parEntreprise ? ClassementsEntrepriseSemaine : ClassementsDepartementSemaine;

            cible.Clear();

            foreach (ClassementDate classement in classementsTrouves)
            {
                cible.Add(classement);
            }
        }

        private async Task ActualiserClassementsAsync()
        {
            await ChargerClassementsAsync(true);
            await ChargerClassementsAsync(false);
        }
    }
}
