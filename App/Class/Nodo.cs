using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace App.Class
{
    public class Nodo
    {
        static int orden = 0;

        int num;
        public Point centro;
        Rectangle rec;

        GraphicsPath gp;
        Pen circ;
        Pen lin;
        List<Nodo> conec;

        public Nodo(int x, int y)
        {
            centro = new Point(x, y);
            rec = new Rectangle(new Point(x - 3, y - 3), new Size(6, 6));

            gp = new GraphicsPath();
            gp.AddRectangle(rec);

            circ = new Pen(Brushes.Black, 3);

            num = ++orden;
            conec = new List<Nodo>();
            Color line = Color.FromArgb(5, 59, 195);

            lin = new Pen(new SolidBrush(line), 1);
            GraphicsPath gpLin = new GraphicsPath();
            gpLin.AddLine(new Point(0, 0), new Point(3, -3));
            gpLin.AddLine(new Point(0, 0), new Point(-3, -3));
        }

        public virtual void DibujaNodo(Graphics g)
        {
            g.FillPath(new SolidBrush(Color.FromArgb(17, 81, 242)), gp);
        }

        public virtual void DibujaArista(Graphics g)
        {
            foreach (Nodo item in conec)
            {
                Point p = new Point(item.centro.X, item.centro.Y);
                g.DrawLine(lin, centro, p);
            }
        }

        public bool Adentro(Point pt)
        {
            return gp.IsVisible(pt);
        }

        public void Posicion(Point pt)
        {
            gp.Transform(new Matrix(1, 0, 0, 1, pt.X - centro.X, pt.Y - centro.Y));
            centro = pt;
            rec = Rectangle.Round(gp.GetBounds());
        }

        public void ConectarA(Nodo n)
        {
            conec.Add(n);
        }

        public void Desconectar(Nodo n)
        {
            conec.Remove(n);
        }
    }
}
