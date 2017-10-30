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
        void Redraw(IPolygonData[] polygonData, Bitmap bitmap, Point selectedPoint, int polygonCount);
    }

    public interface IPolygonData
    {
        void MovePolygon(int dx, int dy);
        void MovePoint(int index, Point newPosition);
        void ChangeRelation(int index, VH newRelation);
        void Realign(int startingIndex);
        bool ChangeMaxSize(int index, int newMaxSize);
        void AddPoint(Point newPoint);
        int CheckIfNextToExistingPoint(Point closePoint);
        int CheckIfNextToExistingEdge(Point closePoint);
        void GetRelations(int index, out VH verticalHorizontal, out int maxSize, out int currentDistance);
        void DeletePoint(int index);
        void GetData(out Point[] points, out VH[] verticalHorizontal, out int[] maxSizes, out int currentPointCount);
        Point GetPoint(int index);
        PolygonDTO GetPolygonDTO();
        void SetDataFromDto(PolygonDTO polygonDto);
    }

    internal interface IMatrixInverser
    {
        void InverseMatrix(double[][] matrix, out double[][] inversed);
    }
}
