using System;
using System.Drawing;
using System.Windows.Forms;

namespace GK1
{
    public partial class Form1 : Form
    {
        public const int Vertical = -1;
        public const int Horizontal = -2;
        public const int NoRelation = -3;
        public const int MaxPoints = 100;
        public const int RectangleWidth = 8;
        public const int MinimumDistance = 5;

        private readonly Font _drawFont = new Font("Arial", 16);
        private readonly SolidBrush _drawBrush = new SolidBrush(Color.Black);
        private Bitmap _mainBitmap;

        private int _selectedPointIndex;
        private int _rightClickedLineIndex;
        private int _currentPointsCount;

        private bool _moveKeyPressed;
        private Point _lastPosition;

        private readonly Point[] _points;
        private readonly int[] _relations;

        public Form1()
        {
            InitializeComponent();

            pictureBox1.MouseDoubleClick += AddPoint;
            pictureBox1.MouseClick += PictureBox1_MouseClick;
            constLengthTextBox.KeyPress += ConstLengthTextBoxKeyPress;
            constLengthTextBox.LostFocus += ConstLengthTextBoxTextChanged;
            verticalRelationControl.Click += VerticalRelationControlClick;
            horizontalRelationControl.Click += HorizontalRelationControlClick;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            Resize += Form1_Resize;
            pictureBox1.MouseMove += Form1_MouseMove;

            _points = new Point[MaxPoints];
            _relations = new int[MaxPoints];
            _selectedPointIndex = -1;
            _lastPosition = new Point(-1, -1);
            _mainBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = _mainBitmap;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            _mainBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = _mainBitmap;
            Redraw();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            _moveKeyPressed = false;
            _lastPosition = new Point(-1, -1);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.M)
                _moveKeyPressed = true;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_moveKeyPressed)
                MoveAllPoints(e.X, e.Y);

            else if (_selectedPointIndex != -1)
            {
                _points[_selectedPointIndex] = new Point(e.X, e.Y);
                Realign(_selectedPointIndex);
            }
        }

        private void MoveAllPoints(int cursorPositionX, int cursorPositionY)
        {
            //Świeżo po naciśnięciu, więc nie ma zapisanej pozycji i nie wiadomo ie ruszyć.
            if (_lastPosition.X != -1)
                return;

            int xDiff = cursorPositionX - _lastPosition.X;
            int yDiff = cursorPositionY - _lastPosition.Y;

            for (int i = 0; i < _currentPointsCount; i++)
                _points[i] = new Point(_points[i].X + xDiff, _points[i].Y + yDiff);

            _lastPosition = new Point(cursorPositionX, cursorPositionY);
            Redraw();
        }

        private void HorizontalRelationControlClick(object sender, EventArgs e)
        {
            bool horizontalClicked = !horizontalRelationControl.Checked;

            if (horizontalClicked && _relations[(_rightClickedLineIndex + 1) % _currentPointsCount] != Horizontal &&
                _relations[(_rightClickedLineIndex + _currentPointsCount - 1) % _currentPointsCount] != Horizontal)

                _relations[_rightClickedLineIndex] = Horizontal;
            else
                _relations[_rightClickedLineIndex] = NoRelation;

            Realign(_rightClickedLineIndex);
        }

        private void VerticalRelationControlClick(object sender, EventArgs e)
        {
            bool horizontalClicked = !verticalRelationControl.Checked;
            if (horizontalClicked && _relations[(_rightClickedLineIndex + 1) % _currentPointsCount] != Vertical &&
                _relations[(_rightClickedLineIndex + _currentPointsCount - 1) % _currentPointsCount] != Vertical)

                _relations[_rightClickedLineIndex] = Vertical;
            else
                _relations[_rightClickedLineIndex] = NoRelation;

            Realign(_rightClickedLineIndex);
        }

        private void ConstLengthTextBoxTextChanged(object sender, EventArgs e)
        {
            horizontalRelationControl.Checked = false;
            verticalRelationControl.Checked = false;

            if (!int.TryParse(constLengthTextBox.Text, out _relations[_rightClickedLineIndex]) ||
                _relations[_rightClickedLineIndex] <= 0)
            {
                _relations[_rightClickedLineIndex] = NoRelation;
                constantLengthRelationControl.Checked = false;
            }
            else
                constantLengthRelationControl.Checked = true;

            Realign(_rightClickedLineIndex);
        }

        private void ConstLengthTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void AddPoint(object sender, MouseEventArgs e)
        {
            _points[_currentPointsCount] = new Point(e.X, e.Y);
            _currentPointsCount++;
            Realign(_currentPointsCount - 1);
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                HandleRightClick(new Point(e.X, e.Y));
            else if (e.Button == MouseButtons.Left)
                HandleLeftClick(new Point(e.X, e.Y));
        }

        private void HandleRightClick(Point clickPoint)
        {
            if (!CheckAndHandleNextToPoint(clickPoint))
                CheckAndHandleNextToEdge(clickPoint);
        }

        private bool CheckAndHandleNextToPoint(Point clickPoint)
        {
            for (int i = 0; i < _currentPointsCount; ++i)
                if (Math.Abs(_points[i].X - clickPoint.X) <= RectangleWidth / 2 &&
                    Math.Abs(_points[i].Y - clickPoint.Y) <= RectangleWidth / 2)
                {
                    DeletePoint(i);
                    return true;
                }

            return false;
        }

        private void DeletePoint(int index)
        {
            for (int j = index + 1; j < _currentPointsCount; j++)
            {
                _points[j - 1] = _points[j];
                _relations[j - 1] = _relations[j];
            }

            _currentPointsCount--;
            Realign(0);
        }

        private void CheckAndHandleNextToEdge(Point clickPoint)
        {
            for (var i = 0; i < _currentPointsCount; i++)
            {
                int shortDist =
                    PositionCalculator.ShortestDistanceFromSegment(_points[i], _points[(i + 1) % _currentPointsCount], clickPoint);

                if (shortDist <= MinimumDistance)
                {
                    ShowRightClickMenuForEdge(i, clickPoint);
                    return;
                }
            }
        }

        private void ShowRightClickMenuForEdge(int edgeIndex, Point clickPoint)
        {
            _rightClickedLineIndex = edgeIndex;
            verticalRelationControl.Checked = _relations[edgeIndex] == Vertical;
            horizontalRelationControl.Checked = _relations[edgeIndex] == Horizontal;

            if (_relations[edgeIndex] > 0)
            {
                constLengthTextBox.Text = _relations[edgeIndex].ToString();
                constantLengthRelationControl.Checked = true;
            }
            else
            {
                constLengthTextBox.Text =
                    PositionCalculator.EuclideanDistance(_points[edgeIndex], _points[edgeIndex + 1]).ToString();
                constantLengthRelationControl.Checked = false;
            }

            rightClickMenu.Show(pictureBox1, clickPoint);
            Realign(edgeIndex);
        }

        //Jeśli jest zaznaczony punkt to go odznaczamy, umieszczmy w tablicy i odświeżamy wygląd, jeśli nie to szukamy punktu do zaznaczenia
        private void HandleLeftClick(Point clickPoint)
        {
            if (_selectedPointIndex != -1)
            {
                _points[_selectedPointIndex] = clickPoint;
                Realign(_selectedPointIndex);
                _selectedPointIndex = -1;
            }
            else
            {
                for (int i = 0; i < _currentPointsCount; i++)
                    if (Math.Abs(_points[i].X - clickPoint.X) <= RectangleWidth / 2 &&
                        Math.Abs(_points[i].Y - clickPoint.Y) <= RectangleWidth / 2)
                    {
                        _selectedPointIndex = i;
                        break;
                    }
            }
            Redraw();
        }

        private void Realign(int startingIndex)
        {
            PositionCalculator.CalculatePointsPosition(_points, _relations, startingIndex, _currentPointsCount);
            Redraw();
        }

        private void Redraw()
        {
            using (var g = Graphics.FromImage(_mainBitmap))
            {
                g.Clear(Color.White);
                for (var i = 0; i < _currentPointsCount; i++)
                {
                    g.DrawRectangle(Pens.Blue, _points[i].X - RectangleWidth / 2, _points[i].Y - RectangleWidth / 2,
                        RectangleWidth, RectangleWidth);

                    BresenhamDrawer.DrawLine(_points[i], _points[(i + 1) % _currentPointsCount], _mainBitmap);

                    if (_relations[i] > 0)
                        g.DrawString(_relations[i].ToString(), _drawFont, _drawBrush,
                            new Point((_points[i].X + _points[(i + 1) % _currentPointsCount].X) / 2,
                                (_points[i].Y + _points[(i + 1) % _currentPointsCount].Y) / 2));

                    else if (_relations[i] == Horizontal)
                        g.DrawString("Horizontal", _drawFont, _drawBrush,
                            new Point((_points[i].X + _points[(i + 1) % _currentPointsCount].X) / 2,
                                (_points[i].Y + _points[(i + 1) % _currentPointsCount].Y) / 2));

                    else if (_relations[i] == Vertical)
                        g.DrawString("Vertical", _drawFont, _drawBrush,
                            new Point((_points[i].X + _points[(i + 1) % _currentPointsCount].X) / 2,
                                (_points[i].Y + _points[(i + 1) % _currentPointsCount].Y) / 2));
                }
                if (_selectedPointIndex != -1)
                    g.FillRectangle(Brushes.Red, _points[_selectedPointIndex].X - RectangleWidth / 2,
                        _points[_selectedPointIndex].Y - RectangleWidth / 2, RectangleWidth, RectangleWidth);
            }
            pictureBox1.Refresh();
        }
    }
}
