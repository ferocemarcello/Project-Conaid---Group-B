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
        public void RoundCoordinates(int decimals)
        {
            this.xCord = Math.Round(this.xCord, decimals);
            this.yCord = Math.Round(this.yCord, decimals);
            this.zCord = Math.Round(this.zCord, decimals);
        }
        public static _3Dpoint Subtract(_3Dpoint p2, _3Dpoint p1)
        {
            return new _3Dpoint(p2.GetX() - p1.GetX(), p2.GetY() - p1.GetY(), p2.GetZ() - p1.GetZ());
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

        public bool PointEqualsDelta(_3Dpoint p, double delta)
        {
            if (this.xCord >= p.xCord - delta && this.xCord <= p.xCord + delta &&
                this.yCord >= p.yCord - delta && this.yCord <= p.yCord + delta &&
                this.zCord >= p.zCord - delta && this.zCord <= p.zCord + delta )
                return true;
            else return false;
        }


        public double GetX()
        {
            return this.xCord;
        }
        public void SetX(double x)
        {
            this.xCord = x;
        }

        public double GetY()
        {
            return this.yCord;
        }
        public void SetY(double y)
        {
            this.yCord = y;
        }
        public double GetZ()
        {
            return this.zCord;
        }
        public void SetZ(double z)
        {
            this.zCord = z;
        }
        public string PointToString()
        {
            return this.xCord.ToString(CultureInfo.InvariantCulture) + " " + this.yCord.ToString(CultureInfo.InvariantCulture) + " " + this.zCord.ToString(CultureInfo.InvariantCulture);
        }

        public void RemoveEdges()
        {
            this.edges.Clear();
        }

        public double GetDistance(_3Dpoint p)
        {
            return Math.Sqrt(Math.Pow(this.GetX() - p.GetX(), 2) + Math.Pow(this.GetY() - p.GetY(), 2) + Math.Pow(this.GetZ() - p.GetZ(), 2));
        }
    }
}
