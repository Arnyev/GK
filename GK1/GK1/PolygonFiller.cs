using System;
using System.Collections.Generic;
using System.Drawing;

namespace GK1
{
    public class PolygonFiller:IPolygonFiller
    {
        public static readonly Color FillColor = Color.FromArgb(0, 0, 100);
        private static readonly Pen Pen = new Pen(FillColor);

        private PointColorCalculator _pointColorCalculator;

        public PolygonFiller(PointColorCalculator pointColorCalculator)
        {
            _pointColorCalculator = pointColorCalculator;
            Bitmap image1 = (Bitmap)Image.FromFile(@"C:\Documents and Settings\Artur\Desktop\Untitled.png", true);
            _pointColorCalculator.RefreshVectorNoBump(image1);
        }

        public void FillPolygon(Graphics graphics,DirectBitmap bitmap, Point[] points, Color color)
        {
            if (points.Length < 3)
                return;

            GetIndexesArray(out int[] indexes, points);
            CreateEdgeArrays(out Edge[] edgesFrom, out Edge[] edgesTo, points);

            var AET = new List<Edge>();

            int yMin = points[indexes[0]].Y;
            int yMax = points[indexes[points.Length - 1]].Y;

            var lastActiveIndex = 0;

            for (int y = yMin + 1; y < yMax; y++)
            {
                for (int i = lastActiveIndex; i < points.Length; i++)
                {
                    int pointY = points[indexes[i]].Y;
                    if (pointY >= y)
                    {
                        lastActiveIndex = i;
                        break;
                    }
                    if (pointY == y - 1)
                        UpdateActiveLines(indexes[i], y, edgesTo, edgesFrom, AET);
                }

                FillScanLine(AET, graphics, bitmap, y, color);
            }
        }

        private static void GetIndexesArray(out int[] indexes, Point[] points)
        {
            indexes = new int[points.Length];
            for (int i = 0; i < points.Length; i++)
                indexes[i] = i;

            var pointsCopy = (Point[])points.Clone();

            Array.Sort(pointsCopy, indexes, new PointYComparer());
        }

        private static void UpdateActiveLines(int index, int y, Edge[] edgesTo,Edge[] edgesFrom, List<Edge> AET)
        {
            if (edgesTo[index].YMax > y - 1)
                AET.Add(edgesTo[index]);
            else
                AET.Remove(edgesTo[index]);

            if (edgesFrom[index].YMax > y - 1)
                AET.Add(edgesFrom[index]);
            else
                AET.Remove(edgesFrom[index]);
        }

        private static void CreateEdgeArrays(out Edge[] edgesFrom, out Edge[] edgesTo, Point[] points)
        {
            int n = points.Length;
            edgesFrom = new Edge[n];
            edgesTo = new Edge[n];

            for (int i = 0; i < n; i++)
            {
                int nextIndex = (i + 1) % n;
                if (points[i].Y < points[nextIndex].Y)
                {
                    double inverseM = (double) (points[nextIndex].X - points[i].X) /
                                      (points[nextIndex].Y - points[i].Y);
                    edgesTo[nextIndex] =
                        edgesFrom[i] = new Edge(points[i].Y, points[nextIndex].Y, inverseM, points[i].X);
                }
                else
                {
                    double inverseM = (double) (points[i].X - points[nextIndex].X) /
                                      (points[i].Y - points[nextIndex].Y);
                    edgesTo[nextIndex] =
                        edgesFrom[i] = new Edge(points[nextIndex].Y, points[i].Y, inverseM, points[nextIndex].X);
                }
            }
        }

        private  void FillScanLine(List<Edge> AET, Graphics graphics,DirectBitmap bitmap, int y, Color color)
        {
            var intersections = new int[AET.Count];
            for (int j = 0; j < AET.Count; j++)
            {
                AET[j].X += +AET[j].InverseM;
                intersections[j] = (int) AET[j].X;
            }
            Array.Sort(intersections);
            for (int i = 0; i < intersections.Length; i += 2)
            {
                var startPoint = intersections[i] > 0 ? intersections[i] : 0;
                var endPoint = intersections[(i + 1) % intersections.Length] < bitmap.Width
                    ? intersections[(i + 1) % intersections.Length]
                    : bitmap.Width - 1;

                for (int j = startPoint; j < endPoint; j++)
                {
                    var newColor = _pointColorCalculator.GetPixelColor(color, j, y);
                    bitmap.SetPixel(j, y, newColor);
                }
            }
        }
    }

    public class Edge
    {
        public readonly int YMin;
        public readonly int YMax;
        public readonly double InverseM;
        public double X;

        public Edge(int yMin, int yMax, double inverseM, double x)
        {
            YMin = yMin;
            YMax = yMax;
            InverseM = inverseM;
            X = x;
        }
    }

    public class PointXComparer : IComparer<Point>
    {
        public int Compare(Point p1, Point p2)
        {
            return p1.X.CompareTo(p2.X);
        }
    }

    public class PointYComparer : IComparer<Point>
    {
        public int Compare(Point p1, Point p2)
        {
            return p1.Y.CompareTo(p2.Y);
        }
    }
}
