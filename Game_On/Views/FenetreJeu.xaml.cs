using Game_On.Data.Context;
using Game_On.Models;
using Game_On.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Game_On.Views
{
    public partial class FenetreJeu : UserControl
    {
        private string memory;
        private bool noteMode = false;
        private readonly TextBox[,] textBoxes = new TextBox[9, 9];

        // Timer pour le compteur
        private DispatcherTimer gameTimer;
        private TimeSpan elapsedTime;

        public event EventHandler? SudokuSolved;


        // Petit garde-fou pour ignorer les PropertyChanged que NOUS provoquons
        private bool _updatingFromUI = false;

        private SudokuVM? VM => DataContext as SudokuVM;
        private static string EmptyPuzzle => new string('0', 81);
        private string CurrentPuzzle => VM?.Puzzle?.PadRight(81, '0') ?? EmptyPuzzle;

        public string Puzzle
        {
            get => CurrentPuzzle;
            set
            {
                if (VM != null)
                {
                    VM.Puzzle = value;
                }
            }
        }

        public FenetreJeu()
        {
            InitializeComponent();

            // Si pas de VM, on en crée un
            if (DataContext is not SudokuVM)
                DataContext = new SudokuVM();

            // Abonnements
            DataContextChanged += FenetreJeu_DataContextChanged;
            AttachToVm(VM);

            CreateSudokuGrid();

            // Démarrer le compteur
            InitializeTimer();

            // Réinitialiser le timer quand la page devient visible
            this.IsVisibleChanged += FenetreJeu_IsVisibleChanged;
        }
        public async Task SavePartieAsync(int userId)
        {
            if (VM is null)
                return;

            using var db = new ModelContext();

            var sudoku = await db.Set<Sudoku>()
                .FirstOrDefaultAsync(s => s.Puzzle == VM.OriginalPuzzle);

            if (sudoku == null)
                return; // pas de sudoku → rien à sauvegarder

            var partie = await db.Set<Partie>()
               .FirstOrDefaultAsync(p => p.UtilisateurId == userId
                 && p.SudokuId == sudoku.Id
                 && p.DateFin == default(DateTime));

            if (partie == null)
            {
                partie = new Partie
                {
                    UtilisateurId = userId,
                    SudokuId = sudoku.Id,
                    DateDebut = DateTime.Now,
                    Save = VM.Puzzle ?? new string('0', 81)
                };

                await db.Set<Partie>().AddAsync(partie);
            }
            else
            {
                partie.Save = VM.Puzzle ?? new string('0', 81);
                db.Set<Partie>().Update(partie);
                System.Diagnostics.Debug.WriteLine($"SavePartieAsync: Partie ID {partie.Id} mise à jour avec save: {partie.Save}");
            }

            var rows = await db.SaveChangesAsync();
            Console.WriteLine("test");
        }

        private void FenetreJeu_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                // Réinitialiser et démarrer le timer
                elapsedTime = TimeSpan.Zero;
                TimerTextBlock.Text = "00:00:00";
                gameTimer?.Start();
            }
            else
            {
                    // Arrêter le timer quand la page n'est pas visible
                gameTimer?.Stop();
            }
        }

        public async void ChooseDifficulty(string difficulte)
        {
            FenetrePrincipale? fenetre = Window.GetWindow(this) as FenetrePrincipale;
            if (fenetre is null)
                return;

            await VM.InitializeAsync(difficulte, fenetre.tokenUser.Id);

            if (VM != null)
                VM.Mode = "Compétition";
        }

        public async void ChooseDifficultyTraining(string difficulte)
        {
            FenetrePrincipale? fenetre = Window.GetWindow(this) as FenetrePrincipale;
            if (fenetre is null) return;

            await VM.InitializeTrainingAsync(difficulte, fenetre.tokenUser.Id);

            if (VM != null)
                VM.Mode = "Entraînement";
        }

        private void InitializeTimer()
        {
            elapsedTime = TimeSpan.Zero;
            gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            gameTimer.Tick += GameTimer_Tick;
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));
            TimerTextBlock.Text = elapsedTime.ToString(@"hh\:mm\:ss");
        }

        private void FenetreJeu_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DetachFromVm(e.OldValue as SudokuVM);
            AttachToVm(e.NewValue as SudokuVM);
            CreateSudokuGrid();
        }

        private void AttachToVm(SudokuVM? vm)
        {
            if (vm is null) return;
            vm.PropertyChanged += VM_PropertyChanged;
        }

        private void DetachFromVm(SudokuVM? vm)
        {
            if (vm is null) return;
            vm.PropertyChanged -= VM_PropertyChanged;
        }

        // Si le VM remplace entièrement le puzzle (reset / chargement),
        // on reconstruit la grille. On ignore si c’est nous qui venons d’éditer.
        private void VM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SudokuVM.Puzzle) && !_updatingFromUI)
            {
                CreateSudokuGrid();
            }
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (VM is null) return;
            VM.Puzzle = VM.OriginalPuzzle;   // notifie proprement, recrée la grille via VM_PropertyChanged
            ResetTimer();
        }


        private void CreateSudokuGrid()
        {
            SudokuGrid.Children.Clear();
            SudokuGrid.RowDefinitions.Clear();
            SudokuGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < 9; i++)
            {
                SudokuGrid.RowDefinitions.Add(new RowDefinition());
                SudokuGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            string puzzle = CurrentPuzzle.Length >= 81 ? CurrentPuzzle : EmptyPuzzle;
            memory = CurrentPuzzle;

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    int index = row * 9 + col;
                    char c = puzzle[index];

                    if (_cellNotes[row, col] == null)
                        _cellNotes[row, col] = new CellNotes();

                    var cellContainer = new Grid();

                    bool isGiven = VM?.OriginalPuzzle != null
                       && VM.OriginalPuzzle.Length > index
                       && VM.OriginalPuzzle[index] != '0';

                    // --- TextBox principal ---
                    var tb = new TextBox
                    {
                        Text = c == '0' ? "" : c.ToString(),
                        FontSize = 24,
                        FontWeight = FontWeights.Bold,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        BorderBrush = Brushes.Gray,
                        Background = isGiven
                            ? new SolidColorBrush(Color.FromRgb(235, 235, 235))
                            : Brushes.White,
                        IsReadOnly = isGiven,
                        MaxLength = 1,
                        Width = 50,
                        Height = 50,
                        Margin = new Thickness(2)
                    };

                    tb.PreviewTextInput += (s, e) =>
                    {
                        e.Handled = !char.IsDigit(e.Text[0]) || e.Text == "0";
                    };

                    tb.TextChanged += TextBox_TextChanged;

                    // Quand on est en mode note, on ne veut pas que le TextBox soit cliquable
                    tb.IsHitTestVisible = !noteMode || tb.IsReadOnly;

                    // --- Panneau de notes 3x3 ---
                    var notesPanel = new UniformGrid
                    {
                        Rows = 3,
                        Columns = 3,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Margin = new Thickness(4),
                        Visibility = (noteMode && string.IsNullOrEmpty(tb.Text) && !tb.IsReadOnly)
                            ? Visibility.Visible
                            : Visibility.Collapsed,
                        IsHitTestVisible = true
                    };

                    var notes = _cellNotes[row, col].Notes;

                    for (int d = 1; d <= 9; d++)
                    {
                        int digit = d;
                        var btn = new System.Windows.Controls.Primitives.ToggleButton
                        {
                            Content = digit.ToString(),
                            FontSize = 10,
                            Margin = new Thickness(1),
                            IsChecked = notes[digit - 1] // on restaure l'état
                        };

                        btn.Click += (s, e) => NoteButton_Click(row, col, digit, (System.Windows.Controls.Primitives.ToggleButton)s!);

                        notesPanel.Children.Add(btn);
                    }

                    _notePanels[row, col] = notesPanel;

                    // Empile les deux dans la cellule
                    cellContainer.Children.Add(tb);
                    cellContainer.Children.Add(notesPanel);

                    // Bordures épaisses pour les blocs 3x3
                    Thickness thick = new Thickness(0.5);
                    if (col % 3 == 0) thick.Left = 3;
                    if (row % 3 == 0) thick.Top = 3;
                    if (col == 8) thick.Right = 3;
                    if (row == 8) thick.Bottom = 3;
                    tb.BorderThickness = thick;

                    Grid.SetRow(cellContainer, row);
                    Grid.SetColumn(cellContainer, col);
                    SudokuGrid.Children.Add(cellContainer);

                    textBoxes[row, col] = tb;
                }
            }
        }


        /// <summary>
        /// Handler NOMMÉ pour éviter CS0079 et pouvoir (dé)abonner proprement.
        /// </summary>
        private void TextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox tb || tb.IsReadOnly) return;

            tb.TextChanged -= TextBox_TextChanged;

            if (tb.Text.Length > 1)
                tb.Text = tb.Text[0].ToString();

            int row = Grid.GetRow((FrameworkElement)tb.Parent);   // le parent direct est cellContainer (Grid)
            int col = Grid.GetColumn((FrameworkElement)tb.Parent);

            char newChar = tb.Text.Length > 0 ? tb.Text[0] : '0';

            bool ok = UpdatePuzzle(row, col, newChar);

            var panel = _notePanels[row, col];

            if (!ok)
            {
                tb.Text = "";
                tb.Background = new SolidColorBrush(Color.FromRgb(255, 220, 220));
            }
            else
            {
                bool isEmpty = string.IsNullOrEmpty(tb.Text);

                // Si on met un chiffre, on cache les notes
                if (!isEmpty && panel != null)
                    panel.Visibility = Visibility.Collapsed;

                // Si on efface et qu'on est en mode note, on peut réafficher les notes
                if (isEmpty && panel != null)
                {
                    panel.Visibility = noteMode ? Visibility.Visible : Visibility.Collapsed;

                    tb.Background = noteMode
                        ? new SolidColorBrush(Color.FromRgb(230, 245, 255))
                        : Brushes.White;
                }
                else if (!tb.IsReadOnly)
                {
                    tb.Background = Brushes.White;
                }
            }

            tb.TextChanged += TextBox_TextChanged;
        }

        /// <summary>
        /// Met à jour la string Puzzle du VM (et interroge le VM/CoupScanner).
        /// Retourne true si accepté, false si invalide.
        /// </summary>
        private bool UpdatePuzzle(int row, int col, char value)
        {
            if (VM is null) return false;

            int digit = (value >= '1' && value <= '9') ? (value - '0') : 0;

            // MAJ du string puzzle côté VM (on accepte aussi vider la case => '0')
            int index = row * 9 + col;

            char[] chars = CurrentPuzzle.ToCharArray();
            chars[index] = (digit == 0) ? '0' : value;

            try
            {
                _updatingFromUI = true; // évite de re-créer la grille si le VM notifie Puzzle
                VM.Puzzle = new string(chars);
            }
            finally
            {
                _updatingFromUI = false;
            }

            return true;
        }

        // ✏️ Mode Note
        private void BtnNote_Click(object sender, RoutedEventArgs e)
        {
            noteMode = !noteMode;
            BtnNote.Content = noteMode ? "📝 Mode Normal" : "✏️ Mode Note";
            BtnNote.Background = noteMode
                ? new SolidColorBrush(Color.FromRgb(180, 220, 255))
                : new SolidColorBrush(Color.FromRgb(255, 238, 170));

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    int r = row; // 👈 copie locale
                    int c = col; // 👈 copie locale

                    int index = row * 9 + col;
                    char cPuzzle = Puzzle[index];

                    if (_cellNotes[r, c] == null)
                        _cellNotes[r, c] = new CellNotes();

                    var cellContainer = new Grid();

                    bool isGiven = VM?.OriginalPuzzle != null
                                   && VM.OriginalPuzzle.Length > index
                                   && VM.OriginalPuzzle[index] != '0';

                    var tb = new TextBox
                    {
                        Text = cPuzzle == '0' ? "" : cPuzzle.ToString(),
                        FontSize = 24,
                        FontWeight = FontWeights.Bold,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        BorderBrush = Brushes.Gray,
                        Background = isGiven
                            ? new SolidColorBrush(Color.FromRgb(235, 235, 235))
                            : Brushes.White,
                        IsReadOnly = isGiven,
                        MaxLength = 1,
                        Width = 50,
                        Height = 50,
                        Margin = new Thickness(2)
                    };

                    tb.PreviewTextInput += (s, e) =>
                    {
                        e.Handled = !char.IsDigit(e.Text[0]) || e.Text == "0";
                    };
                    tb.TextChanged += TextBox_TextChanged;
                    tb.IsHitTestVisible = !noteMode || tb.IsReadOnly;

                    var notesPanel = new UniformGrid
                    {
                        Rows = 3,
                        Columns = 3,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Margin = new Thickness(4),
                        Visibility = (noteMode && string.IsNullOrEmpty(tb.Text) && !tb.IsReadOnly)
                            ? Visibility.Visible
                            : Visibility.Collapsed
                    };

                    var notes = _cellNotes[r, c].Notes;

                    for (int d = 1; d <= 9; d++)
                    {
                        int digit = d;
                        bool isOn = notes[digit - 1];

                        var btn = new ToggleButton
                        {
                            Content = digit.ToString(),
                            FontSize = 10,
                            Margin = new Thickness(1),
                            IsChecked = isOn,
                            BorderThickness = new Thickness(0),
                            Background = Brushes.Transparent,
                            Opacity = isOn ? 0.35 : 1.0   // 👈 opacité liée à l’état
                        };

                        int r2 = row;
                        int c2 = col;
                        btn.Click += (s, e) => NoteButton_Click(r2, c2, digit, (ToggleButton)s!);

                        notesPanel.Children.Add(btn);
                    }

                    _notePanels[r, c] = notesPanel;

                    cellContainer.Children.Add(tb);
                    cellContainer.Children.Add(notesPanel);

                    Thickness thick = new Thickness(0.5);
                    if (c % 3 == 0) thick.Left = 3;
                    if (r % 3 == 0) thick.Top = 3;
                    if (c == 8) thick.Right = 3;
                    if (r == 8) thick.Bottom = 3;
                    tb.BorderThickness = thick;

                    Grid.SetRow(cellContainer, r);
                    Grid.SetColumn(cellContainer, c);
                    SudokuGrid.Children.Add(cellContainer);

                    textBoxes[r, c] = tb;
                }
            }
        }

        private void NoteButton_Click(int row, int col, int digit, ToggleButton btn)
        {
            var cell = _cellNotes[row, col];
            if (cell == null) return;

            bool isOn = btn.IsChecked == true;
            cell.Notes[digit - 1] = isOn;

            btn.Opacity = isOn ? 0.35 : 1.0;
        }

        private async void btn_Verif(object sender, RoutedEventArgs e)
        {
            if (VM is null) return;

            string current = (VM.Puzzle ?? new string('0', 81)).PadRight(81, '0');

            // On cherche la solution à partir du puzzle d’ORIGINE
            Sudoku? entry = await VM.FindSudokuByPuzzleAsync(VM.OriginalPuzzle);
            if (entry?.Solution is null || entry.Solution.Length < 81)
            {
                MessageBox.Show("Impossible de trouver la solution en base pour ce puzzle.",
                                "Vérification", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string solution = entry.Solution.Substring(0, 81);
            bool ok = string.Equals(current, solution, StringComparison.Ordinal);

            if (!ok)
            {
                VM.Puzzle = VM.OriginalPuzzle; // reset propre via VM
                MessageBox.Show("Ce n'est pas la bonne solution. La grille a été réinitialisée.",
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            gameTimer?.Stop();

            MessageBox.Show("Bravo ! Tu as réussi !",
                 "Vérification", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            SudokuSolved?.Invoke(this, EventArgs.Empty);

            FenetrePrincipale? fenetre = Window.GetWindow(this) as FenetrePrincipale;

            if (VM.Mode == "Compétition")
            {
                await VM.GestionPartie(fenetre.tokenUser.Id, isCompetition: true);
            }
            else
            {
                // Mode Entraînement : fermer la partie sans calculer de score
                await VM.GestionPartie(fenetre.tokenUser.Id, isCompetition: false);
            }
        }

        private async void btn_Solve_Click(object sender, RoutedEventArgs e)
        {
            if (VM is null) return;

            try
            {
                var entry = await VM.FindSudokuByPuzzleAsync(VM.OriginalPuzzle); //  clé : puzzle d’origine
                if (entry?.Solution is not null && entry.Solution.Length >= 81)
                {
                    VM.Puzzle = entry.Solution.Substring(0, 81);
                }
                else
                {
                    MessageBox.Show("Solution introuvable pour ce puzzle.",
                                    "Résoudre", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Impossible de récupérer la solution pour le moment.",
                                "Résoudre", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetTimer()
        {
            elapsedTime = TimeSpan.Zero;
            TimerTextBlock.Text = "00:00:00";
            gameTimer.Start();
        }
        private class CellNotes
        {
            public bool[] Notes { get; } = new bool[9];
        }

        private readonly CellNotes[,] _cellNotes = new CellNotes[9, 9];
        private readonly UniformGrid?[,] _notePanels = new UniformGrid?[9, 9];

        private async void btn_Retour_Click(object sender, RoutedEventArgs e)
        {
            FenetrePrincipale? fenetre = Window.GetWindow(this) as FenetrePrincipale;

            if (fenetre != null)
            {
                await SavePartieAsync(fenetre.tokenUser.Id);

                this.Visibility = Visibility.Collapsed;
                fenetre.Selection.Visibility = Visibility.Visible;
            }
        }
    }
}

