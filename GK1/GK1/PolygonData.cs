using System;
using System.Drawing;

namespace GK1
{
    [Serializable]
    public class PolygonData : IPolygonData
    {
        public const int MaxPoints = 100;
        private VH[] _verticalHorizontals;
        private int[] _maxSizes;
        private Point[] _points;
        private int _currentPointCount;
        private readonly IPositionCalculator _positionCalculator;
        private readonly IBasicCalculator _basicCalculator;
        private readonly int _minimumDistance;

        internal PolygonData(int minimumDistance)
        {
            _minimumDistance = minimumDistance;
            _currentPointCount = 0;
            _maxSizes = new int[MaxPoints];
            _points = new Point[MaxPoints];
            _verticalHorizontals = new VH[MaxPoints];
            _basicCalculator = new BasicCalculator();
            _positionCalculator =new PositionCalculator(_basicCalculator);
                //new NewtonRaphsonPositionCalculator(new NewtonRaphsonEquationSolver(new MatrixCalculationHelper()));
        }

        public void ChangeRelation(int index, VH newRelation)
        {
            if (newRelation == VH.None)
            {
                _verticalHorizontals[index] = newRelation;
                return;
            }
            var nextRelation = _verticalHorizontals[(index + 1) % _currentPointCount];
            var prevRelation = _verticalHorizontals[(index + _currentPointCount - 1) % _currentPointCount];

            if (nextRelation != newRelation && prevRelation != newRelation)
                _verticalHorizontals[index] = newRelation;
            else
                _verticalHorizontals[index] = VH.None;

            Realign(index);
        }

        public void MovePoint(int index, Point newPosition, UsageData errorCount)
        {
            _points[index] = newPosition;
            Realign(index, errorCount);
        }

        public void MovePolygon(int dx, int dy)
        {
            for (int i = 0; i < _currentPointCount; i++)
                _points[i] = new Point(_points[i].X + dx, _points[i].Y + dy);
        }

        public void Realign(int startingIndex)
        {
            UsageData usageData = new UsageData();
            _positionCalculator.CalculatePointsPosition(_points, _verticalHorizontals, _maxSizes, startingIndex,
                _currentPointCount, usageData);
        }

        public void Realign(int startingIndex, UsageData errorCount)
        {
            _positionCalculator.CalculatePointsPosition(_points, _verticalHorizontals, _maxSizes, startingIndex,
                _currentPointCount, errorCount);
        }

        public bool ChangeMaxSize(int index, int newMaxSize)
        {
            _maxSizes[index] = newMaxSize;
            try
            {
                Realign(index);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void AddPoint(Point newPoint)
        {
            _points[_currentPointCount] = newPoint;
            _currentPointCount++;
            Realign(_currentPointCount - 1);
        }

        public int CheckIfNextToExistingPoint(Point closePoint)
        {
            for (int i = 0; i < _currentPointCount; ++i)
                if (Math.Abs(_points[i].X - closePoint.X) <= _minimumDistance / 2 &&
                    Math.Abs(_points[i].Y - closePoint.Y) <= _minimumDistance / 2)
                    return i;

            return -1;
        }

        public void DeletePoint(int index)
        {
            for (int j = index + 1; j < _currentPointCount; j++)
            {
                _points[j - 1] = _points[j];
                _maxSizes[j - 1] = _maxSizes[j];
                _verticalHorizontals[j - 1] = _verticalHorizontals[j];
            }

            _currentPointCount--;
            Realign(0);
        }

        public int CheckIfNextToExistingEdge(Point closePoint)
        {
            for (var i = 0; i < _currentPointCount; i++)
            {
                int shortDist = _basicCalculator.ShortestDistanceFromSegment(_points[i],
                    _points[(i + 1) % _currentPointCount], closePoint);

                if (shortDist <= _minimumDistance)
                    return i;
            }
            return -1;
        }

        public void GetRelations(int index, out VH verticalHorizontal, out int maxSize, out int currentDistance)
        {
            currentDistance =
                _basicCalculator.EuclideanDistance(_points[index], _points[(index + 1) % _currentPointCount]);
            verticalHorizontal = _verticalHorizontals[index];
            maxSize = _maxSizes[index];
        }

        public void GetData(out Point[] points, out VH[] verticalHorizontal, out int[] maxSizes,
            out int currentPointCount)
        {
            points = _points;
            verticalHorizontal = _verticalHorizontals;
            maxSizes = _maxSizes;
            currentPointCount = _currentPointCount;
        }

        public Point GetPoint(int index)
        {
            return index < 0 ? new Point(-1, -1) : _points[index];
        }

        public PolygonDTO GetPolygonDTO()
        {
            var polygonDto = new PolygonDTO
            {
                Points = _points,
                VerticalHorizontals = _verticalHorizontals,
                MaxSizes = _maxSizes,
                CurrentPointCount = _currentPointCount
            };
            return polygonDto;
        }

        public void SetDataFromDto(PolygonDTO polygonDto)
        {
            _points = polygonDto.Points;
            _verticalHorizontals = polygonDto.VerticalHorizontals;
            _maxSizes = polygonDto.MaxSizes;
            _currentPointCount = polygonDto.CurrentPointCount;
        }
    }

    [Serializable]
    public class PolygonDTO
    {
        public VH[] VerticalHorizontals;
        public int[] MaxSizes;
        public Point[] Points;
        public int CurrentPointCount;
    }
}
