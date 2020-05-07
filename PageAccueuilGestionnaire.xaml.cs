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

namespace Application_Cooking
{
    /// <summary>
    /// Logique d'interaction pour PageAccueuilGestionnaire.xaml
    /// </summary>
    public partial class PageAccueuilGestionnaire : Window
    {
        public PageAccueuilGestionnaire(Client client_connecte)
        {
            InitializeComponent();
            Bonjour.Text = Bonjour.Text + client_connecte.Prenom_client;
        }

        private void Recette_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Dictionary<string, int> produits = new Dictionary<string, int>();
           // Creation_Recette Creation_de_recette = new Creation_Recette("", "", "", produits);
           // Creation_of_recette recette = new Creation_of_recette();
           // recette.ShowDialog();
            this.Close();           
        }

        private void Gérer_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Page_GestiondelaBD gerer = new Page_GestiondelaBD();
            gerer.ShowDialog();
            this.Close();
        }

        private void Données_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Page_donnees donees = new Page_donnees();
            donees.ShowDialog();
            this.Close();
        }

        private void Passer_une_commande_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            //PageCommandeClient commande = new PageCommandeClient(client_connecte);
            //commande.ShowDialog();
            this.Close();
        }

        private void Renflouer_les_stocks_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Page_renflouer_stocks stocks = new Page_renflouer_stocks();
            stocks.ShowDialog();
            this.Close();
        }
    }
}
