﻿using System;
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
    /// Logique d'interaction pour ConsulterRecetteCommande.xaml
    /// </summary>
    public partial class ConsulterRecetteCommande : Window
    {
        public ConsulterRecetteCommande(List<string[]> elements_recette)
        {
            InitializeComponent();
            Nom_recette.Text = elements_recette[0][0];
            prix.Text = elements_recette[0][2];
            desc.Text = elements_recette[0][1];
            type.Text = elements_recette[0][6];
            //salut sa va
            List<string[]> produits = Database.CheckProduitInRecette(Database.maConnexion(), Nom_recette.Text);
            for (int i = 0; i < produits.Count; i++)
            {
                ListView1.Items.Add(produits[i][0] + " " + produits[i][1] + " " + produits[i][2]);
            }
        }

        private void Ajouter_panier_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
