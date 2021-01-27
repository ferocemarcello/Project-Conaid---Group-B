using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class Edge
    {
        private _3Dpoint point1;
        private _3Dpoint point2;
        public Edge(_3Dpoint p1,_3Dpoint p2)
        {
            if (p1.PointEquals(p2)) throw new Exception("the given points cannot be equals");
            this.point1 = p1;
            this.point2 = p2;
        }
        public bool EdgeEquals(Edge edge)
        {
            if (this.point1.PointEquals(edge.point1) && this.point2.PointEquals(edge.point2)) return true;
            return false;
        }
    }
}
