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
using System.Xml.Schema;


namespace Dayfour
{
    /// <summary>
    /// Логика взаимодействия для compare.xaml
    /// </summary>
    public partial class compare : Window
    {
        const int stabale = 8; //ставка по стабильному и по остальным
        const int optimal = 5;
        const int standart = 6;
        
        public compare(double[] Total, double[] TotalDohods)
        {
            InitializeComponent();
            MainWindow.Anima(btFormitate, 60, 350);
            Stabale.Text = TotalDohods[0].ToString();
            Optimal.Text = TotalDohods[1].ToString();
            Standart.Text = TotalDohods[2].ToString();

            SumStabale.Text = Total[0].ToString();
            SumOptimal.Text = Total[1].ToString();
            SumStandart.Text = Total[2].ToString();
        }

        private void btStability_Click(object sender, RoutedEventArgs e)
        {
            Autorization(stabale, Culculation.Total[0]);
        }

        private void btFormitate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Выписка сформирована!");
        }

        public static int selectedStavka;
        public static double lastSum;
        private void Autorization(int stavka, double summmactino)
        {   lastSum = summmactino;
            selectedStavka = stavka;
            Autorization auto = new Autorization();
            auto.Show();
            this.Close();}

        private void btOptimal_Click(object sender, RoutedEventArgs e)
        {
            Autorization(optimal, Culculation.Total[1]);
        }

        private void btStandart_Click(object sender, RoutedEventArgs e)
        {
            Autorization(standart, Culculation.Total[2]);
        }
    }
}
