using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeFBX
{
    public class Polyhedra
    {
        double minX, minY, minZ = double.MaxValue;
        double maxX, maxY, maxZ = double.MinValue;
        double delta = double.MinValue;

        public List<Vector3> points;

        public Polyhedra(List<Vector3> points) : this(points, 0)
        {
        }

        public Polyhedra(List<Vector3> points, double delta)
        {
            this.points = points;
            this.delta = delta;
            AnalyzeXYZ();
        }

        public Polyhedra(LinkedList<Utility._3Dpoint> points, double delta)
        {
            foreach (Utility._3Dpoint p in points) {
                this.points.Add(new Vector3((float)p.GetX(), (float)p.GetY(), (float)p.GetZ()));
            }
            this.delta = delta;
            AnalyzeXYZ();
        }

        private void AnalyzeXYZ()
        {
            foreach (Vector3 p in points)
            {
                if (p.X < minX)
                    minX = p.X;
                if (p.X > maxX)
                    maxX = p.X;

                if (p.Y < minY)
                    minY = p.Y;
                if (p.Y > maxY)
                    maxY = p.Y;

                if (p.Z < minZ)
                    minZ = p.Z;
                if (p.Z > maxZ)
                    maxZ = p.Z;

            }
        }

        public bool IsInside(Vector3 point)
        {
            return (point.X <= maxX && point.X >= minX
                && point.Y <= maxY && point.Y >= minY
                && point.Z <= maxZ && point.Z >= minZ);

        }

        public bool IsInsideDelta(Vector3 point)
        {
            return (point.X <= (maxX + delta) && point.X >= (minX - delta)
                && point.Y <= (maxY + delta) && point.Y >= (minY - delta)
                && point.Z <= (maxZ + delta) && point.Z >= (minZ - delta));

        }
    }

}
