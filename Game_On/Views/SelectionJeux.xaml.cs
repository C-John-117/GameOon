using Game_On.Data.Context;
using Game_On.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game_On.Views
{
    /// <summary>
    /// Logique d'interaction pour SelectionJeux.xaml
    /// </summary>
    public partial class SelectionJeux : UserControl
    {

        FenetrePrincipale? fenetre;
        public SelectionJeux()
        {
            InitializeComponent();
            this.IsVisibleChanged += SelectionJeux_IsVisibleChanged;
        }

        private void SelectionJeux_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                fenetre = Window.GetWindow(this) as FenetrePrincipale;

                if (fenetre != null && fenetre.tokenUser != null && !string.IsNullOrEmpty(fenetre.tokenUser.Pseudo))
                {
                    txtPseudo.Text = $"Pseudo : {fenetre.tokenUser.Pseudo}";
                }


                if (PanelJeux.Children.Count == 0)
                {
                    ChargerJeux();
                }
            }
        }

        private void ChargerJeux()
        {
            using (var context = new ModelContext())
            {
                List<Jeu> jeux = context.Jeu.ToList();

                foreach (Jeu jeu in jeux)
                    PanelJeux.Children.Add(CreerCarteJeu(jeu));
            }
        }

        private Border CreerCarteJeu(Jeu jeu)
        {
            Border carte = new Border
            {
                Margin = new Thickness(10),
                Background = new SolidColorBrush(Color.FromRgb(40, 40, 40)),
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(15)
            };

            // Grid pour permettre l'overlay
            Grid gridPrincipal = new Grid();

            StackPanel contenu = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10)
            };

            // Image
            Image image = new Image
            {
                Width = 80,
                Height = 80,
                Margin = new Thickness(0, 0, 20, 0)
            };

            // Infos du jeu
            StackPanel infoPanel = new StackPanel();
            infoPanel.Children.Add(new TextBlock
            {
                Text = jeu.NomJeu,
                FontSize = 20,
                Foreground = Brushes.White,
                Margin = new Thickness(0, 0, 0, 5)
            });

            // Boutons horizontaux
            StackPanel boutonsPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(30, 0, 0, 0)
            };

            Button btnClassement = CreerBouton("Classement", Brushes.SteelBlue);
            btnClassement.Click += (s, e) => AfficherClassement();

            Button btnEntrainement = CreerBouton("Entraînement", Brushes.MediumSeaGreen);
            Button btnCompetition = CreerBouton("Compétition", Brushes.OrangeRed);

            // Crée les panneaux de difficultés (cachés au départ)
            StackPanel panelEntrainement = CreerPanelDifficultes(true);
            panelEntrainement.Visibility = Visibility.Collapsed;

            StackPanel panelCompetition = CreerPanelDifficultes(false);
            panelCompetition.Visibility = Visibility.Collapsed;

            // Toggle pour Entraînement
            btnEntrainement.Click += (s, e) =>
      {
          if (panelEntrainement.Visibility == Visibility.Visible)
          {
              panelEntrainement.Visibility = Visibility.Collapsed;
          }
          else
          {
              panelEntrainement.Visibility = Visibility.Visible;
              panelCompetition.Visibility = Visibility.Collapsed;
          }
      };

            // Toggle pour Compétition
            btnCompetition.Click += (s, e) =>
       {
           if (panelCompetition.Visibility == Visibility.Visible)
           {
               panelCompetition.Visibility = Visibility.Collapsed;
           }
           else
           {
               panelCompetition.Visibility = Visibility.Visible;
               panelEntrainement.Visibility = Visibility.Collapsed;
           }
       };

            boutonsPanel.Children.Add(btnClassement);
            boutonsPanel.Children.Add(btnEntrainement);
            boutonsPanel.Children.Add(panelEntrainement);
            boutonsPanel.Children.Add(btnCompetition);
            boutonsPanel.Children.Add(panelCompetition);

            contenu.Children.Add(image);
            contenu.Children.Add(infoPanel);
            contenu.Children.Add(boutonsPanel);

            gridPrincipal.Children.Add(contenu);

            // Si le jeu n'est pas disponible, ajoute un overlay
            if (!jeu.EstDisponible)
            {
                Border overlay = new Border
                {
                    Background = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0)), // Noir semi-transparent
                    CornerRadius = new CornerRadius(15)
                };

                StackPanel overlayContent = new StackPanel
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                overlayContent.Children.Add(new TextBlock
                {
                    Text = "🔒 BIENTÔT DISPONIBLE",
                    FontSize = 24,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center
                });

                overlay.Child = overlayContent;
                gridPrincipal.Children.Add(overlay);

                btnClassement.IsEnabled = false;
                btnEntrainement.IsEnabled = false;
                btnCompetition.IsEnabled = false;
            }

            carte.Child = gridPrincipal;
            return carte;
        }

        private StackPanel CreerPanelDifficultes(bool isTraining)
        {
            StackPanel panel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(5, 0, 5, 0)
            };

            Button facile = CreerBouton("Easy", Brushes.LightGreen);
            Button moyen = CreerBouton("Medium", Brushes.Goldenrod);
            Button difficile = CreerBouton("Hard", Brushes.IndianRed);

            if (isTraining)
            {
                facile.Click += (s, e) => AfficherSudokuEntrainement("Easy");
                moyen.Click += (s, e) => AfficherSudokuEntrainement("Medium");
                difficile.Click += (s, e) => AfficherSudokuEntrainement("Hard");
            }
            else
            {
                facile.Click += (s, e) => AfficherSudoku("Easy");
                moyen.Click += (s, e) => AfficherSudoku("Medium");
                difficile.Click += (s, e) => AfficherSudoku("Hard");
            }

            panel.Children.Add(facile);
            panel.Children.Add(moyen);
            panel.Children.Add(difficile);

            return panel;
        }

        private Button CreerBouton(string texte, Brush couleur)
        {
            return new Button
            {
                Content = texte,
                Background = couleur,
                Foreground = Brushes.White,
                Margin = new Thickness(5, 0, 0, 0),
                Padding = new Thickness(10, 5, 10, 5),
                BorderThickness = new Thickness(0),
                Width = 95
            };
        }

        private void AfficherSudokuEntrainement(string Difficulty)
        {
            fenetre = Window.GetWindow(this) as FenetrePrincipale;

            this.Visibility = Visibility.Collapsed;

            fenetre.Jeu.ChooseDifficultyTraining(Difficulty);
            fenetre.Jeu.Visibility = Visibility.Visible;
        }
        private void AfficherClassement()
        {
            fenetre = Window.GetWindow(this) as FenetrePrincipale;
            this.Visibility = Visibility.Collapsed;

            fenetre.Classement.Visibility = Visibility.Visible;
        }
        private void AfficherSudoku(string diff)
        {
            fenetre = Window.GetWindow(this) as FenetrePrincipale;
            this.Visibility = Visibility.Collapsed;
            fenetre.Jeu.ChooseDifficulty(diff);
            fenetre.Jeu.Visibility = Visibility.Visible;
        }
    }
}
