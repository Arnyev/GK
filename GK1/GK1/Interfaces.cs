using System.Drawing;

namespace GK1
{
    internal interface IBasicCalculator
    {
        int ShortestDistanceFromSegment(Point startSeg, Point endSeg, Point p);
        int EuclideanDistance(Point p1, Point p2);
        void CheckLengths(int[] relations, int pointsCount);
    }

    internal interface IPositionCalculator
    {
        void CalculatePointsPosition(Point[] points, VH[] verticalHorizontals, int[] maxSizes, int startingIndex,
            int pointsCount);
    }

    internal interface IFormDrawer
    {
        void Redraw(int currentPointCount, int[] maxSizes, VH[] verticalsHorizontals, Point[] points,
            Bitmap bitmap, int selectedPointIndex);
    }

    internal interface IPointsProvider
    {//todo

    }

    internal interface IMatrixInverser
    {
        void InverseMatrix(double[][] matrix, out double[][] inversed);
    }
}
