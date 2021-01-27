using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    class StraightLine3D
    {
        private _3Dpoint referementPoint;
        private _3Dpoint direction;//actually it's not a 3dpoint
        public double a { get; set; }
        public double b { get; set; }
        public double c { get; set; }
        public double d { get; set; }
        public double e { get; set; }
        public double f { get; set; }
        //{ax+by+c=0
        //{dy+ez+f=0
        public StraightLine3D(_3Dpoint p1,_3Dpoint p2)
        {
            this.direction = (_3Dpoint.Subtract(p2, p1));
            this.referementPoint = p1;
            /*(x-p1.GetX())/direction.GetX()=(y-p1.GetY())/direction.GetY();
             *(y-p1.GetY())/direction.GetY()=(z-p1.GetZ())/direction.GetZ(); 
             * 
             * 
             * */
            
        }
        public LinkedList<_3Dpoint> Intersection(StraightLine3D sl1,StraightLine3D sl2)
        {
            LinearMatrix m = new LinearMatrix(4, 3, sl1.a, sl1.b, 0, -sl1.c, 0, sl1.d, sl1.e, -sl1.f, sl2.a, sl2.b, 0, -sl2.c, 0, sl2.d, sl2.e, -sl2.f);
            m = m.GetStairMatrix();
            _3Dpoint p = new _3Dpoint(0, 0, 0);
            
            LinkedList<_3Dpoint> intersections = new LinkedList<_3Dpoint>();
            if (m.GetNumEq() == m.GetNumVars())
            {
                double z = m.GetCoefficientsAndClearValues()[11] / m.GetCoefficientsAndClearValues()[10];
                p.SetZ(z);
                double y = (m.GetCoefficientsAndClearValues()[7] - z) / m.GetCoefficientsAndClearValues()[5];
                p.SetY(y);
                double x = (m.GetCoefficientsAndClearValues()[3] - z - y) / m.GetCoefficientsAndClearValues()[0];
                intersections.AddLast(p);
            }
            return intersections;
        }
    }
}
