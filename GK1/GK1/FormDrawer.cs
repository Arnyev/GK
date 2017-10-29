using System.Drawing;

namespace GK1
{
    internal class FormDrawer:IFormDrawer
    {
        private readonly Font _drawFont = new Font("Arial", 16);
        private readonly SolidBrush _drawBrush = new SolidBrush(Color.Black);
        private readonly int _rectangleWidth;

        public FormDrawer(int rectangleWidth)
        {
            _rectangleWidth = rectangleWidth;
        }

        const string Instructions =
            "Naciśnij lewy przycisk na wierzchołek aby go złapać wierzchołek, jeszcze raz lewy aby wypuścić. \n" +
            "Naciśnij lewy przycisk dwa razy aby stworzyć nowy wierzchołek. \n" +
            "Naciśnij prawy przycisk koło krawędzi aby wybrać relację. \n" +
            "Naciśnij prawy przycisk na wierzchołek aby go usunąć";

        public void Redraw(int currentPointCount, int[]maxSizes, VH[] verticalsHorizontals, Point[] points,
            Bitmap bitmap, int selectedPointIndex)
        {

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
                graphics.DrawString(Instructions, new Font("Arial", 10), _drawBrush, new Point(3, 3));

                for (var i = 0; i < currentPointCount; i++)
                {
                    graphics.DrawRectangle(Pens.Blue, points[i].X - _rectangleWidth / 2,
                        points[i].Y - _rectangleWidth / 2, _rectangleWidth, _rectangleWidth);
                    DrawLineAndLabel(graphics, points[i], points[(i + 1) % currentPointCount], maxSizes[i],
                        verticalsHorizontals[i], bitmap);
                }
                if (selectedPointIndex != -1)
                    graphics.FillRectangle(Brushes.Red, points[selectedPointIndex].X - _rectangleWidth / 2,
                        points[selectedPointIndex].Y - _rectangleWidth / 2, _rectangleWidth, _rectangleWidth);
            }
        }

        private void DrawLineAndLabel(Graphics graphics, Point p1, Point p2, int maxSize, VH verticalHorizontal,
            Bitmap bitmap)
        {
            BresenhamCalculator.DrawLine(p1, p2, bitmap);

            if (maxSize > 0)
                graphics.DrawString(maxSize.ToString(), _drawFont, _drawBrush,
                    new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2));

            else if (verticalHorizontal == VH.Horizontal)
                graphics.DrawString("Horizontal", _drawFont, _drawBrush,
                    new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2));

            else if (verticalHorizontal == VH.Vertical)
                graphics.DrawString("Vertical", _drawFont, _drawBrush, new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2));
        }
    }
}
