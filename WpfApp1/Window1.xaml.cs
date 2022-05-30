using System;
using System.Collections.Generic;
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
using Microsoft.Win32;


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
        public string fileName;
        public string fileMode = "default";

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
            

            if ((fileMode == "write" || fileMode == "read") && fileName == null)
            {
                MessageBox.Show("сначала нужно выбрать файл!");
                return;
            }

            if (fileMode != "read")
                pf.calculation(Canvas.GetLeft(Bird), Canvas.GetTop(Bird));

            if (fileMode == "write") pf.Write(fileName);

            if (fileMode == "read")
                pf.Read(fileName);

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

        private void PickFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;
                FileName.Content = "Путь файла: " + fileName;
            }
        }

        private void PickFileMode(object sender, RoutedEventArgs e)
        {
            if (!IsInitialized) return;

            if (e.OriginalSource is RadioButton radioButton)
            {
                MessageBox.Show("выбран режим: " + radioButton.Content, Title);

                if (radioButton.Name == "defaultRB")
                    fileMode = "default";
                else if (radioButton.Name == "writeRB")
                    fileMode = "write";
                else
                    fileMode = "read";
            }
        }
    }
}
