using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

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

        public Triangle()
        {
        }

        public bool TriangleEquals(Triangle t)
        {
            if(this.point1.PointEquals(t.point1)|| this.point1.PointEquals(t.point2) || this.point1.PointEquals(t.point3))
            {
                if (this.point2.PointEquals(t.point1) || this.point2.PointEquals(t.point2) || this.point2.PointEquals(t.point3))
                {
                    if (this.point3.PointEquals(t.point1) || this.point3.PointEquals(t.point2) || this.point3.PointEquals(t.point3))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool OverLapAtLeastOneTriangle(LinkedList<Triangle> triangles)
        {
            foreach(Triangle t in triangles)
            {
                if (this.OverLapWith(t)) return true;
            }
            return false;
        }

        public bool OverLapWith(Triangle t)
        {
            //1.Compute plane equation of triangle 2.
            Plane p1 = Plane.CreateFromVertices(
                 CreateVectorFromPoint(t.point1),
                 CreateVectorFromPoint(t.point2),
                 CreateVectorFromPoint(t.point3));

            //2.Reject as trivial if all points of triangle 1 are on same side.
            float d1 = ComputeDistance(CreateVectorFromPoint(t.point1), p1);
            float d2 = ComputeDistance(CreateVectorFromPoint(t.point2), p1);
            float d3 = ComputeDistance(CreateVectorFromPoint(t.point3), p1);
            if (d1 == d2 && d2 == d3 && d3 == 0) return OverLapSamePlane(t);
            if (((d1 >= 0) && (d2 >= 0) && (d3 >= 0))
                ||
               ((d1 <= 0) && (d2 <= 0) && (d3 <= 0)))
                return false;
            
            //3.Compute plane equation of triangle 1.
            Plane p2 = Plane.CreateFromVertices(
                 CreateVectorFromPoint(this.point1),
                 CreateVectorFromPoint(this.point2),
                 CreateVectorFromPoint(this.point3));

            //4.Reject as trivial if all points of triangle 2 are on same side.
            float dd1 = ComputeDistance(CreateVectorFromPoint(this.point1), p2);
            float dd2 = ComputeDistance(CreateVectorFromPoint(this.point2), p2);
            float dd3 = ComputeDistance(CreateVectorFromPoint(this.point3), p2);

            if (dd1 == dd2 && dd2 == dd3 && dd3 == 0) return OverLapSamePlane(t);
            if (((dd1 >= 0) && (dd2 >= 0) && (dd3 >= 0))
                ||
               ((dd1 <= 0) && (dd2 <= 0) && (dd3 <= 0)))
                return false;

            //5.Compute intersection line and project onto largest axis.
            //direction of line l
            Vector3 d = Vector3.Cross(p1.Normal, p2.Normal);


            //6.Compute the intervals for each triangle.
            //7.Intersect the intervals.
            return true;
        }
        private bool Contains(Triangle t)
        {
            _3Dpoint p1T2 = new _3Dpoint(t.point1.GetX(), t.point1.GetY(), 0);
            _3Dpoint p2T2 = new _3Dpoint(t.point2.GetX(), t.point2.GetY(), 0);
            _3Dpoint p3T2 = new _3Dpoint(t.point3.GetX(), t.point3.GetY(), 0);
            LinkedList<_3Dpoint> otherVertexes = new LinkedList<_3Dpoint>();
            otherVertexes.AddLast(p1T2); otherVertexes.AddLast(p2T2); otherVertexes.AddLast(p3T2);
            int cont = 0;
            foreach (_3Dpoint p in otherVertexes)
            {
                if (this.ContainsPointInside(p)) cont++;
            }
            if (cont == 3) return true;return false;
        }

        private bool ContainsPointInside(_3Dpoint p)
        {
            _3Dpoint p1T1 = new _3Dpoint(this.point1.GetX(), this.point1.GetY(), 0);
            _3Dpoint p2T1 = new _3Dpoint(this.point2.GetX(), this.point2.GetY(), 0);
            _3Dpoint p3T1 = new _3Dpoint(this.point3.GetX(), this.point3.GetY(), 0);
            Edge segment1T1 = new Edge(p1T1, p2T1);
            Edge segment2T1 = new Edge(p1T1, p3T1);
            Edge segment3T1 = new Edge(p2T1, p3T1);
            if (segment1T1.Contains(p)|| segment2T1.Contains(p)||segment3T1.Contains(p))return true;
            StraightLine2D linePointParallYaxis = new StraightLine2D(p, new _3Dpoint(p.GetX(), p.GetY() + 1,0));
            StraightLine2D linePointParallXaxis = new StraightLine2D(p, new _3Dpoint(p.GetX()+1, p.GetY(), 0));
            LinkedList<_3Dpoint> interParallYaxis = this.intersectionPointsLine(linePointParallYaxis);
            LinkedList<_3Dpoint> interParallXaxis = this.intersectionPointsLine(linePointParallXaxis);
            bool insideParallYaxis = false;
            bool insideParallXaxis = false;
            if (interParallXaxis.Count <2 || interParallYaxis.Count <2) return false;
            if ((interParallYaxis.ToArray()[0].GetX() * interParallYaxis.ToArray()[1].GetX()) < 0) insideParallYaxis = true;
            if ((interParallXaxis.ToArray()[0].GetY() * interParallXaxis.ToArray()[1].GetY()) < 0) insideParallXaxis = true;
            if (insideParallXaxis && insideParallYaxis) return true;return false;
        }

        private LinkedList<_3Dpoint> intersectionPointsLine(StraightLine2D line)
        {
            Edge segment1 = new Edge(this.point1, this.point2);
            Edge segment2 = new Edge(this.point1, this.point3);
            Edge segment3 = new Edge(this.point2, this.point3);
            StraightLine2D lineSegment1 = new StraightLine2D(segment1.GetPoint1(), segment1.GetPoint2());
            StraightLine2D lineSegment2 = new StraightLine2D(segment2.GetPoint1(), segment2.GetPoint2());
            StraightLine2D lineSegment3 = new StraightLine2D(segment3.GetPoint1(), segment3.GetPoint2());
            LinkedList<_3Dpoint> points = new LinkedList<_3Dpoint>();
            _3Dpoint p1 = StraightLine2D.IntersectionPoint(lineSegment1, line);
            if (p1 != null && (segment1.Contains(p1)|| segment2.Contains(p1)||segment3.Contains(p1))) points.AddLast(p1);

            _3Dpoint p2 = StraightLine2D.IntersectionPoint(lineSegment2, line);
            if (p2 != null && (segment1.Contains(p2) || segment2.Contains(p2) || segment3.Contains(p2)))
            {
                if(!Manager.LinkedListOfPointsContains(points,p2)) points.AddLast(p2);
            }

            _3Dpoint p3 = StraightLine2D.IntersectionPoint(lineSegment3, line);
            if (p3 != null && (segment1.Contains(p3) || segment2.Contains(p3) || segment3.Contains(p3)))
            {
                if (!Manager.LinkedListOfPointsContains(points,p3)) points.AddLast(p3);
            }

            return points;
        }

        private bool OverLapSamePlane(Triangle t)
        {
            if (this.TriangleEquals(t)) return true;
            
            //declare the 6 points from the 2 triangles, with the z coordinate=0; so that the triangles are now in 2d  space
            _3Dpoint p1T1 = new _3Dpoint(this.point1.GetX(), this.point1.GetY(), 0);
            _3Dpoint p2T1 = new _3Dpoint(this.point2.GetX(), this.point2.GetY(), 0);
            _3Dpoint p3T1 = new _3Dpoint(this.point3.GetX(), this.point3.GetY(), 0);
            _3Dpoint p1T2 = new _3Dpoint(t.point1.GetX(), t.point1.GetY(), 0);
            _3Dpoint p2T2 = new _3Dpoint(t.point2.GetX(), t.point2.GetY(), 0);
            _3Dpoint p3T2 = new _3Dpoint(t.point3.GetX(), t.point3.GetY(), 0);

            //declare the 6 segments of the triangle
            Edge segment1T1 = new Edge(p1T1, p2T1);
            Edge segment2T1 = new Edge(p1T1, p3T1);
            Edge segment3T1 = new Edge(p2T1, p3T1);
            Edge segment1T2 = new Edge(p1T2,p2T2);
            Edge segment2T2 = new Edge(p1T2, p3T2);
            Edge segment3T2 = new Edge(p2T2, p3T2);

            //lists of segments
            LinkedList<Edge> thisTriangleSegments = new LinkedList<Edge>();
            LinkedList<Edge> otherTriangleSegments = new LinkedList<Edge>();

            //add to the lists the previously declared segments
            thisTriangleSegments.AddLast(segment1T1);
            thisTriangleSegments.AddLast(segment2T1);
            thisTriangleSegments.AddLast(segment3T1);
            otherTriangleSegments.AddLast(segment1T2);
            otherTriangleSegments.AddLast(segment2T2);
            otherTriangleSegments.AddLast(segment3T2);

            if (this.Contains(t) || t.Contains(this)) return true;

            //check the intersection for each pair of segments
            int numInterPoints = 0;
            foreach(Edge thisSeg in thisTriangleSegments)
            {
                foreach(Edge otherSeg in otherTriangleSegments)
                {
                    _3Dpoint p = Edge.IntersectionPoint(thisSeg, otherSeg);
                    if (p!=null)
                    {
                        if(!(p.PointEquals(thisSeg.GetPoint1()))&& !(p.PointEquals(thisSeg.GetPoint2()))&& !(p.PointEquals(otherSeg.GetPoint1()))&& !(p.PointEquals(otherSeg.GetPoint2())))
                        {
                            numInterPoints++;
                        }
                    }
                    //if (otherSeg.Intersect(thisSeg)) numInterPoints++;
                }
            }
            
            if (numInterPoints > 0) return true;
            return false;
            /*_3Dpoint interPoint = new _3Dpoint(x, y, 0);
            double dist1 = interPoint.GetDistance(segment1T1.GetPoint1());
            double dist2 = interPoint.GetDistance(segment1T1.GetPoint2());
            if (dist1 > segment1T1.GetLength() || dist2 > segment1T1.GetLength()) return false; else return true;*/
        }

        public static float ComputeDistance(Vector3 point, Plane plane)
        {
            float dot = Vector3.Dot(plane.Normal, point);
            float value = dot + plane.D;
            return value;
        }

        public static Vector3 CreateVectorFromPoint(_3Dpoint p)
        {
            return new Vector3( (float) p.GetX(), (float) p.GetY(), (float) p.GetZ());
        }

        public void UpdatePoint(_3Dpoint previous, _3Dpoint next)
        {
            if (point1.PointEquals(previous))
            {
                point2 = next;
            }
            if (point2.PointEquals(previous))
            {
                point2 = next;
            }
            if (point3.PointEquals(previous))
            {
                point3 = next;
            }
        }
        public string TriangleToString()
        {
            return this.point1.PointToString()+" "+this.point2.PointToString()+" "+this.point3.PointToString();
        }
    }
}
