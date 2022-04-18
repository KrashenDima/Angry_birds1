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
        //Vector relativeMousePos;
        //FrameworkElement draggedObject;


        public string velocity, angle, weight, splitting;
        public Window1()
        {
            InitializeComponent();
        }

        /*void StartDrag(object sender, MouseButtonEventArgs e)
        {
            draggedObject = (FrameworkElement)sender;
            relativeMousePos = e.GetPosition(draggedObject) - new Point();
            draggedObject.MouseMove += OnDragMove;
            draggedObject.LostMouseCapture += OnLostCapture;
            draggedObject.MouseUp += OnMouseUp;
            Mouse.Capture(draggedObject);
        }

        void OnDragMove(object sender, MouseEventArgs e)
        {
            UpdatePosition(e);
        }

        void UpdatePosition(MouseEventArgs e)
        {
            var point = e.GetPosition(DragArena);
            var newPos = point - relativeMousePos;
            Canvas.SetLeft(draggedObject, newPos.X);
            Canvas.SetTop(draggedObject, newPos.Y);
        }

        void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            FinishDrag(sender, e);
            Mouse.Capture(null);
        }

        void OnLostCapture(object sender, MouseEventArgs e)
        {
            FinishDrag(sender, e);
        }

        void FinishDrag(object sender, MouseEventArgs e)
        {
            draggedObject.MouseMove -= OnDragMove;
            draggedObject.LostMouseCapture -= OnLostCapture;
            draggedObject.MouseUp -= OnMouseUp;
            UpdatePosition(e);
        }*/

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

        void TimerOnTick(Point_and_Velocity pv)
        {
            Canvas.SetLeft(Bird, pv.x - Canvas.GetLeft(Bird));
            Canvas.SetTop(Bird, pv.y - Canvas.GetTop(Bird));
        }

        private void ButtonStartMove_Click(object sender, RoutedEventArgs e)
        {
            ParabolaFlight pf = new ParabolaFlight(Convert.ToDouble(velocity), Convert.ToDouble(angle), 
                Convert.ToDouble(weight), Convert.ToDouble(splitting));
            pf.calculation(Canvas.GetLeft(Bird), Canvas.GetTop(Bird));
            foreach(Point_and_Velocity pv in pf.Points)
            {
                DispatcherTimer tmr = new DispatcherTimer();
                tmr.Interval = TimeSpan.FromMilliseconds(100);
                tmr.Tick += TimerOnTick(pv);
                tmr.Start();
            }
        }
    }
}
