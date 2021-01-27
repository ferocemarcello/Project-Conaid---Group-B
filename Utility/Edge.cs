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
            //if (p1.PointEquals(p2)) throw new Exception("the given points cannot be equals");
            this.point1 = p1;
            this.point2 = p2;
        }
        public bool EdgeEquals(Edge edge)
        {
            if (this.point1.PointEquals(edge.point1) && this.point2.PointEquals(edge.point2)) return true;
            return false;
        }

        public _3Dpoint GetPoint1()
        {
            return this.point1;
        }
        public _3Dpoint GetPoint2()
        {
            return this.point2;
        }

        public double GetLength()
        {
            return this.point1.GetDistance(this.point2);
        }
        public bool Contains(_3Dpoint p)
        {
            StraightLine2D line = new StraightLine2D(this.GetPoint1(), this.GetPoint2());
            if (!(line.Contains(p))) return false;
            return (p.GetDistance(this.GetPoint1()) <= this.GetLength()) && (p.GetDistance(this.GetPoint2()) <= this.GetLength());
        }
        public bool Intersect(Edge otherSeg)
        {
            if (Edge.IntersectionPoint(this, otherSeg) == null) return false;return true;
        }

        public static _3Dpoint IntersectionPoint(Edge thisSeg, Edge otherSeg)
        {
            StraightLine2D thisLine = new StraightLine2D(thisSeg.GetPoint1(), thisSeg.GetPoint2());
            StraightLine2D otherLine = new StraightLine2D(otherSeg.GetPoint1(), otherSeg.GetPoint2());
            _3Dpoint p = StraightLine2D.IntersectionPoint(thisLine, otherLine);
            if(p!=null) p.RoundCoordinates(0);
            if (p != null && (thisSeg.Contains(p) && otherSeg.Contains(p))) return p;return null;
        }
    }
}
