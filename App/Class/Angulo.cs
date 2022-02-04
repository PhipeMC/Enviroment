using System;
using System.Collections.Generic;
using System.Drawing;

namespace App.Class
{

    public class Angulo
    {
        Point p1;
        Point p2;
        Point p3;
        Double angle;

        public Angulo(Point pp1, Point pp2, Point pp3)
        {
            this.p1 = pp1;
            this.p2 = pp2;
            this.p3 = pp3;
            this.angle = calculateAngle();
        }

        public Angulo() { }

        public double calculateAngle()
        {

            double radio = (p2.Y * (p1.X - p3.X) + p1.Y * (p3.X - p2.X) + p3.Y * (p2.X - p1.X)) / (((p2.X - p1.X) * (p1.X - p3.X)) + ((p2.Y - p1.Y) * (p1.Y - p3.Y)));

            double angleRad = Math.Atan(radio);
            double angleDeg = (angleRad * 180) / Math.PI;

            if (angleDeg < 0)
            {
                angleDeg += 180;
            }

            return angleDeg;
        }

        public double calculateAngle(Point P1, Point P2, Point P3)
        {
            double numerator = P2.Y * (P1.X - P3.X) + P1.Y * (P3.X - P2.X) + P3.Y * (P2.X - P1.X);
            double denominator = (P2.X - P1.X) * (P1.X - P3.X) + (P2.Y - P1.Y) * (P1.Y - P3.Y);
            double ratio = numerator / denominator;

            double angleRad = Math.Atan(ratio);
            double angleDeg = (angleRad * 180) / Math.PI;

            if (angleDeg < 0)
            {
                angleDeg = 180 + angleDeg;
            }

            return angleDeg;
        }

        public double angulocal(Point P1, Point P2, Point P3)
        {
            //double result = atan2(P3.Y - P1.Y, P3.X - P1.X) - atan2(P2.Y - P1.Y, P2.X - P1.X);
            double angle = Math.Atan2(P1.X - P2.X, P2.Y - P1.Y);
            double angle2 = Math.Atan2(P2.X - P3.X, P3.Y - P2.Y);

            double Answer = 180 + angle2 - angle;

            if (Answer < 0)
            {
                return Answer + 360;
            }
            else
            {
                return Answer;
            }
        }

        public double calAngu(Point P1, Point P2, Point P3)
        {
            double result = Math.Atan2(P3.Y - P1.Y, P3.X - P1.X) - Math.Atan2(P2.Y - P1.Y, P2.X - P1.X);
            return result;
        }

        public double VectorAngle(List<Nodo> nodos)
        {
            // Find the vectors.
            PointF v1 = new PointF(nodos[1].centro.X - nodos[0].centro.X, nodos[1].centro.Y - nodos[0].centro.Y);
            PointF v2 = new PointF(nodos[2].centro.X - nodos[1].centro.X, nodos[2].centro.Y - nodos[1].centro.Y);

            // Calculate the vector lengths.
            double len1 = Math.Sqrt(v1.X * v1.X + v1.Y * v1.Y);
            double len2 = Math.Sqrt(v2.X * v2.X + v2.Y * v2.Y);

            // Use the dot product to get the cosine.
            double dot_product = v1.X * v2.X + v1.Y * v2.Y;
            double cos = dot_product / len1 / len2;

            // Use the cross product to get the sine.
            double cross_product = v1.X * v2.Y - v1.Y * v2.X;
            double sin = cross_product / len1 / len2;

            // Find the angle.
            double angle = Math.Acos(cos);
            if (sin < 0) angle = -angle;
            return angle;
        }
    }
}
