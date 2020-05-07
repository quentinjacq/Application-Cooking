using System;
using Projet;
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
using MySql.Data.MySqlClient;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Application_Cooking
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void Incripstion_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Inscription Formulaire = new Inscription();
            Formulaire.ShowDialog();
            this.Show();
        }

        private void Connexion_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string mdp =  Mdp.Password.ToString();
            bool result = Database.Identification(Database.maConnexion(), "0678548272", "comptines");
            if ((username == "")||(mdp == ""))
            {
                MessageBox.Show("Les valeurs ne peuvent pas être nulles.");
            }
            else
            {
                if (result)
                {
                    this.Hide();
                    Client client_connecte = Database.ClientConnecte(Database.maConnexion(), "0678548272");
                    
                    if (client_connecte.Est_gestionnaire_cooking == false)
                    {
                        if (client_connecte.Nbr_recettes_crees >0)
                        {
                            PageAccueuilCDR Pagecommande = new PageAccueuilCDR(client_connecte);
                            Pagecommande.ShowDialog();
                        }
                        else
                        {
                            PageCommandeClient Pagecommande = new PageCommandeClient(client_connecte);
                            Pagecommande.ShowDialog();
                        }
                    }
                    else
                    {
                        PageAccueuilGestionnaire Pagecommande = new PageAccueuilGestionnaire(client_connecte);
                        Pagecommande.ShowDialog();
                    }
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Le numéro de téléphone/email ou le mot de passe n'existe pas !");
                }
            }
  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Démo Demo = new Démo();
            Demo.ShowDialog();
            
        }
    }
}
