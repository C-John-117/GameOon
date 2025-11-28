using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Game_On.Data.Context;
using Game_On.Models;
using Microsoft.EntityFrameworkCore;

namespace Game_On.ViewModels
{
    public partial class UtilisateurVM : ObservableObject
    {
        private readonly ModelContext context;

        public UtilisateurVM()
        {
            this.context = new ModelContext();
        }

        [ObservableProperty]
        string _nomUtilisateur = "";

        [ObservableProperty]
        string _prenomUtilisateur = "";

        [ObservableProperty]
        string _email = "";

        [ObservableProperty]
        string _pseudo = "";

        [ObservableProperty]
        string _motDePasse = "";

        [ObservableProperty]
        string _erreurNom = "";

        [ObservableProperty]
        string _erreurPrenom = "";

        [ObservableProperty]
        string _erreurEmail = "";

        [ObservableProperty]
        string _erreurPseudo = "";

        [ObservableProperty]
        string _erreurMotDePasse = "";

        public bool ValiderFormatEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string[] parts = email.Split('@');
            if (parts.Length != 2)
                return false;

            string nomEmail = parts[0];
            string domainEmail = parts[1];

            if (string.IsNullOrWhiteSpace(nomEmail) || string.IsNullOrWhiteSpace(domainEmail))
                return false;

            if (!domainEmail.Contains('.'))
                return false;

            if (domainEmail.StartsWith('.') || domainEmail.EndsWith('.'))
                return false;

            if (email.Contains(' '))
                return false;

            return true;
        }

        [RelayCommand]
        public async Task<bool> ValiderInscription()
        {
            ErreurNom = string.Empty;
            ErreurPrenom = string.Empty;
            ErreurEmail = string.Empty;
            ErreurPseudo = string.Empty;
            ErreurMotDePasse = string.Empty;

            bool estValide = true;

            if (string.IsNullOrWhiteSpace(NomUtilisateur))
            {
                ErreurNom = "Le nom est requis.";
                estValide = false;
            }

            if (string.IsNullOrWhiteSpace(PrenomUtilisateur))
            {
                ErreurPrenom = "Le prénom est requis.";
                estValide = false;
            }

            if (string.IsNullOrWhiteSpace(MotDePasse))
            {
                ErreurMotDePasse = "Le mot de passe est requis.";
                estValide = false;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                ErreurEmail = "L'email est requis.";
                estValide = false;
            }
            else if (!ValiderFormatEmail(Email))
            {
                ErreurEmail = "Le format de l'email est invalide.";
                estValide = false;
            }
            else
            {
                string domaineEmail = Email.Split('@')[1].ToLower();
                Entreprise? entreprise = await (
                    from e in context.Entreprise.AsNoTracking()
                    where e.NomDomaine.ToLower() == domaineEmail
                    select e
                ).FirstOrDefaultAsync();

                if (entreprise == null)
                {
                    ErreurEmail = $"Aucune entreprise n'est enregistrée pour le domaine '{domaineEmail}'.";
                    estValide = false;
                }
            }

            if (string.IsNullOrWhiteSpace(Pseudo))
            {
                ErreurPseudo = "Le pseudo est requis.";
                estValide = false;
            }
            else
            {
                Utilisateur? utilisateurExistant = await (from utilisateur in context.Utilisateur.AsNoTracking()
                                                          where utilisateur.Pseudo.ToLower() == Pseudo.ToLower()
                                                          select utilisateur).FirstOrDefaultAsync();
                if (utilisateurExistant != null)
                {
                    ErreurPseudo = $"Le pseudo '{Pseudo}' est déjà pris.";
                    estValide = false;
                }
            }

            return estValide;
        }

        public void ReinitialiserChamps()
        {
            NomUtilisateur = string.Empty;
            PrenomUtilisateur = string.Empty;
            Email = string.Empty;
            Pseudo = string.Empty;
            MotDePasse = string.Empty;
        }
    }
}
