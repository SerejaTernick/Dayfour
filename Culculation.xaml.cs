using System;
using System.Collections.Generic;
using System.Data;
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
using static System.Convert;



namespace Dayfour
{
    public partial class Culculation : Window
    {
        
        //попробовать разобраться со слайдерами

        public Culculation()
        {
            InitializeComponent();

            MainWindow.Anima(btCompare, 30,175);
        }
        private void tbMonthInpay_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

         //маассиив чтобы в след. окно передать полученные данные
        public static double[] Total = new double[3];
        public static double[] TotalDohods = new double[3];
        public static int period; //cрок
        public static double sum;

        private void btCompare_Click(object sender, RoutedEventArgs e)
        {
            tbShow1.Text = Math.Round(SlideSumm.Value, 2).ToString();
            tbShow2.Text = Math.Round(SlidePeriod.Value, 2).ToString();
            tbShow3.Text = Math.Round(SlidePay.Value, 2).ToString();

            TotalDohods[0] = Math.Round(SlideSumm.Value, 2);
            TotalDohods[1] = Math.Round(SlidePeriod.Value, 2);
            TotalDohods[2] = Math.Round(SlidePay.Value, 2);
            //Первый взнос
            sum = ToDouble(tbShow1.Text); 
            period = ToInt32(Math.Round(ToDouble(tbShow2.Text), 2)); 

            double EveryMonthMoney = ToDouble(tbShow3.Text); //ежемесячное
            

            //Рассчитываем, выводим и сохраняем стабильны доход
            double stabale = (sum * 8 * period) / (365 * 100);

            tbStabaleMoney.Text = "" + Math.Round(stabale, 2);

            Total[0] = sum + Math.Round(stabale, 2); // итоговый стейбл
            //считаем тарифы
            double optimal = 0;
            double standart = 0;
            double dohod; 

            while (period / 30 >= 1)
            {
                dohod = ((sum * 0.05) / 365) * 30;
                optimal += dohod;
                sum += EveryMonthMoney + dohod; //Сумма к концу срока
                period -= 30;
            }
            Total[1] = Math.Round(sum, 2); // итоговый оптимал

            //Возвращаем переменный к первоначальным значениям
            period = ToInt32(Math.Round(ToDouble(tbShow2.Text), 2));


            standart = (sum + EveryMonthMoney) * 0.06 * (period / 365);
            Total[2] = sum + Math.Round(standart, 2); // итоговый стандарт

            tbOptimalMoney.Text = "" + Math.Round(optimal, 2);
            tbStandartMoney.Text = "" + Math.Round(standart, 2);

            compare comp = new compare(Total, TotalDohods);
            comp.Show();
            this.Close();
        }

        private void Chet()
        {
            tbShow1.Text = Math.Round(SlideSumm.Value, 2).ToString();
            tbShow2.Text = Math.Round(SlidePeriod.Value, 2).ToString();
            tbShow3.Text = Math.Round(SlidePay.Value, 2).ToString();

            double sum = ToDouble(tbShow1.Text); //Первый взнос
            double period = ToDouble(tbShow2.Text); //День
            double EveryMonthMoney = ToDouble(tbShow3.Text); //Сумма пополнения

            //Рассчитываем, выводим и сохраняем стабильны доход
            double stabale = (sum * 8 * period) / (365 * 100);

            tbStabaleMoney.Text = "" + Math.Round(stabale, 2);

            //считаем тарифы
            double optimal = 0;
            double standart = 0;
            double dohod;

            while (period / 30 >= 1)
            {
                dohod = ((sum * 0.05) / 365) * 30;

                optimal += dohod;
                sum += dohod + EveryMonthMoney; //Сумма к концу срока

                period -= 30;
            }

            //Возвращаем переменный к первоначальным значениям
            sum = ToDouble(tbShow1.Text);
            period = ToDouble(tbShow2.Text);

            standart = (sum + EveryMonthMoney) * 0.06 * (period / 365);

            tbOptimalMoney.Text = "" + Math.Round(optimal, 2);
            tbStandartMoney.Text = "" + Math.Round(standart, 2);
        }

        private void SlidePay_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SlidePay != null && SlidePeriod != null && SlideSumm != null)
                Chet();
        }

        private void SlidePeriod_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SlidePay != null && SlidePeriod != null && SlideSumm != null)
                Chet();
        }

        private void SlideSumm_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SlidePay != null && SlidePeriod != null && SlideSumm != null)
                Chet();
        }
    }
}
