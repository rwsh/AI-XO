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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XO3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Base = new TBase();

            Pole = new TPole(gPole, Base);
        }

        private void cmClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        TPole Pole;

        TBase Base;

        private void cmRun(object sender, RoutedEventArgs e)
        {
            Pole = new TPole(gPole, Base);
        }

        private void cmCheck(object sender, MouseButtonEventArgs e)
        {
            if (Pole == null)
            {
                return;
            }

            if(!Pole.Can)
            {
                return;
            }

            Point p = e.GetPosition(gPole);

            int i, j;

            Pole.Get_ij(p.X, p.Y, out i, out j);

            Pole.Move(i, j);
        }
    }
}
