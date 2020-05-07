using Renci.SshNet.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application_Cooking;
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
    /// Logique d'interaction pour PageCommandeClient.xaml
    /// </summary>
    public partial class PageCommandeClient : Window
    {
         public Client client;
           

         public PageCommandeClient(Client client_connecte)
         {
            InitializeComponent();
            Bonjour.Text = Bonjour.Text + client_connecte.Prenom_client;
            int num_page = 0;
            this.client = client_connecte;
            List<string[]> commande = Database.ListeRecette(Database.maConnexion(), 0, num_page, null, null);
            Titre1.Content = commande[0][0];
            text1.Text = commande[0][1];
            prix1.Text = commande[0][2];
            Titre2.Content = commande[1][0];
            text2.Text = commande[1][1];
            prix2.Text = commande[1][2];
            Titre3.Content = commande[2][0];
            text3.Text = commande[2][1];
            prix3.Text = commande[2][2];
            Titre4.Content = commande[3][0];
            text4.Text = commande[3][1];
            prix4.Text = commande[3][2];
            Titre5.Content = commande[4][0];
            text5.Text = commande[4][1];
            prix5.Text = commande[4][2];
            Titre6.Content = commande[5][0];
            text6.Text = commande[5][1];
            prix6.Text = commande[5][2];
         }


        private void recherche_Combo_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> commande = new List<string[]>();
            int type_recette;

            if (Combobox_Commande.Text == "Entrée")
            {
                type_recette = 1;
            }
            else if (Combobox_Commande.Text == "Dessert")
            {
                type_recette = 3;
            }
            else if (Combobox_Commande.Text == "plat principal")
            {
                type_recette = 2;
            }
            else if (Combobox_Commande.Text == "Boisson")
            {
                type_recette = 4;
            }
            else
            {
                type_recette = 0;
            }

            if (Rechercher.Text != "")
            {
                commande = Database.ListeRecette(Database.maConnexion(), type_recette, 0, null, Rechercher.Text);
            }
            else
            {
                commande = Database.ListeRecette(Database.maConnexion(), type_recette, 0, null, null);
            }


            text1.Text = commande[0][1];
            Titre1.Content = commande[0][0];
            prix1.Text = commande[0][2];
            text2.Text = commande[1][1];
            Titre2.Content = commande[1][0];
            prix2.Text = commande[1][2];
            text3.Text = commande[2][1];
            Titre3.Content = commande[2][0];
            prix3.Text = commande[2][2]; 
            text4.Text = commande[3][1]; 
            Titre4.Content = commande[3][0];
            prix4.Text = commande[3][2]; 
            text5.Text = commande[4][1]; 
            Titre5.Content = commande[4][0];
            prix5.Text = commande[4][2]; 
            text6.Text = commande[5][1]; 
            Titre6.Content = commande[5][0];
            prix6.Text = commande[5][2];

        }

        private void Page_prec_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void page_suiv_Click(object sender, RoutedEventArgs e)
        {

        }

        private void recherche_txt_Click(object sender, RoutedEventArgs e)
        {
            recherche_Combo_Click(sender, e);
        }

        private void Titre1_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> elements_recette = Database.ListeRecette(Database.maConnexion(), 0, 0, null, Titre1.Content.ToString());
            this.Hide();
            ConsulterRecetteCommande consulter = new ConsulterRecetteCommande(elements_recette);
            consulter.ShowDialog();
            this.Show();
        }

        private void Titre2_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> elements_recette = Database.ListeRecette(Database.maConnexion(), 0, 0, null, Titre1.Content.ToString());
            this.Hide();
            ConsulterRecetteCommande consulter = new ConsulterRecetteCommande(elements_recette);
            consulter.ShowDialog();
            this.ShowDialog();
        }

        private void Titre3_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> elements_recette = Database.ListeRecette(Database.maConnexion(), 0, 0, null, Titre1.Content.ToString());
            this.Hide();
            ConsulterRecetteCommande consulter = new ConsulterRecetteCommande(elements_recette);
            consulter.ShowDialog();
            this.ShowDialog();
        }


        private void Titre4_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> elements_recette = Database.ListeRecette(Database.maConnexion(), 0, 0, null, Titre1.Content.ToString());
            this.Hide();
            ConsulterRecetteCommande consulter = new ConsulterRecetteCommande(elements_recette);
            consulter.ShowDialog();
            this.ShowDialog();
        }

        private void Titre5_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> elements_recette = Database.ListeRecette(Database.maConnexion(), 0, 0, null, Titre1.Content.ToString());
            this.Hide();
            Consulter_Recette consulter = new Consulter_Recette(elements_recette);
            consulter.ShowDialog();
            this.ShowDialog();
        }

        private void Titre6_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> elements_recette = Database.ListeRecette(Database.maConnexion(), 0, 0, null, Titre1.Content.ToString());
            this.Hide();
            ConsulterRecetteCommande consulter = new ConsulterRecetteCommande(elements_recette);
            consulter.ShowDialog();
            this.ShowDialog();
        }

        private void Profil_Click(object sender, RoutedEventArgs e)
        {

            
        }
    }
}
