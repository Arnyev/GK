namespace GK1
{
    internal class NewtonRaphsonEquationSolver
    {
        private readonly IMatrixInverser _matrixInverser;

        public NewtonRaphsonEquationSolver(IMatrixInverser matrixInverser)
        {
            _matrixInverser = matrixInverser;
        }

        private void Iteration(QuadraticEquationSystem system, double[] x0, ref double[] x1)
        {
            var dimension = x0.Length;
            var jacobian = new double[dimension][];
            for (var index = 0; index < dimension; ++index)
                jacobian[index] = new double[dimension];
            var functionValues = new double[dimension];

            for (int i = 0; i < dimension; i++)
            {
                x1[i] = x0[i];
            }

            system.Jacobian(x0, ref jacobian);
            system.Calculate(x0, ref functionValues);
            double[][] inversed;
            try
            {
                _matrixInverser.InverseMatrix(jacobian, out inversed);

            }
            catch
            {
                return;
            }

            for (int i = 0; i < dimension; i++)
            {
                x1[i] = x0[i];
                for (int j = 0; j < dimension; j++)
                    x1[i] -= inversed[i][j] * functionValues[j];
            }
        }

        internal bool Solve(QuadraticEquationSystem system, double[] x0, out double[] x, int iterations, double precision, out int iterationsUsed)
        {
            var dimension = x0.Length;
            var x01 = new double[dimension];
            var y = new double[dimension];
            x = new double[dimension];
            x0.CopyTo(x01, 0);
            iterationsUsed = iterations;

            for (int index = 1; index <= iterations; ++index)
            {
                Iteration(system, x01, ref x);
                system.Calculate(x, ref y);

                double num = 0;
                for (int i = 0; i < y.Length; i++)
                    num += y[i] * y[i];

                if (num <= precision)
                {
                    iterationsUsed = index - 1;
                    return true;
                }

                x.CopyTo(x01, 0);
            }
            return false;
        }
    }
}
