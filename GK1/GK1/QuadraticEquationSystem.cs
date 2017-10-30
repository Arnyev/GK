using System;

namespace GK1
{
    internal class QuadraticEquationSystem
    {
        private readonly Func<double[], double>[] _equations;
        private readonly Func<double[], double>[,] _derivatives;

        internal QuadraticEquationSystem(Func<double[], double>[] equations, Func<double[], double>[,] derivatives)
        {
            _equations = equations;
            _derivatives = derivatives;
        }

        internal void Jacobian(double[] x, ref double[][] jacobian)
        {
            int dimension = x.Length;
            for (int index = 0; index < dimension; ++index)
            {
                for (int j = 0; j < dimension; ++j)
                    jacobian[index][j] = _derivatives[index, j](x);
            }
        }

        internal void Calculate(double[] x, ref double[] y)
        {
            for (int i = 0; i < x.Length; i++)
                y[i] = _equations[i](x);
        }
    }
}
