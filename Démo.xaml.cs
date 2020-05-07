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
    /// Logique d'interaction pour Démo.xaml
    /// </summary>
    public partial class Démo : Window
    {
        public Démo()
        {
            InitializeComponent();
            Demo1.Text = Database.NbClients(Database.maConnexion(), false).ToString();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Démo2 demo2 = new Démo2();
            demo2.ShowDialog();
            this.Close();
        }
    }
}
