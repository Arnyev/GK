using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GK1
{
    internal class NewtonRaphsonPositionCalculator : IPositionCalculator
    {
        private const double Precision = 200;
        private const int MaxIterations = 30;
        private readonly NewtonRaphsonEquationSolver _solver;

        public NewtonRaphsonPositionCalculator(NewtonRaphsonEquationSolver solver)
        {
            _solver = solver;
        }

        private static double BasicFunc(double[] x) => 0;
        private static double BasicDerivative(double[] x) => 1;
        private static double StartingDerivative(double[] x) => 100;


        public void CalculatePointsPosition(Point[] points, VH[] verticalHorizontals, int[] maxSizes, int startingIndex,
            int pointsCount, UsageData usageData)
        {
            if(pointsCount<2)
                return;

            var equationsVector = new Func<double[], double>[pointsCount * 2];
            var derivativeMatrix = new Func<double[], double>[pointsCount * 2, pointsCount * 2];
            for (int i = 0; i < pointsCount; i++)
            {
                for (int j = 0; j < pointsCount; j++)
                {
                    derivativeMatrix[2 * i, 2 * j] = BasicFunc;
                    derivativeMatrix[2 * i, 2 * j + 1] = BasicFunc;
                    derivativeMatrix[2 * i + 1, 2 * j] = BasicFunc;
                    derivativeMatrix[2 * i + 1, 2 * j + 1] = BasicFunc;
                }
                var startingX = (double) points[i].X;
                var startingY = (double) points[i].Y;
                int curI = i;
                equationsVector[2 * i] = x => x[2 * curI] - startingX;
                equationsVector[2 * i + 1] = x => x[2 * curI + 1] - startingY;
                derivativeMatrix[2 * i, 2 * i] = BasicDerivative;
                derivativeMatrix[2 * i + 1, 2 * i + 1] = BasicDerivative;
            }
            var startingIndexX = (double)points[startingIndex].X;
            var startingIndexY = (double)points[startingIndex].Y;

            equationsVector[2 * startingIndex] = x => 100 * (x[2 * startingIndex] - startingIndexX);
            equationsVector[2 * startingIndex + 1] = x => 100 * (x[2 * startingIndex + 1] - startingIndexY);

            derivativeMatrix[2 * startingIndex, 2 * startingIndex] = StartingDerivative;
            derivativeMatrix[2 * startingIndex + 1, 2 * startingIndex + 1] = StartingDerivative;

            for (int i = 0; i < pointsCount; i++)
            {
                if (i == startingIndex)
                {
                    continue;
                }

                if (verticalHorizontals[i] == VH.Vertical)
                {
                    var nextIndex = (i + 1) % pointsCount;
                    var curI = i;
                    equationsVector[2 * i] = x => x[2 * curI] - x[2 * nextIndex];
                    derivativeMatrix[2 * i, 2 * i] = x => 1;
                    derivativeMatrix[2 * i, 2 * nextIndex] = x => -1;
                }
                else if (verticalHorizontals[i] == VH.Horizontal)
                {
                    var nextIndex = (i + 1) % pointsCount;
                    var curI = i;
                    equationsVector[2 * i + 1] = x => x[2 * curI + 1] - x[2 * nextIndex + 1];
                    derivativeMatrix[2 * i + 1, 2 * i + 1] = x => 1;
                    derivativeMatrix[2 * i + 1, 2 * nextIndex + 1] = x => -1;
                }
                var indexForDistEquation = verticalHorizontals[i] == VH.Vertical ? 2 * i + 1 : 2 * i;

                if (maxSizes[i] > 0)
                {
                    var nextIndex = (i + 1) % pointsCount;
                    var curI = i;

                    var squareDist = maxSizes[i] * maxSizes[i];
                    equationsVector[indexForDistEquation] = x =>
                        (x[2 * curI] - x[2 * nextIndex]) * (x[2 * curI] - x[2 * nextIndex]) +
                        (x[2 * curI + 1] - x[2 * nextIndex + 1]) * (x[2 * curI + 1] - x[2 * nextIndex + 1]) -
                        squareDist;

                    derivativeMatrix[indexForDistEquation, 2 * curI] = x => 2 * (x[2 * curI] - x[2 * nextIndex]);
                    derivativeMatrix[indexForDistEquation, 2 * curI + 1] =
                        x => 2 * (x[2 * curI + 1] - x[2 * nextIndex + 1]);
                    derivativeMatrix[indexForDistEquation, 2 * nextIndex] = x => 2 * (x[2 * nextIndex] - x[2 * curI]);
                    derivativeMatrix[indexForDistEquation, 2 * nextIndex + 1] =
                        x => 2 * (x[2 * nextIndex + 1] - x[2 * curI + 1]);
                }
            }

            var system = new QuadraticEquationSystem(equationsVector, derivativeMatrix);
            var x0 = new double[pointsCount * 2];

            for (int i = 0; i < pointsCount; i++)
            {
                x0[2 * i] = points[i].X;
                x0[2 * i + 1] = points[i].Y;
            }
            var b = _solver.Solve(system, x0, out double[] results, MaxIterations, Precision, out int iterationsUsed);
            if (!b)
                usageData.ErrorCount++;
            usageData.IterationCount += iterationsUsed;
            usageData.CalculationCount++;

            for (int i = 0; i < pointsCount; i++)
            {
                points[i] = new Point((int) results[2 * i], (int) results[2 * i + 1]);
            }
        }
    }

    public class UsageData
    {
        public int ErrorCount;
        public int IterationCount;
        public int CalculationCount;
    }
}
