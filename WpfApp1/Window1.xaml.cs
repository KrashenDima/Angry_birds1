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
using System.Drawing;
using System.IO;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        public string velocity, angle, weight, splitting;
        ParabolaFlight pf;
        int i = 1;

        public Window1()
        {
            InitializeComponent();
        }

        private void Get_Velocity(object sender, TextChangedEventArgs e)
        {
            velocity = Velocity.Text;
        }

        private void Get_Angle(object sender, TextChangedEventArgs e)
        {
            angle = Angle.Text;
        }

        private void Get_Weight(object sender, TextChangedEventArgs e)
        {
            weight = Weight.Text;
        }

        private void Get_Splitting(object sender, TextChangedEventArgs e)
        {
            splitting = Splitting.Text;
        }

        private void ButtonStartMove_Click(object sender, RoutedEventArgs e)
        {
            pf = new ParabolaFlight(Convert.ToDouble(velocity), Convert.ToDouble(angle),
               Convert.ToDouble(weight), Convert.ToDouble(splitting));
            pf.calculation(Canvas.GetLeft(Bird), Canvas.GetTop(Bird));

            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromMilliseconds(0.5);
            tmr.Tick += new EventHandler(TimerOnTick);
            tmr.Start();

            if (i >= pf.Points.Count)
                tmr.Stop();
            i = 0;
        }

        private void ButtonReturnToStart_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(Bird, pf.Points[0].x);
            Canvas.SetTop(Bird, pf.Points[0].y);
        }

        void TimerOnTick (object sender, EventArgs e)
        {
            if (i < pf.Points.Count)
            {
                Canvas.SetLeft(Bird, pf.Points[i].x);
                Canvas.SetTop(Bird, pf.Points[i].y);
            }
            i++;
        }
    }
}
