using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace GK1
{
    internal class NewtonRaphsonPositionCalculator : IPositionCalculator
    {
        private const double Precision = 0.5;
        private const int MaxIterations = 20;
        private readonly NewtonRaphsonEquationSolver _solver;

        public NewtonRaphsonPositionCalculator(NewtonRaphsonEquationSolver solver)
        {
            _solver = solver;
        }

        private double BasicFunc(double[] x) => 0;

        public void CalculatePointsPosition(Point[] points, VH[] verticalHorizontals, int[] maxSizes, int startingIndex,
            int pointsCount)
        {
            var equations = new Func<double[], double>[pointsCount * 2];
            var derivatives = new Func<double[], double>[pointsCount * 2, pointsCount * 2];
            var variables = new double[pointsCount * 2];


           double startingX = points[startingIndex].X;
           double startingY = points[startingIndex].Y;

            equations[0] = x =>
                100 * (Math.Abs(x[startingIndex * 2] - startingX) + Math.Abs(x[startingIndex * 2 + 1] - startingY));

            for (int i = 0; i < pointsCount; i++)
                derivatives[0, i] = BasicFunc;

            derivatives[0, 2 * startingIndex] = x => x[2 * startingIndex] > startingX ? -100 : 100;
            derivatives[0, 2 * startingIndex + 1] = x => x[2 * startingIndex + 1] > startingY ? -100 : 100;

            var currentRelationCount = 1;

            for (int i = 0; i < pointsCount; i++)
            {
                variables[2 * i] = points[i].X;
                variables[2 * i + 1] = points[i].Y;

                if (currentRelationCount > 2 * pointsCount)
                    throw new ArgumentException("Za dużo relacji");

                if (maxSizes[i] > 0)
                {
                    
                }
                if (verticalHorizontals[i] == VH.Horizontal)
                {
                    
                }
                if (verticalHorizontals[i] == VH.Vertical)
                {

                }
            }
            for (int i = currentRelationCount; i < pointsCount * 2; i++)
            {
                equations[i] = BasicFunc;
                for (int j = 0; j < pointsCount*2; j++)
                    derivatives[i,j] = BasicFunc;
            }

            var system = new QuadraticEquationSystem(equations, derivatives);

            _solver.Solve(system, variables, out double[] results, MaxIterations, Precision);

            for (int i = 0; i < pointsCount; i++)
                points[i] = new Point((int) results[2 * i], (int) results[2 * i + 1]);
        }
    }
}
