using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Game_On.Data.Context;
using Game_On.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Game_On.ViewModels
{
    public partial class EntrepriseVM : ObservableObject
    {
        private readonly ModelContext context; // appel ENTity Framework pour ecriture et lecture dans la BD
        public EntrepriseVM()
        {
            // Initialisation du contexte ce qui va permettre de rendre la liste observable et la lier directement à la vue
            this.context = new ModelContext();
            entreprises = new ObservableCollection<Entreprise>(context.Entreprise.ToList());
        }

        //liste affichée dans la vue
        public ObservableCollection<Entreprise> entreprises { get; set; }

        [ObservableProperty]
        string nomEntreprise = "";

        [ObservableProperty]
        string nomDomaine = "";

        [RelayCommand]
        public async Task AjouterEntreprise()
        {
            //recuperation des informations saisies dans la vue et creation d'une nouvelle entreprise
            Entreprise entreprise = new Entreprise();
            entreprise.NomEntreprise = NomEntreprise;
            entreprise.NomDomaine = NomDomaine;

            try
            {
                //on envois les informations à la base de données 
                await context.Entreprise.AddAsync(entreprise);
                await context.SaveChangesAsync();
                entreprises.Add(entreprise);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur EF : {ex.Message}");
                if (ex.InnerException != null)
                    Debug.WriteLine($"Détail SQL : {ex.InnerException.Message}");
            }

            NomEntreprise = string.Empty;
            NomDomaine = string.Empty;

        }

        [RelayCommand]
        public async Task RecupererEntreprises()
        {
            try
            {
                // si dans un futur eventuel on veut pouvoir modifier la liste des eentreprises directement alors faire : 
                // var entrepriseEnBd = await context.Entreprise.ToListAsync(); 

                var entreprisesEnDb = await context.Entreprise.AsNoTracking().ToListAsync(); //  AsNoTracking() empeche denregistrer les modifications qui peuvenent etre faites
                entreprises.Clear();

                foreach (Entreprise entrepriz in entreprisesEnDb)
                {
                    entreprises.Add(entrepriz);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur EF : {ex.Message}");
                if (ex.InnerException != null)
                    Debug.WriteLine($"Détail SQL : {ex.InnerException.Message}");

            }
        }
    }
}
