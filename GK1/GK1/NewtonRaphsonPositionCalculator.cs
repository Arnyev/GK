using System;
using System.Collections.Generic;
using System.Drawing;

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

        private static double BasicFunc(double[] x) => 0;

        public void CalculatePointsPosition(Point[] points, VH[] verticalHorizontals, int[] maxSizes, int startingIndex,
            int pointsCount)
        {
            var equations = new List<Func<double[], double>>();
            var derivatives = new List<List<Func<double[], double>>>();
            var x0 = new List<double>();

            double startingX = points[startingIndex].X;
            double startingY = points[startingIndex].Y;

            var variableMapArray = new int[pointsCount * 2];

            equations.Add(x => 100 * (x[0] - startingX));
            equations.Add(x => 100 * (x[1] - startingY));

            x0.Add(points[startingIndex].X);
            x0.Add(points[startingIndex].Y);

            derivatives.Add(new List<Func<double[], double>>());
            derivatives.Add(new List<Func<double[], double>>());

            derivatives[0].Add(x => 100);
            derivatives[0].Add(BasicFunc);
            derivatives[1].Add(BasicFunc);
            derivatives[1].Add(x => 100);

            int variableCount = 2;
            int equationCount = 2;
            variableMapArray[0] = 2 * startingIndex;
            variableMapArray[1] = 2 * startingIndex + 1;

            for (int i = 0; i < pointsCount; i++)
            {
                if (verticalHorizontals[i] == VH.Vertical)
                {
                    x0.Add(points[i].X);
                    x0.Add(points[(i + 1) % pointsCount].X);
                    derivatives.Add(new List<Func<double[], double>>());
                    for (int j = 0; j < variableCount; j++) //wypelnianie macierzy pochodnych
                    {
                        derivatives[j].Add(BasicFunc);
                        derivatives[j].Add(BasicFunc);
                    }
                    for (int j = 0; j < variableCount; j++)
                        derivatives[equationCount].Add(BasicFunc);

                    var currentCount = variableCount;
                    equations.Add(x => x[currentCount] - x[currentCount + 1]);

                    derivatives[equationCount].Add(x => 1);
                    derivatives[equationCount].Add(x => -1);

                    equationCount++;
                    variableMapArray[variableCount++] = 2 * i;
                    variableMapArray[variableCount++] = 2 * i + 2;
                }
            }
            var derivativeMatrix = new Func<double[], double>[variableCount, variableCount];
            var equationsVector = new Func<double[], double>[variableCount];
            for (int i = 0; i < variableCount; i++)
            {
                var curI = i;
                var startingValue = i % 2 == 0 ? points[i / 2].X : points[i / 2].Y;
                equationsVector[i] = x => x[curI] - startingValue;
                for (int j = 0; j < variableCount; j++)
                    derivativeMatrix[i, j] = BasicFunc;

                derivativeMatrix[i, i] = x => 1;
            }

            for (int i = 0; i < derivatives.Count; i++)
            {
                equationsVector[i] = equations[i];
                for (int j = 0; j < variableCount; j++)
                    derivativeMatrix[i, j] = derivatives[i][j];
            }
            if (equationCount > 1)
            {

            }
            var system = new QuadraticEquationSystem(equationsVector, derivativeMatrix);

            _solver.Solve(system, x0.ToArray(), out double[] results, MaxIterations, Precision);

            for (int i = 0; i < variableCount; i++)
            {
                var index = variableMapArray[i];
                if (index % 2 == 1)
                    points[index / 2] = new Point(points[index / 2].X, (int) results[i]);
                else
                    points[index / 2] = new Point((int) results[i], points[index / 2].Y);
            }
        }
    }
}
