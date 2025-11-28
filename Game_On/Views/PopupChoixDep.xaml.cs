using Game_On.Models;
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
using System.Windows.Shapes;

namespace Game_On.Views
{
    /// <summary>
    /// Logique d'interaction pour PopupChoixDep.xaml
    /// </summary>
    public partial class PopupChoixDep : Window
    {
        PopupChoixDpartemntVM dpartemntVM;
        FenetrePrincipale principale;

        public PopupChoixDep(Utilisateur user, FenetrePrincipale principale)
        {
            dpartemntVM = new PopupChoixDpartemntVM(user);
            this.principale = principale;

            InitializeComponent();
            this.DataContext = dpartemntVM;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            PopupChoixDep? fenetre = Window.GetWindow(this) as PopupChoixDep;
            await dpartemntVM.Inscription(fenetre);

            principale.Inscription.Visibility = Visibility.Collapsed;
            principale.Connexion.Visibility = Visibility.Visible;
        }
    }
}
