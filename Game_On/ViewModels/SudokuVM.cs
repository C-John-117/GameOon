using CommunityToolkit.Mvvm.ComponentModel;
using Game_On.Data.Context;
using Game_On.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;

public partial class SudokuVM : ObservableObject
{
    private readonly ModelContext context = new ModelContext();
    private CoupScanner _scanner;
    private Sudoku? sudoku = new Sudoku();
    Partie? partieUser = new Partie();

    [ObservableProperty]
    private string _puzzle = new string('0', 81);

    [ObservableProperty]
    private string _difficulte = "Medium";

    [ObservableProperty]
    private string _mode = "Compétition";

    public string OriginalPuzzle { get; private set; } = new string('0', 81);
    public SudokuVM()
    {
        _scanner = new CoupScanner(_puzzle);

        //  _ = InitializeAsync();

    }

    public Task<Sudoku?> FindSudokuByPuzzleAsync(string originalPuzzle)
        => context.Sudoku.FirstOrDefaultAsync(s => s.Puzzle == originalPuzzle);

    public async Task InitializeAsync(string difficultéChoisie, int userId)
    {
        try
        {
            // 1) Sudoku du jour
            sudoku = await GetDailyLevel(difficultéChoisie);

            if (sudoku == null)
                return;

            OriginalPuzzle = sudoku.Puzzle;

            // 2) Chercher une sauvegarde pour ce user + ce sudoku
            var partie = await context.Partie
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UtilisateurId == userId
                                       && p.SudokuId == sudoku.Id);

            if (partie != null && !string.IsNullOrEmpty(partie.Save))
            {
                // ✅ Reprendre la partie sauvegardée
                Puzzle = partie.Save.PadRight(81, '0'); // au cas où
            }
            else
            {
                // ✅ Pas de sauvegarde → partir du puzzle d'origine
                Puzzle = sudoku.Puzzle;
                await GestionPartie(userId);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            // tu peux aussi faire un MessageBox en cas d'erreur si tu veux
        }
    }


    partial void OnPuzzleChanged(string value)
        => _scanner = new CoupScanner(value);

    private async Task<Sudoku?> GetDailyLevel(string difficulte)
    {
        var today = DateTime.Today;

        return await context.Sudoku

          .Where(s => s.Date >= today && s.Date < today.AddDays(1) && s.Difficulte == difficulte)

            .Select(s => s)

         .FirstOrDefaultAsync();

    }

    public async Task GestionPartie(int userId, bool isCompetition = true)
    {
        var requetePartie = from partie in context.Partie
                            where partie.SudokuId == sudoku.Id && partie.UtilisateurId == userId
                            select partie;

        partieUser = await requetePartie.FirstOrDefaultAsync();

        if (partieUser != null)
        {
            // Vérifier si la partie est déjà terminée (déjà finie auparavant)
            bool dejaTerminee = partieUser.DateFin != default(DateTime);

            partieUser.DateFin = DateTime.Now;

            // Calculer le score UNIQUEMENT en mode Compétition ET si pas déjà terminée
            if (isCompetition && !dejaTerminee)
            {

                TimeSpan duree = partieUser.DateFin - partieUser.DateDebut;

                double score = (1 / ((double)duree.TotalSeconds)) * 500000;
                Math.Round(score);

                switch (sudoku.Difficulte)
                {
                    case "Easy":
                        score *= 1;
                        break;

                    case "Medium":
                        score *= 2;
                        break;

                    case "Hard":
                        score *= 4;
                        break;

                    default:
                        break;
                }

                var requeteUser = from utilisateur in context.Utilisateur
                                  where utilisateur.Id == userId
                                  select utilisateur;

                Utilisateur? user = await requeteUser.FirstOrDefaultAsync();

                if (user == null)
                {
                    MessageBox.Show("une erreur c'est prooduite lors de la sauvegarde", "Erreur Serveur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                user.Score += (int)score;

                context.Utilisateur.Update(user);
                MessageBox.Show($"Félicitations ! Vous avez gagné {(int)score} points !", "Score ajouté", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (isCompetition && dejaTerminee)
            {
                // Déjà terminé avant en mode Compétition : pas de points
                MessageBox.Show("Vous avez terminé ce sudoku, mais vous l'aviez déjà complété aujourd'hui. Aucun point ajouté.",
                "Sudoku rejoué", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            // ✅ En mode Entraînement : juste fermer la partie

            context.Partie.Update(partieUser);
            await context.SaveChangesAsync();

            return;
        }

        // ✅ Créer une nouvelle partie si elle n'existe pas
        partieUser = new Partie();

        partieUser.DateDebut = DateTime.Now;
        partieUser.SudokuId = sudoku.Id;
        partieUser.UtilisateurId = userId;
        partieUser.Save = Puzzle;

        context.Partie.Add(partieUser);
        await context.SaveChangesAsync();
    }

    public async Task InitializeTrainingAsync(string difficultéChoisie, int userId)
    {
        try
        {
            Difficulte = difficultéChoisie;

            System.Diagnostics.Debug.WriteLine($"InitializeTrainingAsync: Recherche partie en cours pour user {userId}, difficulté {difficultéChoisie}");

            var partieEnCours = await (from partie in context.Partie.AsNoTracking()
                                       join sudokuJoin in context.Sudoku on partie.SudokuId equals sudokuJoin.Id
                                       where partie.UtilisateurId == userId
                                    && partie.DateFin == default(DateTime)
                                      && sudokuJoin.IsTraining == true
                                     && sudokuJoin.Difficulte == difficultéChoisie
                                       orderby partie.DateDebut descending
                                       select partie).FirstOrDefaultAsync();

            Sudoku? sudokuPartieEnCours = null;
            if (partieEnCours != null)
            {
                sudokuPartieEnCours = await context.Sudoku
                   .AsNoTracking()
                   .FirstOrDefaultAsync(s => s.Id == partieEnCours.SudokuId);
            }

            // 2) Si une partie en cours existe, demander si on veut la reprendre
            if (partieEnCours != null && sudokuPartieEnCours != null)
            {
                System.Diagnostics.Debug.WriteLine($"InitializeTrainingAsync: Partie en cours trouvée (ID: {partieEnCours.Id}, Sudoku ID: {partieEnCours.SudokuId}, Difficulté: {sudokuPartieEnCours.Difficulte}, Save: '{partieEnCours.Save}', Save length: {partieEnCours.Save?.Length ?? 0})");

                var result = MessageBox.Show($"Vous avez une partie d'entraînement en cours en difficulté {sudokuPartieEnCours.Difficulte}. Voulez-vous la reprendre ?", "Reprendre la partie", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Reprendre la partie sauvegardée
                    sudoku = sudokuPartieEnCours;
                    partieUser = partieEnCours;
                    OriginalPuzzle = sudoku.Puzzle;

                    if (!string.IsNullOrEmpty(partieEnCours.Save))
                    {
                        Puzzle = partieEnCours.Save.PadRight(81, '0');
                        System.Diagnostics.Debug.WriteLine($"InitializeTrainingAsync: Reprise avec save: '{partieEnCours.Save}'");
                    }
                    else
                    {
                        Puzzle = sudoku.Puzzle;
                        System.Diagnostics.Debug.WriteLine($"InitializeTrainingAsync: Reprise sans save, utilise puzzle original");
                    }
                    return;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"InitializeTrainingAsync: Fermeture de l'ancienne partie");

                    var partieAFermer = await context.Partie.FindAsync(partieEnCours.Id);
                    if (partieAFermer != null)
                    {
                        partieAFermer.DateFin = DateTime.Now;
                        context.Partie.Update(partieAFermer);
                        await context.SaveChangesAsync();
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"InitializeTrainingAsync: Aucune partie en cours trouvée pour la difficulté {difficultéChoisie}");
            }

            // 3) Créer un nouveau sudoku d'entraînement via l'API
            System.Diagnostics.Debug.WriteLine($"InitializeTrainingAsync: Création d'un nouveau sudoku via API");
            sudoku = await CreateNewTrainingSudoku(difficultéChoisie);

            OriginalPuzzle = sudoku.Puzzle;
            Puzzle = sudoku.Puzzle;

            // 4) Créer une nouvelle partie
            var nouvellePartie = new Partie
            {
                UtilisateurId = userId,
                SudokuId = sudoku.Id,
                DateDebut = DateTime.Now,
                DateFin = default(DateTime),
                Save = sudoku.Puzzle
            };

            await context.Partie.AddAsync(nouvellePartie);
            await context.SaveChangesAsync();

            partieUser = nouvellePartie;

            System.Diagnostics.Debug.WriteLine($"InitializeTrainingAsync: Nouvelle partie créée (ID: {nouvellePartie.Id}, DateDebut: {nouvellePartie.DateDebut})");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"InitializeTrainingAsync ERROR: {ex.Message}\n{ex.StackTrace}");
            MessageBox.Show($"Erreur lors de l'initialisation de l'entraînement : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task<Sudoku> CreateNewTrainingSudoku(string difficulte)
    {
        var apiRecup = new apiRecuperateur();
        var newSudoku = await apiRecup.GetApiData(difficulte);

        if (newSudoku == null)
        {
            throw new Exception("Impossible de générer un nouveau sudoku d'entraînement");
        }

        newSudoku.IsTraining = true;
        newSudoku.Date = DateTime.Now;

        await context.Sudoku.AddAsync(newSudoku);
        await context.SaveChangesAsync();

        return newSudoku;
    }


}