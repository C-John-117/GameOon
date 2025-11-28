using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Game_On.Data.Context;
using Game_On.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game_On.ViewModels
{
    public partial class PopupChoixDpartemntVM : ObservableObject
    {
        private readonly ModelContext context;
        private Entreprise? entrepriseRegister;
        private Utilisateur user;
        public ObservableCollection<Utilisateur> utilisateurs { get; set; }

        List<Departement> listeDesDepartementsExistants;

        [ObservableProperty]
        private ObservableCollection<Departement> departements;

        [ObservableProperty]
        private Departement departementSelectionne;

        [ObservableProperty]
        private string messageErreur = "";

        public PopupChoixDpartemntVM(Utilisateur user)
        {
            this.user = user;
            context = new ModelContext();
            listeDesDepartementsExistants = new List<Departement>();
            departements = new ObservableCollection<Departement>();
            RecupererListeDepartement(user.Email);

            this.context = new ModelContext();
            utilisateurs = new ObservableCollection<Utilisateur>(context.Utilisateur.ToList());
        }

        [RelayCommand]
        private async Task RecupererListeDepartement(string emailUtilisateur)
        {
            // reverifier que Jordy menvois bien un bon email et pas vide
            string domaineEmail = emailUtilisateur.Split('@')[1];
            domaineEmail = domaineEmail.ToLower();

            var requeteEntreprise = from entreprise in context.Entreprise.AsNoTracking()
                                    where entreprise.NomDomaine.ToLower() == domaineEmail
                                    select entreprise;

            entrepriseRegister = await requeteEntreprise.FirstOrDefaultAsync();
            if (entrepriseRegister == null)
            {
                MessageErreur = $"Aucune entreprise trouvée pour le domaine {domaineEmail}.";

                DepartementSelectionne = null;
                return;
            }

            // si lentreprise est trouvée alors on cherche tous les departements 
            var requeteDepartements = from departement in context.Departement.AsNoTracking()
                                      where departement.EntrepriseId == entrepriseRegister.Id
                                      orderby departement.NomDepartement
                                      select departement;

            List<Departement> departementsTrouvés = await requeteDepartements.ToListAsync();
            listeDesDepartementsExistants.Clear();
            listeDesDepartementsExistants.AddRange(departementsTrouvés);
            Departements.Clear(); // on vide la liste observable avant de la remplir
            foreach (Departement departement in listeDesDepartementsExistants)
            {
                Departements.Add(departement);
            }
            DepartementSelectionne = Departements.FirstOrDefault(); // par defaut le premier est pris


        }

        [RelayCommand]
        public async Task Inscription(object parametre)
        {
            user.EntrepriseId = entrepriseRegister.Id;
            user.DepartementId = DepartementSelectionne.Id;
            user.LoginTime = DateTime.Now;

            try
            {
                await context.Utilisateur.AddAsync(user);
                await context.SaveChangesAsync();
                utilisateurs.Add(user);

                MessageBox.Show($"Merci de vous etre inscrit {user.NomUtilisateur} {user.PrenomUtilisateur}", "Inscription reussi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur EF : {ex.Message}");
                if (ex.InnerException != null)
                    Debug.WriteLine($"Détail SQL : {ex.InnerException.Message}");
            }

            //fermer la page suite à l'éxecution
            if (parametre is Window window) window.Close();
        }
    }
}
