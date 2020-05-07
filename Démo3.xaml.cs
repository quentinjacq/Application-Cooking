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
    /// Logique d'interaction pour Démo3.xaml
    /// </summary>
    public partial class Démo3 : Window
    {
        public Démo3()
        {
            InitializeComponent();
            Demo3.Text = Database.NbRecette(Database.maConnexion()).ToString();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Démo4 demo4 = new Démo4();
            demo4.ShowDialog();
            this.Close();
        }
    }
}
