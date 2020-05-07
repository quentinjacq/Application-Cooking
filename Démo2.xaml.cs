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
    /// Logique d'interaction pour Démo2.xaml
    /// </summary>
    public partial class Démo2 : Window
    {
        public Démo2()
        {
            InitializeComponent();
            List<string[]> cdr_recettes = Database.CdR_recettes(Database.maConnexion());
            for (int i = 0; i < cdr_recettes.Count; i++)
            {
                list_démo2.Items.Insert(i, cdr_recettes[i][0] + ", " + cdr_recettes[i][1] + ", " + cdr_recettes[i][2] + ".");
            }
        }

        private void list_démo2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
                
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Démo3 demo3 = new Démo3();
            demo3.ShowDialog();
            this.Close();
        }
    }
}
