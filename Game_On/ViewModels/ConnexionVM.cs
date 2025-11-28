using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Game_On.Data.Context;
using Game_On.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game_On.ViewModels
{
    public partial class ConnexionVM : ObservableObject
    {
        private readonly ModelContext context;
        public ConnexionVM()
        {
            this.context = new ModelContext();
        }

        [ObservableProperty]
        private string pseudo = "";

        [ObservableProperty]
        private string motDePasse = "";

        [ObservableProperty]
        private Utilisateur? utilisateurAuthentifie = null;

        [ObservableProperty]
        private Sudoku tokenSudoku = null;

        [ObservableProperty]
        private string messageConnexion = "";

        [ObservableProperty]
        private bool dejaConnecte;

        [RelayCommand]
        public async Task Connexion()
        {
            try
            {
                messageConnexion = "";

                //je recupere les infos des utilisateur et tenant compte des relations
                var utilisateurTrouvé = context.Utilisateur.Include("Entreprise").Include("Departement").Where(u => u.Pseudo == Pseudo).FirstOrDefault();

                //var utilisateurRecherché = from utilisateurCourant in utilisateursAvecRelations
                // where utilisateurCourant.Pseudo == Pseudo
                // select utilisateurCourant;

                if (utilisateurTrouvé == null)
                {
                    MessageConnexion = "Désolé, nous n'avons trouvé aucun utilisateur.";
                    //  UtilisateurAuthentifie = null; // 
                    MessageBox.Show(MessageConnexion, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    DejaConnecte = false;
                    return;
                }

                if (utilisateurTrouvé.MotDePasse != MotDePasse)
                {
                    MessageConnexion = "Mot de passe incorrect.";
                    MessageBox.Show(MessageConnexion, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    DejaConnecte = false;
                    return;
                }

                utilisateurTrouvé.LoginTime = DateTime.Now;
                context.Utilisateur.Update(utilisateurTrouvé);
                await context.SaveChangesAsync();
                UtilisateurAuthentifie = utilisateurTrouvé;
                MessageConnexion = "Connexion réussie.";
                DejaConnecte = true;

                await VerifierEtGenererSudokusQuotidiens();
            }
            catch (Exception ex)
            {
                MessageConnexion = $"Erreur lors de la connexion : {ex.Message}";
                DejaConnecte = false;
                utilisateurAuthentifie = null;
            }

        }

        private async Task VerifierEtGenererSudokusQuotidiens()
        {
            DateTime aujourdhui = DateTime.Today;
            apiRecuperateur api = new apiRecuperateur();

            string[] difficultes = { "Easy", "Medium", "Hard" };

            foreach (string difficulte in difficultes)
            {
                var sudokuExistant = await context.Sudoku
                    .FirstOrDefaultAsync(s => s.Date.Date == aujourdhui && s.Difficulte == difficulte && !s.IsTraining);

                if (sudokuExistant == null)
                {
                    var nouveauSudoku = await api.GetApiData(difficulte);

                    if (nouveauSudoku != null)
                    {
                        TokenSudoku = nouveauSudoku;
                        context.Sudoku.Add(nouveauSudoku);
                    }
                }
            }
            await context.SaveChangesAsync();
        }

        [RelayCommand]
        private async Task Deconnexion()
        {
            if (UtilisateurAuthentifie is null)
            {
                return;
            }

            try
            {
                UtilisateurAuthentifie.LogoutTime = DateTime.Now;
                context.Utilisateur.Update(UtilisateurAuthentifie);
                await context.SaveChangesAsync();
                MessageConnexion = "DECONNEXION REUSSIE";
            }
            catch (Exception ex)
            {

                MessageConnexion = $"OUPS ERREUR DE DECONNEXION : {ex.Message}";
                return;
            }

            UtilisateurAuthentifie = null;
            Pseudo = "";
            MotDePasse = "";
            MessageConnexion = "";

            DejaConnecte = false;
        }


    }
}