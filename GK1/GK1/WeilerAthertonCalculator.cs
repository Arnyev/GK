using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GK1
{
    public interface IWeilerAthertonCalculator
    {
        Point[] PolygonSum(Point[] p1, Point[] p2);
    }

    public class WeilerAthertonCalculator : IWeilerAthertonCalculator
    {
        public Point[] PolygonSum(Point[] p1, Point[] p2)
        {
            if (p1.Length < 3 || p2.Length < 3)
                return new Point[0];

            if (PolygonArea(p1) > 0)//wieksze bo bitmapa odwraca
                p1 = p1.Reverse().ToArray();
            if (PolygonArea(p2) > 0)
                p2 = p2.Reverse().ToArray();

            var startingNodeA = GetPolygonList(p1, true);
            var startingNodeB = GetPolygonList(p2, false);

            FillListsWithIntersections(p1, p2, startingNodeA, startingNodeB, out int added);

            if (added == 0)
                return new Point[0];

            return GetPolygonFromList(startingNodeA);
        }

        private static void FillListsWithIntersections(Point[] p1, Point[] p2, PointNode nodeA, PointNode nodeB, out int added)
        {
            added = 0;
            List<PointNode>[] listsB = new List<PointNode>[p2.Length]; //listy skrzyzowan dla kazdej krawedzi z a
            for (int i = 0; i < p2.Length; i++)
                listsB[i] = new List<PointNode>();

            List<PointNode>[] listsA = new List<PointNode>[p1.Length]; //listy skrzyzowan dla kazdej krawedzi z b
            for (int i = 0; i < p1.Length; i++)
                listsA[i] = new List<PointNode>();

            for (int i = 0; i < p1.Length; i++)
            {
                var curA = p1[i];
                var nextA = p1[(i + 1) % p1.Length];

                for (int j = 0; j < p2.Length; j++)
                {
                    var curB = p2[j];
                    var nextB = p2[(j + 1) % p2.Length];
                    if (GetIntersection(curA, nextA, curB, nextB, out Point intersection))
                    {
                        var enteringA = !IsLeft(curA, nextA, nextB);
                        var enteringB = !IsLeft(curB, nextB, nextA);
                        var newNode = new PointNode(intersection, enteringA, enteringB);
                        added++;
                        listsB[j].Add(newNode);
                        listsA[i].Add(newNode);
                    }
                }
            }
            FillPolygonWithIntersections(nodeA, listsA, p1, true);
            FillPolygonWithIntersections(nodeB, listsB, p2, false);
        }

        private static void FillPolygonWithIntersections(PointNode node, List<PointNode>[] lists, Point[] polygon, bool isA)
        {
            for (int i = 0; i < polygon.Length; i++)
            {
                if (lists[i].Count > 0)
                {
                    SortListClockwise(polygon[i], polygon[(i + 1) % polygon.Length], ref lists[i]);

                    for (int j = 0; j < lists[i].Count; j++)
                    {
                        if (isA)
                        {
                            lists[i][j].NextInA = node.NextInA;
                            node.NextInA = lists[i][j];
                            node = node.NextInA;
                        }
                        else
                        {
                            lists[i][j].NextInB = node.NextInB;
                            node.NextInB = lists[i][j];
                            node = node.NextInB;
                        }
                    }
                }
                node = isA ? node.NextInA : node.NextInB;
            }
        }

        private static void SortListClockwise(Point startSegment, Point endSegment, ref List<PointNode> list)
        {
            bool xDescending =startSegment.X > endSegment.X;
            bool yDescending =startSegment.Y > endSegment.Y;
            if (xDescending && yDescending)
                list = list.OrderByDescending(p => p.Point.X).ThenByDescending(p => p.Point.Y).ToList();
            else if (xDescending)
                list = list.OrderByDescending(p => p.Point.X).ThenBy(p => p.Point.Y).ToList();
            else if (yDescending)
                list = list.OrderBy(p => p.Point.X).ThenByDescending(p => p.Point.Y).ToList();
            else
                list = list.OrderBy(p => p.Point.X).ThenBy(p => p.Point.Y).ToList();
        }

        private static Point[] GetPolygonFromList(PointNode startingNodeA)
        {
            var newPolygon = new List<Point>();
            var curNode = startingNodeA;

            while (!curNode.EnteringB)
            {
                curNode = curNode.NextInA;
            }
            var startingNode = curNode;
            bool isInA = true;
            do
            {
                newPolygon.Add(curNode.Point);
                if (curNode.Intersection && !isInA)
                {
                    curNode = curNode.NextInA;
                    isInA = true;
                }
                else if (curNode.Intersection)
                {
                    curNode = curNode.NextInB;
                    isInA = false;
                }
                else if (isInA)
                    curNode = curNode.NextInA;
                else
                    curNode = curNode.NextInB;
            } while (curNode != startingNode);

            return newPolygon.ToArray();
        }

        private static PointNode GetPolygonList(Point[] polygon, bool isA)
        {
            var node = new PointNode(polygon[0]);
            var startingNode = node;
            for (int i = 1; i < polygon.Length; i++)
            {
                var next = new PointNode(polygon[i]);
                if (isA)
                    node.NextInA = next;
                else
                    node.NextInB = next;
                node = next;
            }
            if (isA)
                node.NextInA = startingNode;
            else
                node.NextInB = startingNode;
            return startingNode;
        }

        private static bool IsLeft(Point s1, Point s2, Point point)
        {
            return ((s2.X - s1.X) * (point.Y - s1.Y) - (s2.Y - s1.Y) * (point.X - s1.X)) > 0;
        }

        private static bool GetIntersection(Point a1, Point a2, Point b1, Point b2, out Point intersection)
        {
            double aDx = a2.X - a1.X;
            double aDy = a2.Y - a1.Y;
            double bDx = b2.X - b1.X;
            double bDy = b2.Y - b1.Y;

            double s = (-aDy * (a1.X - b1.X) + aDx * (a1.Y - b1.Y)) / (-bDx * aDy + aDx * bDy);
            double t = (bDx * (a1.Y - b1.Y) - bDy * (a1.X - b1.X)) / (-bDx * aDy + aDx * bDy);

            if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
                intersection = new Point((int) (a1.X + (t * aDx)), (int) (a1.Y + (t * aDy)));
                return true;
            }
            intersection = new Point();
            return false;
        }

        private static double PolygonArea(Point[] polygon)
        {
            int n = polygon.Length;
            if (n < 3) return 0;
            double result = polygon[0].X * polygon[1].Y - polygon[0].X * polygon[n - 1].Y +
                            polygon[n - 1].X * polygon[0].Y - polygon[n - 1].X * polygon[n - 2].Y;
            for (int i = 1; i < n - 1; i++)
                result += polygon[i].X * polygon[i + 1].Y - polygon[i].X * polygon[i - 1].Y;

            return result;
        }

        private class PointNode
        {
            public readonly Point Point;
            public PointNode NextInA;
            public PointNode NextInB;

            public readonly bool EnteringA;
            public readonly bool EnteringB;

            public bool Intersection => EnteringB || EnteringA;

            public PointNode(Point point, bool enteringA = false, bool enteringB = false)
            {
                Point = point;
                EnteringA = enteringA;
                EnteringB = enteringB;
            }
        }
    }
}
