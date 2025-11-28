using Game_On.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game_On.Views
{
    /// <summary>
    /// Logique d'interaction pour PageConnexion.xaml
    /// </summary>
    public partial class PageConnexion : UserControl
    {
        ConnexionVM ConnexionVM = new ConnexionVM();
        string message;
        bool connecte;
        public PageConnexion()
        {
            InitializeComponent();
            this.DataContext = ConnexionVM;
        }

        private async void btn_connexion_Click(object sender, RoutedEventArgs e)
        {
            FenetrePrincipale? fenetre = Window.GetWindow(this) as FenetrePrincipale;

            if (fenetre != null)
            {
                await ConnexionVM.Connexion();
                message = ConnexionVM.MessageConnexion;
                connecte = ConnexionVM.DejaConnecte;

                pseudo.Text = "";
                motdepasse.Password = "";

                if (!ConnexionVM.DejaConnecte)
                    return;

                fenetre.tokenUser = ConnexionVM.UtilisateurAuthentifie;

                this.Visibility = Visibility.Collapsed;
                fenetre.barregestion.Visibility = Visibility.Visible;
                fenetre.btn_Admin.Visibility = Visibility.Collapsed;
                fenetre.Selection.Visibility = Visibility.Visible;
            }
        }

        private void btn_inscription_Click(object sender, RoutedEventArgs e)
        {
            FenetrePrincipale? fenetre = Window.GetWindow(this) as FenetrePrincipale;
            this.Visibility = Visibility.Collapsed;

            if (fenetre != null)
            {
                fenetre.Inscription.Visibility = Visibility.Visible;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            ConnexionVM authVM = (ConnexionVM)this.DataContext;

            authVM.MotDePasse = passwordBox.Password;
        }
    }
}

