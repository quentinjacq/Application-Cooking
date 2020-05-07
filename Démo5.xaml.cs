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
    /// Logique d'interaction pour Démo5.xaml
    /// </summary>
    public partial class Démo5 : Window
    {
        public Démo5(string produit)
        {
            InitializeComponent();
            titre_demo5.Text = titre_demo5.Text + produit;
            List<string[]> prod = Database.DetailProduit(Database.maConnexion(), produit);
            for (int i = 0; i < prod.Count; i++)
            {
                list_démo5.Items.Insert(i, prod[i][0] + ", " + prod[i][1] + ", " + prod[i][2] + ".");
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.ShowDialog();
            this.Close();
        }
    }
}
