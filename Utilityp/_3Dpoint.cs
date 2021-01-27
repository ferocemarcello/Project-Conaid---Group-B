using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class _3Dpoint
    {
        private double zCord;
        private double xCord;
        private double yCord;
        private List<_3Dpoint> edges;

        public _3Dpoint(double xCord, double yCord, double zCord)
        {
            this.xCord = xCord;
            this.yCord = yCord;
            this.zCord = zCord;
            edges = new List<_3Dpoint>();
        }
        public List<_3Dpoint> GetEdges()
        {
            return this.edges;
        }
        public bool AddEdge(_3Dpoint p)
        {
            if (p.PointEquals(this) || edges.Count == 2 || (edges.Count != 0&&edges.First().PointEquals(p))) return false;
            this.edges.Add(p);return true;
        }
        public bool HavingEdge(_3Dpoint p)
        {
            if (edges.Count!=0&&(edges.First().PointEquals(p) || edges.Last().PointEquals(p))) return true;
            return false;
        }
        public bool PointEquals(_3Dpoint p)
        {
            if (this.xCord == p.xCord && this.yCord == p.yCord && this.zCord == p.zCord) return true;
            else return false;
        }

        public double GetX()
        {
            return this.xCord;
        }

        public double GetY()
        {
            return this.yCord;
        }

        public double GetZ()
        {
            return this.zCord;
        }
        public string PointToString()
        {
            return this.xCord.ToString(CultureInfo.InvariantCulture) + " " + this.yCord.ToString(CultureInfo.InvariantCulture) + " " + this.zCord.ToString(CultureInfo.InvariantCulture);
        }

        public void RemoveEdges()
        {
            this.edges.Clear();
        }
    }
}
