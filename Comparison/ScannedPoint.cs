using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Comparison
{
    public class ScannedPoint
    {
        public Vector3 point;
        public int collisions;
        public List<Polyhedra> collidedObjects = new List<Polyhedra>();

       public ScannedPoint(Vector3 point)
        {
            this.point = point;
        }

        internal void HasCollidedWith(Polyhedra poly)
        {
            collidedObjects.Add(poly);
            collisions++;
        }
    }
}
