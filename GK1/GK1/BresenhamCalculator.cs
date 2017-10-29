using System.Drawing;

namespace GK1
{
    internal static class BresenhamCalculator
    {
        private static readonly Color Color = Color.Black;

        public static void DrawLine(Point p1, Point p2, Bitmap bitmap)
        {
            var differencePoint = new Point(p2.X - p1.X, p2.Y - p1.Y);
            var octant = FindOctant(differencePoint);

            var mappedDifference = MapInput(octant, differencePoint.X, differencePoint.Y);

            var dx = mappedDifference.X;
            var dy = mappedDifference.Y;
            var d = 2 * dy - dx;
            var y = 0;

            for (int x = 0; x <= mappedDifference.X; x++)
            {
                var p = MapOutput(octant, x, y);
                var newPointX = p.X + p1.X;
                var newPointY = p.Y + p1.Y;

                if (newPointX >= 0 && newPointX < bitmap.Width && newPointY >= 0 && newPointY < bitmap.Height)
                    bitmap.SetPixel(newPointX, newPointY, Color);

                if (d > 0)
                {
                    y = y + 1;
                    d = d - 2 * dx;
                }
                d = d + 2 * dy;
            }
        }

        private static int FindOctant(Point end)
        {
            var dx = end.X;
            var dy = -end.Y;

            if (dx >= 0 && dy >= 0)
                return dy > dx ? 1 : 0;

            if (dx < 0 && dy >= 0)
                return dy > -dx ? 2 : 3;

            if (dx < 0 && dy < 0)
                return dy > dx ? 4 : 5;

            return -dy > dx ? 6 : 7;
        }

        private static Point MapInput(int octant, int x, int y)
        {
            switch (octant)
            {
                case 0: return new Point(x, -y);
                case 1: return new Point(-y, x);
                case 2: return new Point(-y, -x);
                case 3: return new Point(-x, -y);
                case 4: return new Point(-x, y);
                case 5: return new Point(y, -x);
                case 6: return new Point(y, x);
                case 7: return new Point(x, y);
            }
            return new Point();
        }

        private static Point MapOutput(int octant, int x, int y)
        {
            switch (octant)
            {
                case 0: return new Point(x, -y);
                case 1: return new Point(y, -x);
                case 2: return new Point(-y, -x);
                case 3: return new Point(-x, -y);
                case 4: return new Point(-x, y);
                case 5: return new Point(-y, x);
                case 6: return new Point(y, x);
                case 7: return new Point(x, y);
            }
            return new Point();
        }
    }
}
