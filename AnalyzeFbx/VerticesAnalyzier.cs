using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using System.Numerics;

namespace AnalyzeFbx
{
    public class VerticesAnalyzier
    {

        static public LinkedList<Utility._3Dpoint> OccourrenceMoreThanOne(LinkedList<Utility._3Dpoint> input)
        {
            LinkedList<Utility._3Dpoint> output = new LinkedList<Utility._3Dpoint>();

            foreach (Utility._3Dpoint p in input)
            {
                List<Utility._3Dpoint> x = input.Where(q => q.PointEquals(p)).ToList();
                if (x.Count > 1)
                {
                    output.AddLast(p);
                }
            }

            return RemoveEqualsDelta(output);
        }

        private static LinkedList<_3Dpoint> RemoveEqualsDelta(LinkedList<_3Dpoint> output)
        {
            LinkedList<Utility._3Dpoint> result = new LinkedList<Utility._3Dpoint>();

            foreach (Utility._3Dpoint p in output)
            {
                List<Utility._3Dpoint> x = result.Where(q => q.PointEqualsDelta(p,1)).ToList();
                if (x.Count == 0)
                {
                    result.AddLast(p);
                }
            }

            return result;
        }

        public static List<Plane> PlanesForPoints(List<System.Numerics.Vector3> points)
        {
            double x = points[0].X;
            if (points[1].X == x && points[2].X == x)
                return new List<Plane> { Plane.CreateFromVertices(points[0], points[1], points[2]) };

            double y = points[0].Y;
            if (points[1].Y == y && points[2].Y == y)
                return new List<Plane> { Plane.CreateFromVertices(points[0], points[1], points[2]) };

            double z = points[0].Z;
            if (points[1].Z == z && points[2].Z == z)
                return new List<Plane> { Plane.CreateFromVertices(points[0], points[1], points[2]) };
            throw new NotImplementedException();
        }

        public static Plane PlaneForWall(List<Vector3> points)
        {
            return Plane.CreateFromVertices(points[0], points[2], points[3]);
        }
    }
}
