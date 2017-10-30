using System;
using System.Drawing;

namespace GK1
{
    [Serializable]
    public class PolygonData : IPolygonData
    {
        public const int MaxPoints = 100;
        private Point _lastMovePosition;
        private VH[] _verticalHorizontals;
        private int[] _maxSizes;
        private  Point[] _points;
        private int _currentPointCount;
        private readonly IPositionCalculator _positionCalculator;
        private readonly IBasicCalculator _basicCalculator;
        private readonly int RectangleWidth;
        private readonly int MinimumDistance;

        internal PolygonData()
        {
            RectangleWidth = 8;
            MinimumDistance = 5;
            _lastMovePosition = new Point(-1, -1);
            _currentPointCount = 0;
            _maxSizes = new int[MaxPoints];
            _points = new Point[MaxPoints];
            _verticalHorizontals = new VH[MaxPoints];
            _basicCalculator = new BasicCalculator();
            _positionCalculator = new PositionCalculator(_basicCalculator);
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

        public void MovePoint(int index, Point newPosition)
        {
            _points[index] = newPosition;
            Realign(index);
        }

        public void MovePolygon(Point newPosition)
        {
            if (_lastMovePosition.X == -1)
            {
                _lastMovePosition = newPosition;
                return;
            }

            int xDiff = newPosition.X - _lastMovePosition.X;
            int yDiff = newPosition.Y - _lastMovePosition.Y;
            for (int i = 0; i < _currentPointCount; i++)
                _points[i] = new Point(_points[i].X + xDiff, _points[i].Y + yDiff);
        }

        public void ResetMovePosition()
        {
            _lastMovePosition = new Point(-1, -1);
        }

        public void Realign(int startingIndex)
        {
            _positionCalculator.CalculatePointsPosition(_points, _verticalHorizontals, _maxSizes, startingIndex, _currentPointCount);
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
                if (Math.Abs(_points[i].X - closePoint.X) <= RectangleWidth / 2 &&
                    Math.Abs(_points[i].Y - closePoint.Y) <= RectangleWidth / 2)
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
                int shortDist = _basicCalculator.ShortestDistanceFromSegment(_points[i], _points[(i + 1) % _currentPointCount], closePoint);

                if (shortDist <= MinimumDistance)
                    return i;
            }
            return -1;
        }

        public void GetRelations(int index, out VH verticalHorizontal, out int maxSize, out int currentDistance)
        {
            currentDistance = _basicCalculator.EuclideanDistance(_points[index], _points[(index + 1) % _currentPointCount]);
            verticalHorizontal = _verticalHorizontals[index];
            maxSize = _maxSizes[index];
        }

        public void GetData(out Point[] points, out VH[] verticalHorizontal, out int[] maxSizes,out int currentPointCount)
        {
            points = _points;
            verticalHorizontal = _verticalHorizontals;
            maxSizes = _maxSizes;
            currentPointCount = _currentPointCount;
        }

        public Point GetPoint(int index)
        {
            if (index < 0)
                return new Point(-1, -1);
            return _points[index];
        }

        public PolygonDTO GetPolygonDTO()
        {
            PolygonDTO polygonDto = new PolygonDTO();
            polygonDto.Points = _points;
            polygonDto.VerticalHorizontals = _verticalHorizontals;
            polygonDto.MaxSizes = _maxSizes;
            polygonDto.CurrentPointCount = _currentPointCount;
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
