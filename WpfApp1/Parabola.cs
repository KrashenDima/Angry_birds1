using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace WpfApp1
{
    class ParabolaFlight
    {
        readonly double V0;
        readonly double alpha;
        readonly double weight;
        readonly double g = 9.8;
        public List<Point_and_Velocity> Points = new List<Point_and_Velocity>();
        double splitting = 1;

        public ParabolaFlight(double V0, double alpha, double weight, double splitting)
        {
            this.V0 = V0;
            this.alpha = alpha * Math.PI / 180;
            this.splitting = splitting;
            this.weight = weight;
        }

        public Point_and_Velocity Get_Position(List<Point_and_Velocity> Points, Int32 i, double dt, double m, double k)
        {
            double x = Points[i - 1].x + dt * Points[i - 1].Vx;
            double Vx = Points[i - 1].Vx - dt * k * Points[i - 1].Vx / m;

            double y = Points[i - 1].y - dt * Points[i - 1].Vy;
            double Vy = Points[i - 1].Vy - dt * (g + k * Points[i - 1].Vy / m);

            return new Point_and_Velocity(x, y, Vx, Vy);
        }

        public void calculation(double x0, double y0)
        {
            double t = 0;
            double T = 2 * V0 * Math.Sin(alpha) / g;
            double dt = T / splitting;

            Point_and_Velocity pv = new
            Point_and_Velocity(x0, y0, V0 * Math.Cos(alpha), V0 * Math.Sin(alpha));
            Points.Add(pv);
            t += dt;
            Int32 i = 1;

            while (t < T)
            {
                double k = 1/2;
                Point_and_Velocity getpos =
                  Get_Position(Points, i, dt, weight, k);
                Points.Add(getpos);
                t += dt;
                i += 1;
            }
        }
        public void Read(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string s;

                while ((s = sr.ReadLine()) != null)
                {
                    string[] subs = s.Split();

                    if (!double.TryParse(subs[0], out double X))
                    {
                        return;
                    }

                    if (!double.TryParse(subs[1], out double Y))
                    {
                        return;
                    }

                    Point_and_Velocity p = new Point_and_Velocity(X, Y, 0, 0);
                    Points.Add(p);
                }
            }
        }

        public async void Write(string path)
        {
            using (StreamWriter outputFile = new StreamWriter(path))
            {
                foreach (Point_and_Velocity p in Points)
                {
                    await outputFile.WriteLineAsync($"{p.x} {p.y}");
                }
            }
        }
    }
}
