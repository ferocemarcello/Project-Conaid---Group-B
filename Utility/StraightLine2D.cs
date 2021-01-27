using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class StraightLine2D
    {
        private decimal m;
        private decimal q;
        private bool parallYAxis;

        public StraightLine2D(decimal m,decimal q)
        {
            this.m = m;
            this.q = q;
        }
        public StraightLine2D(_3Dpoint p1,_3Dpoint p2)
        {
            if (p1.GetX() == p2.GetX())
            {
                this.parallYAxis = true;
                //this.q = (decimal)p1.GetX();
                this.q = new decimal(p1.GetX());
                this.m = 0;
            }
            else
            {
                if (p1.GetY() == p2.GetY())
                {
                    this.m = 0;
                    //this.q = (decimal)p1.GetY();
                    this.q = new decimal(p1.GetY());
                    this.parallYAxis = false;
                }
                else
                {
                    decimal y1 = new decimal(p1.GetY());
                    decimal y2 = new decimal(p2.GetY());
                    decimal x1 = new decimal(p1.GetX());
                    decimal x2 = new decimal(p2.GetX());
                    this.m=decimal.Divide(decimal.Subtract(y2, y1), decimal.Subtract(x2, x1));
                    //this.m = ((decimal)p2.GetY() - (decimal)p1.GetY()) / ((decimal)p2.GetX() - (decimal)p1.GetX());
                    this.q = decimal.Subtract(y1, decimal.Multiply(m,x1));
                    //this.q = (decimal)p1.GetY() - (decimal)m * (decimal)p1.GetX();
                    this.parallYAxis = false;
                }
            }
            
        }
        public bool Contains(_3Dpoint p)
        {
            if (this.parallYAxis)
            {
                //if ((decimal)p.GetX() == this.q) return true;return false;
                if (new decimal(p.GetX()) == this.q) return true; return false;
            }
            else
            {
                //if ((decimal)this.m * (decimal)p.GetX() + (decimal)this.q == (decimal)p.GetY()) return true;return false;
                decimal x = new decimal(p.GetX());
                decimal y = new decimal(p.GetY());
                decimal a = decimal.Multiply(this.m, x);
                decimal b = decimal.Add(a, this.q);
                b = decimal.Round(b, 0);
                int delta = 1;
                if ((b >= y-delta) && (b <= y +delta))
                {
                    return true;
                }
                else return false;
//                if (decimal.Add(decimal.Multiply(this.m, new decimal(p.GetX())), this.q) == new decimal(p.GetY())) return true; return false;
            }
        }
        public static _3Dpoint IntersectionPoint(StraightLine2D line1, StraightLine2D line2)
        {
            double x = 0;
            double y = 0;
            bool sameM = false;
            bool bothParallelSameAxis = false;
            if (line1.m == line2.m) sameM = true;
            if (line1.m == 0 && line2.m == 0 && ((line1.parallYAxis && line2.parallYAxis) || (!(line1.parallYAxis) && !(line2.parallYAxis)))) bothParallelSameAxis = true;
            if (bothParallelSameAxis) return null;
            if (!bothParallelSameAxis && sameM)
            {
                if (line1.parallYAxis)
                {
                    //x = (double)line1.q;
                    x = decimal.ToDouble(line1.q);
                    //y = (double)line2.q;
                    y = decimal.ToDouble(line2.q);
                }
                if (line2.parallYAxis)
                {
                    //x = (double)line2.q;
                    x = decimal.ToDouble(line2.q);
                    //y = (double)line1.q;
                    y = decimal.ToDouble(line1.q);
                }
                return new _3Dpoint(x, y, 0);
            }
            if ((!sameM) && (line1.m == 0 || line2.m == 0))
            {
                if (line1.parallYAxis)
                {
                    //x = (double)line1.q;
                    x = decimal.ToDouble(line1.q);
                    //y = (double)(line2.m * (decimal)x + line2.q);
                    y = decimal.ToDouble(decimal.Add(decimal.Multiply(line2.m, new decimal(x)), line2.q));
                }
                if (line2.parallYAxis)
                {
                    //x = (double)line2.q;
                    x = decimal.ToDouble(line2.q);
                    //y = (double)(line1.m * (decimal)x + line1.q);
                    y = decimal.ToDouble(decimal.Add(decimal.Multiply(line1.m, new decimal(x)), line1.q));
                }
                if (line1.m == 0 && (!line1.parallYAxis))
                {
                    //y = (double)line1.q;
                    y = decimal.ToDouble(line1.q);
                    //x = (double)(((decimal)y - line2.q) / line2.m);
                    x = decimal.ToDouble(decimal.Divide(decimal.Subtract(new decimal(y), line2.q), line2.m));
                }
                if (line2.m == 0 && (!line2.parallYAxis))
                {
                    //y = (double)line2.q;
                    y = decimal.ToDouble(line2.q);
                    //x = (double)(((decimal)y - line1.q) / line1.m);
                    x = decimal.ToDouble(decimal.Divide(decimal.Subtract(new decimal(y), line1.q), line1.m));
                }
            }
            
            else
            {
                //m1x+q1=m2x+q2
                //x(m1-m2)=q2-q1
                //x=(q2-q1)/(m1-m2)

                //x = (double)((line2.q - line1.q) / (line1.m - line2.m));
                x = decimal.ToDouble(decimal.Divide(decimal.Subtract(line2.q, line1.q), decimal.Subtract(line1.m, line2.m)));
                //y =(double)(line1.m * (decimal)x + line1.q);
                y = decimal.ToDouble(decimal.Add(decimal.Multiply(line1.m, new decimal(x)), line1.q));
            }
            return new _3Dpoint(x, y, 0);
        }
    }
}
