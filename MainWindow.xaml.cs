using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Dayfour
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Anima(btCulc, 40,274); // анимация кнопки
        }

        private void btCulc_Click(object sender, RoutedEventArgs e)
        { // след. окно
            Culculation culc = new Culculation();
            culc.Show();
            Close();
        }
        public static void Anima(Button name, double sizeFrom,double sizeTo)
        { // метод анимации кнопки
            DoubleAnimation buttonAnimation = new DoubleAnimation();
            buttonAnimation.From = 15;
            buttonAnimation.To = sizeTo;
            buttonAnimation.Duration = TimeSpan.FromSeconds(1);
            name.BeginAnimation(Button.WidthProperty, buttonAnimation);
        }
    }
}
