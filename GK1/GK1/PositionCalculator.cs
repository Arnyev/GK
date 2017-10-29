using System;
using System.Drawing;

namespace GK1
{
    internal static class PositionCalculator
    {
        public const int Vertical = -1;
        public const int Horizontal = -2;

        public static void CalculatePointsPosition(Point[] points, int[] relations, int startingIndex, int pointsCount)
        {
            for (int i = startingIndex, j = 0; j < pointsCount - 1; j++, i = (i + 1) % pointsCount)
            {
                var nextIndex = (i + 1) % pointsCount;

                if (relations[i] == Vertical)
                    points[nextIndex] = new Point(points[i].X, points[nextIndex].Y);
                else if (relations[i] == Horizontal)
                    points[nextIndex] = new Point(points[nextIndex].X, points[i].Y);
                else if (relations[i] > 0)
                    points[nextIndex] = ChangeDistance(points[i], points[nextIndex], relations[i]);
                else
                    break;
            }

            for (int i = startingIndex, j = 0; j < pointsCount - 1; j++, i = (i + pointsCount - 1) % pointsCount)
            {
                var nextIndex = (i + pointsCount - 1) % pointsCount;

                if (relations[nextIndex] == Vertical)
                    points[nextIndex] = new Point(points[i].X, points[nextIndex].Y);
                else if (relations[nextIndex] == Horizontal)
                    points[nextIndex] = new Point(points[nextIndex].X, points[i].Y);
                else if (relations[nextIndex] > 0)
                    points[nextIndex] = ChangeDistance(points[i], points[nextIndex], relations[nextIndex]);
                else
                    break;
            }
        }

        private static Point ChangeDistance(Point fixedPoint, Point movingPoint, int newDistance)
        {
            var currentDistance = EuclideanDistance(fixedPoint, movingPoint);
            double ratio = (double) newDistance / currentDistance;

            return new Point((int) (fixedPoint.X + ratio * (movingPoint.X - fixedPoint.X)),
                (int) (fixedPoint.Y + ratio * (movingPoint.Y - fixedPoint.Y)));
        }

        public static int ShortestDistanceFromSegment(Point startSeg, Point endSeg, Point p)
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

        public static int EuclideanDistance(Point p1, Point p2) =>
            (int) Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
    }
}
