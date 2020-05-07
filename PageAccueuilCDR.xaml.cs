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
    /// Logique d'interaction pour PageAccueuilCDR.xaml
    /// </summary>
    public partial class PageAccueuilCDR : Window
    {
        public PageAccueuilCDR(Client client_connecte)
        {
            InitializeComponent();
            Bonjour.Text = Bonjour.Text + client_connecte.Prenom_client;
        }

        private void Confirmer_Modification_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ModificationProfil modifier = new ModificationProfil();
            modifier.ShowDialog();
            this.Close();

        }

        private void Passer_une_commande_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            //PageCommandeClient passer_commande = new PageCommandeClient(client_connecte);
            //passer_commande.ShowDialog();
            this.Close();
        }
    }
}
