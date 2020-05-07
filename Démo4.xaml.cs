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
    /// Logique d'interaction pour Démo4.xaml
    /// </summary>
    public partial class Démo4 : Window
    {
        public Démo4()
        {
            InitializeComponent();
            List<string[]> list_prod = Database.Liste_Produit(Database.maConnexion(), true, true);
            for (int i = 0; i < list_prod.Count; i++)
            {
                list_démo4.Items.Insert(i, list_prod[i][0] + ", " + list_prod[i][1] + ", " + list_prod[i][2] + ".");
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Démo5 demo5 = new Démo5(Produit_demo.Text);
            demo5.ShowDialog();
            this.Close();
        }
    }
}
