using MySqlX.XDevAPI.Common;
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
    /// Logique d'interaction pour Ajouter_Produits.xaml
    /// </summary>
    public partial class Ajouter_Produits : Window
    {
        string produit;
        string quantité;
        public Ajouter_Produits(Creation_Recette creation)
        {
            InitializeComponent();
            Autre_unite.IsReadOnly = true;
            Quantité_produit.IsReadOnly = true;
            unité.IsReadOnly = true;
        }


        private void Valider_Nom_produit_Click(object sender, RoutedEventArgs e)
        {
            if (Nom_produit.Text != "")
            {
                this.produit = Nom_produit.Text;
                unité.IsReadOnly = false;
            }
            else
            {
                this.produit = List_produits.SelectedItem.ToString();
                List<string[]> detail_produit = Database.DetailProduit(Database.maConnexion(), List_produits.SelectedItem.ToString());
                unité.SelectedItem = detail_produit[0][2];
            }

            Quantité_produit.IsReadOnly = false;
            
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            // Creation_Recette retour_creation = creation;
           //Creation_Recette creer = new Creation_Recette(retour_creation.Nom_recette, retour_creation.Descriptif_recette, retour_creation.Type_recette, retour_creation.Prix, retour_creation.Produits);
        }

        private void Ajouter_produit_Click(object sender, RoutedEventArgs e)
        {
            if (Quantité_produit.Text == "" || (unité.SelectedItem.ToString()!="Autre" && Autre_unite.Text == ""))
            {
                MessageBox.Show("Des zones n'ont pas été remplies.");
            }
            else
            {
                this.Hide();
                int stock_min = 3 * int.Parse(Quantité_produit.Text);
                int stock_max = 5 * int.Parse(Quantité_produit.Text);
                if (Nom_produit.Text != "" && unité.SelectedItem.ToString()!= "Autre")
                    {                    
                    Database.NvProduit(Database.maConnexion(), Nom_produit.Text, type_produit.Text, unité.SelectedItem.ToString(), 0, stock_min, stock_max);
                    }
                else if (Nom_produit.Text != "" && unité.SelectedItem.ToString() == "Autre")
                {
                    Database.NvProduit(Database.maConnexion(), Nom_produit.Text, type_produit.Text, Autre_unite.Text, 0, stock_min, stock_max);
                }
                //Dictionary<string, int> dict = creation.produits;
                //dict.Add(this.produit, int.Parse(Quantité_produit.Text));

                //Creation_Recette creer = new Creation_Recette(creation.Nom_recette, creation.Descriptif_recette, creation.Type_recette, creation.Prix, dict);
                //this.Hide();
                //Creation_of_recette window = new Creation_of_recette(creer);
                //window.ShowDialog();
                //this.Close();
            }
        }

        private void unité_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (unité.SelectedItem.ToString() == "Autre")
            {
                Autre_unite.IsReadOnly = true;
            }
            else
            {
                Autre_unite.IsReadOnly = false;
            }
        }
    }
}
