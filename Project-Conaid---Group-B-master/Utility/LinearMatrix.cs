using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class LinearMatrix
    {
        /*
         * 3 vars, 4 equations, 12 coefficients, 4 clear values
        |a b c | |x|  |n|
        |d e f | |y|= |o|
        |g h j | |z|  |p|
        |k l m |      |q|
        */
        private int numEq;
        private int numVars;
        private double[] coefficientsAndClearValues;
        public LinearMatrix(int numEq,int numVars,params double[] coefficientsAndClearValues)
        {
            this.numEq = numEq;
            this.numVars = numVars;
            this.coefficientsAndClearValues = coefficientsAndClearValues;
        }
        public LinearMatrix GetStairMatrix()
        {
            return null;
        }
        public int GetNumEq()
        {
            return this.numEq;
        }
        public int GetNumVars()
        {
            return this.numVars;
        }
        public double[] GetCoefficientsAndClearValues()
        {
            return this.coefficientsAndClearValues;
        }
    }
}
