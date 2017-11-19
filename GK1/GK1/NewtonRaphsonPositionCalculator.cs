using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GK1
{
    internal class NewtonRaphsonPositionCalculator : IPositionCalculator
    {
        private const double Precision = 20;
        private const int MaxIterations = 100;
        private readonly NewtonRaphsonEquationSolver _solver;

        public NewtonRaphsonPositionCalculator(NewtonRaphsonEquationSolver solver)
        {
            _solver = solver;
        }

        private static double ZeroFunc(double[] x) => 0;
        private static double OneFunc(double[] x) => 1;
        private static double TenFunc(double[] x) => 10;

        private static Func<double[], double> GetQuadraticFunc(int x1, int x2, int y1, int y2, double val) =>
            x => (x[x1] - x[x2]) * (x[x1] - x[x2]) + (x[y1] - x[y2]) * (x[y1] - x[y2]) - val;

        private static Func<double[], double> GetQuadraticDerivative(int i, int j) => x => 2 * (x[i] - x[j]);

        private static Func<double[], double> GetForcingStartFunc(int i, double startingValue) =>
            x => 10 * x[i] - startingValue;

        public void CalculatePointsPosition(Point[] points, VH[] verticalHorizontals, int[] maxSizes, int startingIndex,
            int pointsCount, UsageData usageData)
        {
            if(pointsCount!=3)
                return;

            var equationsVector = new Func<double[], double>[6];
            var derivativeMatrix = new Func<double[], double>[6, 6];
            for(int i=0;i<6;i++)
            for (int j = 0; j < 6; j++)
                derivativeMatrix[i, j] = ZeroFunc;

            //equationsVector[0] = x => x[0] - 300;
            equationsVector[0] = x => 0;
            equationsVector[1] = x => x[0] - x[1];
            //equationsVector[1] = x => 0;
            equationsVector[2] = x => x[0] - x[2];
            equationsVector[3] = GetQuadraticFunc(0, 2, 1, 3, 40000);
            equationsVector[4] = GetQuadraticFunc(2, 4, 3, 5, 40000);
            equationsVector[5] = GetQuadraticFunc(4, 0, 5, 1, 40000);

            //derivativeMatrix[0, 0] = OneFunc;
            derivativeMatrix[1, 0] = x => 1;
            derivativeMatrix[1, 1] = x => -1;
            derivativeMatrix[2, 0] = x => 1;
            derivativeMatrix[2, 2] = x => -1;

            derivativeMatrix[3, 0] = GetQuadraticDerivative(0, 2);
            derivativeMatrix[3, 1] = GetQuadraticDerivative(1, 3);
            derivativeMatrix[3, 2] = GetQuadraticDerivative(2, 0);
            derivativeMatrix[3, 3] = GetQuadraticDerivative(3, 1);
            derivativeMatrix[4, 2] = GetQuadraticDerivative(2, 4);
            derivativeMatrix[4, 3] = GetQuadraticDerivative(3, 5);
            derivativeMatrix[4, 4] = GetQuadraticDerivative(4, 2);
            derivativeMatrix[4, 5] = GetQuadraticDerivative(5, 3);
            derivativeMatrix[5, 4] = GetQuadraticDerivative(4, 0);
            derivativeMatrix[5, 5] = GetQuadraticDerivative(5, 1);
            derivativeMatrix[5, 0] = GetQuadraticDerivative(0, 4);
            derivativeMatrix[5, 1] = GetQuadraticDerivative(1, 5);

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

            if (results.Any(x => x < 0 || x > 2000))
                return;

            for (int i = 0; i < pointsCount; i++)
            {
                points[i] = new Point((int)results[2 * i], (int)results[2 * i + 1]);
            }
        }


        public void CalculatePointsPosition2(Point[] points, VH[] verticalHorizontals, int[] maxSizes, int startingIndex,
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
                    derivativeMatrix[2 * i, 2 * j] = ZeroFunc;
                    derivativeMatrix[2 * i, 2 * j + 1] = ZeroFunc;
                    derivativeMatrix[2 * i + 1, 2 * j] = ZeroFunc;
                    derivativeMatrix[2 * i + 1, 2 * j + 1] = ZeroFunc;
                }
                var startingX = (double) points[i].X;
                var startingY = (double) points[i].Y;
                int curI = i;
                equationsVector[2 * i] = x => x[2 * curI] - startingX;
                equationsVector[2 * i + 1] = x => x[2 * curI + 1] - startingY;
                derivativeMatrix[2 * i, 2 * i] = OneFunc;
                derivativeMatrix[2 * i + 1, 2 * i + 1] = OneFunc;
            }
            //equationsVector[2 * startingIndex] = GetForcingStartFunc(2 * startingIndex, points[startingIndex].X);
            //equationsVector[2 * startingIndex + 1] =
            //    GetForcingStartFunc(2 * startingIndex + 1, points[startingIndex].Y);

            //derivativeMatrix[2 * startingIndex, 2 * startingIndex] = TenFunc;
            //derivativeMatrix[2 * startingIndex + 1, 2 * startingIndex + 1] = TenFunc;

            for (int i = 0; i < pointsCount; i++)
            {
                var indexForDistEquation = 2 * i + 1;

                if (maxSizes[i] > 0)
                {
                    int nextIndex = 2 * ((i + 1) % pointsCount);
                    int curIndex = 2 * i;
                    int squareDist = maxSizes[i] * maxSizes[i];
                    equationsVector[indexForDistEquation] =
                        GetQuadraticFunc(curIndex, nextIndex, curIndex + 1, nextIndex + 1, squareDist);

                    derivativeMatrix[indexForDistEquation, curIndex] = GetQuadraticDerivative(curIndex, nextIndex);
                    derivativeMatrix[indexForDistEquation, nextIndex] = GetQuadraticDerivative(nextIndex, curIndex);

                    derivativeMatrix[indexForDistEquation, curIndex + 1] =
                        GetQuadraticDerivative(curIndex + 1, nextIndex + 1);
                    derivativeMatrix[indexForDistEquation, nextIndex + 1] =
                        GetQuadraticDerivative(nextIndex + 1, curIndex + 1);
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

            if (results.Any(x => x < 0 || x > 2000))
                return;
            
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
