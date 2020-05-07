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
    /// Logique d'interaction pour Inscription.xaml
    /// </summary>
    public partial class Inscription : Window
    {
        public Inscription()
        {
            InitializeComponent();
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void inscription_Click(object sender, RoutedEventArgs e)
        {
            string nom = this.Nom.Text;
            string prenom = this.Prenom.Text;
            string age1 = this.Age.Text;
            string email = this.Email.Text;
            string adresse = this.Adresse.Text;
            string ville = this.Ville.Text;
            string mdp = this.Mdp_inscrip.Password.ToString();
            string confirmer_mdp = this.Confirmer_mdp.Password.ToString();
            string num_tel = this.Num_tel.Text;
            bool isnotok = true;
            bool isnotok2 = true;
            try
            {
                int tel = Convert.ToInt32(num_tel);
            }
            catch
            {
                 isnotok = false;
            }
            try
            {
                int age2 = Convert.ToInt32(age1);
            }
            catch
            {
                isnotok2 = false;
            }

            if (nom == "" || prenom == "" || age1 == "" || adresse == "" || ville == "" || mdp == "" || confirmer_mdp == "" || num_tel == "" )
            {
                MessageBox.Show("Seul l'email est facultattif !");
            }
            else if (isnotok == false)
            {
                MessageBox.Show("Le numéro de Téléphone est incorrect !");
            }
            else if (isnotok2 == false)
            {
                MessageBox.Show("L'age est incorrect !");
            }
            else 
            { 
                if ((email != "")&&((Database.CheckMail(Database.maConnexion(),email)==false)|| Database.IsValidEmail(email)==false))
                {
                    MessageBox.Show("Cet email existe déjà ou est incorrect.");
                }
                else if (Database.CheckTel(Database.maConnexion(), num_tel) == false)
                {
                    MessageBox.Show("Ce numéro de téléphone existe déjà.");
                }
                else
                {
                    if (mdp == confirmer_mdp)
                    {
                        int age2 = int.Parse(age1);
                        if (email == "")
                        {
                            email = null;
                            Database.NvClient(Database.maConnexion(), num_tel, nom, prenom, email, age2, adresse, ville, mdp, 0, 0, false);                            
                        }
                        else
                        {
                            Database.NvClient(Database.maConnexion(), num_tel, nom, prenom, email, age2, adresse, ville, mdp, 0, 0, false);                           
                        }
                         
                    }
                    else
                    {
                        MessageBox.Show("Les mots de passe doivent être les mêmes.");
                    }

                    this.Close();
                    MessageBox.Show("Merci d'utiliser Cooking.");
                }
            }
        }
    }
}
