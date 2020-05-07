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
    /// Logique d'interaction pour Profil_Client.xaml
    /// </summary>
    public partial class Profil_Client : Window
    {
        public Profil_Client(Client Client_connecte)
        {
            InitializeComponent();
            Bonjour.Text = Bonjour.Text + Client_connecte.Prenom_client;
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rajouter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CDR_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deconecter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
