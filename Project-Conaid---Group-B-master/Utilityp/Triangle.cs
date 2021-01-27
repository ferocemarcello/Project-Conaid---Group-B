using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class Triangle
    {
        private _3Dpoint point1;
        private _3Dpoint point2;
        private _3Dpoint point3;
        public Triangle(_3Dpoint p1, _3Dpoint p2, _3Dpoint p3)
        {
            if (p1.PointEquals(p2)||p1.PointEquals(p3)||p2.PointEquals(p3)) throw new Exception("the given points cannot be equals");
            this.point1 = p1;
            this.point2 = p2;
            this.point3 = p3;
        }
        public bool TriangleEquals(Triangle t)
        {
            if(this.point1.PointEquals(t.point1)|| this.point1.PointEquals(t.point2) || this.point1.PointEquals(t.point3))
            {
                if (this.point2.PointEquals(t.point2) || this.point2.PointEquals(t.point2) || this.point2.PointEquals(t.point3))
                {
                    if (this.point3.PointEquals(t.point2) || this.point3.PointEquals(t.point2) || this.point3.PointEquals(t.point3))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
