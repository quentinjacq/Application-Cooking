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
    /// Logique d'interaction pour Creation_of_recette.xaml
    /// </summary>
    public partial class Creation_of_recette : Window
    {
        public void Ajouter_Produits (string produit, int quantite)
        {
            string list = "Produits: " + produit + "Quantité: " + quantite.ToString();
            List_Produits.Items.Add(list);
        }
        public Creation_of_recette(Creation_Recette creation)
        {
            InitializeComponent();
            Nom_Recette.Text = creation.Nom_recette;
            Desc_Recette.Text = creation.Descriptif_recette;
            Type_Recette.Text = creation.Type_recette;
            Prix_Recette.Text = creation.Prix.ToString();
            foreach (KeyValuePair<string, int> var in creation.Produits)
            {
                Ajouter_Produits(var.Key, var.Value);
            }
            
            // besoin de rajouter le dictionnaire produits en /all


        }

        private void Ajouter_produits_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            string nom_recette = Nom_Recette.Text;
            string descriptif = Desc_Recette.Text;
            string type = Type_Recette.Text;
            int prix =int.Parse(Prix_Recette.Text);
            //Creation_Recette creation = new Creation_Recette(nom_recette,descriptif,type,prix,produits);
            Ajouter_Produits ajouter =new Ajouter_Produits(this);
            ajouter.ShowDialog();
            this.Close();

        }

        private void Ajouter_recette_Click(object sender, RoutedEventArgs e)
        {
            int prix = int.Parse(Prix_Recette.Text);
            bool isnotok2 = true;
            try
            {
                int prix_test = Convert.ToInt32(prix);
            }
            catch
            {
                isnotok2 = false;
            }
            if (isnotok2 == false)
            {
                MessageBox.Show("Le prix doit être une valeur numérique");
            }
            else if (Nom_Recette.Text == "" || Desc_Recette.Text=="" || Type_Recette.Text=="" || Prix_Recette.Text=="" || List_Produits.Items.Count==0)
            {
                MessageBox.Show("Tous les champs n'ont pas été remplis.");
            }
            else
            {
                int index = 0;
                //string[,] produit= new string [creation.produits.Count,2];
                //foreach (KeyValuePair<string, int> langage in creation.Produits)
                //{
                //    index ++;
                //    string list = "Produits: " + langage.Key + "Quantité: " + langage.Value.ToString();
                //    produit[index, 0] = langage.Key;
                //    produit[index, 1] = langage.Value.ToString();
                //}

                //Database.NvRecette(Database.maConnexion(), creation.Nom_recette, creation.Type_recette, creation.Type_recette, creation.Prix, 2, DateTime.Now, client_connecte.num_tel, produit, 0);
                this.Close();
            }
        }
    }
}
