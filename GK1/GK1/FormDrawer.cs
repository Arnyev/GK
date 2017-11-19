using System.Drawing;
using System.Linq;

namespace GK1
{
    internal class FormDrawer : IFormDrawer
    {
        private readonly Font _drawFont = new Font("Arial", 16);
        private readonly SolidBrush _drawBrush = new SolidBrush(Color.Black);
        private readonly int _rectangleWidth;
        private readonly IPolygonFiller _polygonFiller;
        private IWeilerAthertonCalculator _weilerAthertonCalculator;

        public FormDrawer(int rectangleWidth, IPolygonFiller polygonFiller, IWeilerAthertonCalculator weilerAthertonCalculator)
        {
            _rectangleWidth = rectangleWidth;
            _polygonFiller = polygonFiller;
            _weilerAthertonCalculator = weilerAthertonCalculator;
        }

        private const string Instructions =
            "Naciśnij lewy przycisk na wierzchołek aby go złapać wierzchołek, jeszcze raz lewy aby wypuścić. \n" +
            "Naciśnij lewy przycisk dwa razy aby stworzyć nowy wierzchołek. \n" +
            "Naciśnij prawy przycisk koło krawędzi aby wybrać relację. \n" +
            "Naciśnij prawy przycisk na wierzchołek aby go usunąć\n" +
            "Naciśnij 1 aby przełączyć wielokąt\n" +
            "Przytrzymaj m aby ruszyć aktywny wielokąt";


        public void Redraw(IPolygonData[] polygonData, DirectBitmap bitmap, Point selectedPoint, int polygonCount, UsageData usageData)
        {


            using (var graphics = Graphics.FromImage(bitmap.Bitmap))
            {
                graphics.Clear(Color.White);


                graphics.DrawString(Instructions, new Font("Arial", 10), _drawBrush, new Point(3, 3));
                graphics.DrawString("Ilość obliczeń: " + usageData.CalculationCount, new Font("Arial", 10), _drawBrush,
                    new Point(bitmap.Width - 130, bitmap.Height - 60));
                graphics.DrawString("Ilość iteracji: " + usageData.IterationCount, new Font("Arial", 10), _drawBrush,
                    new Point(bitmap.Width - 130, bitmap.Height - 40));
                graphics.DrawString("Ilość błędów: " + usageData.ErrorCount, new Font("Arial", 10), _drawBrush,
                    new Point(bitmap.Width - 130, bitmap.Height - 20));

                for (int j = 0; j < polygonCount; j++)
                {
                    polygonData[j].GetData(out Point[] points, out VH[] verticalsHorizontals, out int[] maxSizes,
                        out int pointsCount);

                    var points2 = points.Take(pointsCount).ToArray();
                    _polygonFiller.FillPolygon(graphics,bitmap, points2, Color.FromArgb(243,105,24));

                    for (var i = 0; i < pointsCount; i++)
                    {

                        graphics.DrawRectangle(Pens.Blue, points[i].X - _rectangleWidth / 2,
                            points[i].Y - _rectangleWidth / 2, _rectangleWidth, _rectangleWidth);
                        //DrawLineAndLabel(graphics, points[i], points[(i + 1) % pointsCount], maxSizes[i],
                        //    verticalsHorizontals[i], bitmap);
                    }
                }
                if (polygonData.Length > 1)
                {
                    polygonData[0].GetData(out Point[] points1, out VH[] verticalsHorizontals1, out int[] maxSizes1,
                        out int pointsCount1);
                    polygonData[1].GetData(out Point[] points2, out VH[] verticalsHorizontals2, out int[] maxSizes2,
                        out int pointsCount2);
                    var s = _weilerAthertonCalculator.PolygonSum(points1.Take(pointsCount1).ToArray(),
                        points2.Take(pointsCount2).ToArray());

                    _polygonFiller.FillPolygon(graphics,bitmap, s, Color.BlueViolet);
                }


                if (selectedPoint.X != -1)
                    graphics.FillRectangle(Brushes.Red, selectedPoint.X - _rectangleWidth / 2,
                        selectedPoint.Y - _rectangleWidth / 2, _rectangleWidth, _rectangleWidth);
            }
        }

        private void DrawLineAndLabel(Graphics graphics, Point p1, Point p2, int maxSize, VH verticalHorizontal,
            Bitmap bitmap)
        {
            BresenhamDrawer.DrawLine(p1, p2, bitmap);

            if (maxSize > 0)
                graphics.DrawString(maxSize.ToString(), _drawFont, _drawBrush,
                    new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2));

            if (verticalHorizontal == VH.Horizontal)
                graphics.DrawString("Horizontal", _drawFont, _drawBrush,
                    new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2 - 30));

            if (verticalHorizontal == VH.Vertical)
                graphics.DrawString("Vertical", _drawFont, _drawBrush,
                    new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2 - 30));
        }
    }
}
