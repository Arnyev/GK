using System.Drawing;
using System.Linq;

namespace GK1
{
    internal interface IFormDrawer
    {
        void Redraw(IPolygonData[] polygonData, DirectBitmap bitmap, Point selectedPoint, int polygonCount, UsageData usageData);
    }

    internal class FormDrawer : IFormDrawer
    {
        private readonly Font _drawFont = new Font("Arial", 16);
        private readonly Font _textFont = new Font("Arial", 10);
        private readonly SolidBrush _drawBrush = new SolidBrush(Color.Black);
        private readonly int _rectangleWidth;
        private readonly IPolygonFiller _polygonFiller;
        private readonly IWeilerAthertonCalculator _weilerAthertonCalculator;

        public FormDrawer(int rectangleWidth, IPolygonFiller polygonFiller, IWeilerAthertonCalculator weilerAthertonCalculator)
        {
            _rectangleWidth = rectangleWidth;
            _polygonFiller = polygonFiller;
            _weilerAthertonCalculator = weilerAthertonCalculator;
        }

        private const string Instructions =
            "Naciśnij lewy przycisk na wierzchołek aby go złapać wierzchołek, jeszcze raz lewy aby wypuścić. \n" +
            "Naciśnij lewy przycisk dwa razy aby stworzyć nowy wierzchołek. \n" +
            "Naciśnij 1 aby przełączyć wielokąt\n" +
            "Przytrzymaj m aby ruszyć aktywny wielokąt";


        public void Redraw(IPolygonData[] polygonData, DirectBitmap bitmap, Point selectedPoint, int polygonCount,
            UsageData usageData)
        {
            using (var graphics = Graphics.FromImage(bitmap.Bitmap))
            {
                graphics.Clear(Color.White);

                graphics.DrawString(Instructions, _textFont, _drawBrush, new Point(3, 3));

                var polygonA = polygonData[0].GetPoints();
                var polygonB = polygonData[1].GetPoints();

                foreach (Point p in polygonA.Concat(polygonB))
                    graphics.DrawRectangle(Pens.Blue, p.X - _rectangleWidth / 2, p.Y - _rectangleWidth / 2,
                        _rectangleWidth, _rectangleWidth);

                var sum = _weilerAthertonCalculator.PolygonSum(polygonA, polygonB);

                if (sum.Length == 0) //Rozłączne wielokąty
                {
                    _polygonFiller.FillPolygon(bitmap, polygonA);
                    _polygonFiller.FillPolygon(bitmap, polygonB);
                }
                else
                    _polygonFiller.FillPolygon(bitmap, sum);

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
