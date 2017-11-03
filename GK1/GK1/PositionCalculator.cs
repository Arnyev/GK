using System;
using System.Drawing;

namespace GK1
{
    internal class PositionCalculator : IPositionCalculator
    {
        private readonly IBasicCalculator _basicCalculator;

        public PositionCalculator(IBasicCalculator basicCalculator)
        {
            _basicCalculator = basicCalculator;
        }

        public void CalculatePointsPosition(Point[] points, VH[] verticalHorizontals,
            int[] maxSizes, int startingIndex, int pointsCount, UsageData usageData)
        {
            _basicCalculator.CheckLengths(maxSizes, pointsCount);

            for (int i = startingIndex, j = 0; j < pointsCount - 1; j++, i = (i + 1) % pointsCount)
            {
                var nextIndex = (i + 1) % pointsCount;
                points[nextIndex] =
                    GetNewPointFromRelation(points[i], points[nextIndex], verticalHorizontals[i], maxSizes[i]);
            }

            for (int i = startingIndex, j = 0; j < pointsCount - 1; j++, i = (i + pointsCount - 1) % pointsCount)
            {
                var nextIndex = (i + pointsCount - 1) % pointsCount;
                points[nextIndex] = GetNewPointFromRelation(points[i], points[nextIndex],
                    verticalHorizontals[nextIndex], maxSizes[nextIndex]);
            }
        }

        private Point GetNewPointFromRelation(Point p1, Point p2, VH verticalHorizontal, int maxSize)
        {
            if (verticalHorizontal == VH.Vertical)
                return new Point(p1.X, p2.Y);
            if (verticalHorizontal == VH.Horizontal)
                return new Point(p2.X, p1.Y);
            if (maxSize > 0)
                return ChangeDistance(p1, p2, maxSize);
            return p2;
        }

        private Point ChangeDistance(Point fixedPoint, Point movingPoint, int newDistance)
        {
            var currentDistance = _basicCalculator.EuclideanDistance(fixedPoint, movingPoint);
            double ratio = (double) newDistance / currentDistance;

            return new Point((int) (fixedPoint.X + ratio * (movingPoint.X - fixedPoint.X)),
                (int) (fixedPoint.Y + ratio * (movingPoint.Y - fixedPoint.Y)));
        }
    }

    internal class BasicCalculator : IBasicCalculator
    {
        public int ShortestDistanceFromSegment(Point startSeg, Point endSeg, Point p)
        {
            double px = endSeg.X - startSeg.X;
            double py = endSeg.Y - startSeg.Y;

            double temp = (px * px) + (py * py);
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (temp == 0)
                return EuclideanDistance(startSeg, p);

            double u = ((p.X - startSeg.X) * px + (p.Y - startSeg.Y) * py) / (temp);

            if (u > 1)
                u = 1;
            else if (u < 0)
                u = 0;

            double x = startSeg.X + u * px;
            double y = startSeg.Y + u * py;

            double dx = x - p.X;
            double dy = y - p.Y;
            double dist = (int) Math.Sqrt(dx * dx + dy * dy);
            return (int) dist;
        }

        public int EuclideanDistance(Point p1, Point p2) =>
            (int) Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));

        public void CheckLengths(int[] relations, int pointsCount)
        {
            bool allLength = true;
            int lengthSum = 0;
            for (int i = 0; i < pointsCount; i++)
                if (relations[i] > 0)
                    lengthSum += relations[i];
                else
                {
                    allLength = false;
                    break;
                }

            if (allLength)
                for (int i = 0; i < pointsCount; i++)
                    if (relations[i] > Math.Abs(lengthSum - relations[i]))
                        throw new ArgumentException();
        }
    }

    public enum VH
    {
        None = 0,
        Vertical = 1,
        Horizontal = 2
    }
}
