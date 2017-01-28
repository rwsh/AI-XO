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
    class TPole
    {
        public Canvas g;
        public double H;

        public TPosition Pos;
        char V;

        TBase Base;

        public TPole(Canvas g, TBase Base)
        {
            this.Base = Base;
        
            this.g = g;
            H = g.Height;
            g.Width = H;

            g.Children.Clear();

            DrawPole();

            Pos = new TPosition();
            V = 'X';

        }

        void DrawPole()
        {
            Brush br = Brushes.Blue;

            for (int i = 0; i < 4; i++)
            {
                Line L = new Line();
                L.Stroke = br;
                L.X1 = (H / 3) * i;
                L.X2 = L.X1;
                L.Y1 = 0;
                L.Y2 = H;
                L.StrokeThickness = 3;
                g.Children.Add(L);

                Line M = new Line();
                M.Stroke = br;
                M.Y1 = (H / 3) * i;
                M.Y2 = M.Y1;
                M.X1 = 0;
                M.X2 = H;
                M.StrokeThickness = 3;
                g.Children.Add(M);
            }
        }

        public bool Can
        {
            get
            {
                if(V == 'X')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Result Move(int i, int j)
        {
            int i1, j1, i2, j2;

            if (Pos.Who(out i1, out j1, out i2, out j2) != Result.Game)
            {
                return Pos.Who(out i1, out j1, out i2, out j2);
            }

            if(Pos.Mark(i, j, V))
            {
                DrawMark(i, j);

                if(V == 'X')
                {
                    V = 'O';
                }
                else
                {
                    V = 'X';
                }

                Result res = Pos.Who(out i1, out j1, out i2, out j2);

                if (res == Result.X)
                {
                    DrawLine(i1, j1, i2, j2);
                    //MessageBox.Show("Выиграли Крестики!");
                }

                if (res == Result.O)
                {
                    DrawLine(i1, j1, i2, j2);
                    //MessageBox.Show("Выиграли Нолики!");
                }

                if (res == Result.Non)
                {
                    MessageBox.Show("Ничья!");
                }

                if ((res == Result.Game)&&(V == 'O'))
                {
                    Run();
                }

                return res;
            }
            else
            {
                return Result.Error;
            }
        }

        TPosition PreMove(int i, int j)
        {
            TPosition res = Pos.Copy();

            if(res.Mark(i, j, V))
            {
                return res;
            }
            else
            {
                return null;
            }
        }

        void Run()
        {
            Base.Run(this);
        }

        void DrawLine(int i1, int j1, int i2, int j2)
        {
            double H3 = H / 3;

            Brush br = Brushes.Black;

            Line L;

            L = new Line();
            L.Stroke = br;
            L.StrokeThickness = 4;
            L.X1 = i1 * H3 + H3 / 2;
            L.Y1 = j1 * H3 + H3 / 2;
            L.X2 = i2 * H3 + H3 / 2;
            L.Y2 = j2 * H3 + H3 / 2;

            g.Children.Add(L);

        }

        void DrawMark(int i, int j)
        {
            double H3 = H / 3;

            if(V == 'X')
            {
                Brush br = Brushes.Red;

                Line L;
                double x0 = i * H3;
                double y0 = j * H3;

                L = new Line();
                L.Stroke = br;
                L.StrokeThickness = 2;
                L.X1 = x0 + 5;
                L.Y1 = y0 + 5;
                L.X2 = x0 + H3 - 5;
                L.Y2 = y0 + H3 - 5;
                g.Children.Add(L);

                L = new Line();
                L.Stroke = br;
                L.StrokeThickness = 2;
                L.X1 = x0 + H3 - 5;
                L.Y1 = y0 + 5;
                L.X2 = x0 + 5;
                L.Y2 = y0 + H3 - 5;
                g.Children.Add(L);
            }
            else
            {
                Ellipse O = new Ellipse();
                Brush br = Brushes.Green;
                O.Stroke = br;
                O.StrokeThickness = 2;
                O.Width = H3 - 10;
                O.Height = H3 - 10;
                O.Margin = new Thickness(i * H3 + 5, j * H3 + 5, 0, 0);
                g.Children.Add(O);

            }
        }

        public void Get_ij(double x, double y, out int i, out int j)
        {
            i = -1;
            j = -1;

            double H3 = H / 3.0;

            if (x < H3)
            {
                i = 0;
            }
            if ((x > H3) && (x < H3 * 2))
            {
                i = 1;
            }
            if (x > 2 * H3)
            {
                i = 2;
            }

            if (y < H3)
            {
                j = 0;
            }
            if ((y > H3) && (y < H3 * 2))
            {
                j = 1;
            }
            if (y > 2 * H3)
            {
                j = 2;
            }
        }

    }
}
