using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GK1
{
    public partial class Form1 : Form
    {
        public const int MaxPoints = 100;
        public const int MinimumDistance = 5;

        private Bitmap _mainBitmap;

        private int _selectedPointIndex;
        private int _rightClickedLineIndex;

        private bool _moveKeyPressed;
        private Point _lastPosition;
        private int _selectedPolygon = 0;
        private const int RectangleWidth = 8;


        private readonly List<VH[]> _verticalHorizontals;
        private readonly List<int[]> _maxSizes;
        private readonly List<Point[]> _points;
        private readonly List<int> _currentPointCounts;

        private readonly IPositionCalculator _positionCalculator;
        private readonly IBasicCalculator _basicCalculator;
        private readonly IFormDrawer _formDrawer;

        private VH[] VerticalHorizontals => _verticalHorizontals[_selectedPolygon];
        private int[]MaxSizes => _maxSizes[_selectedPolygon];
        private Point[] Points => _points[_selectedPolygon];

        private int CurrentPointCount
        {
            get => _currentPointCounts[_selectedPolygon];
            set => _currentPointCounts[_selectedPolygon] = value;
        }

        public Form1()
        {
            InitializeComponent();

            pictureBox1.MouseDoubleClick += AddPoint;
            pictureBox1.MouseClick += PictureBox1_MouseClick;
            constLengthTextBox.KeyPress += ConstLengthTextBoxKeyPress;
            constLengthTextBox.LostFocus += ConstLengthTextBoxTextChanged;
            constantLengthRelationControl.Click += ConstantLengthRelationControl_CheckedChanged;
            verticalRelationControl.Click += RelationControlClick;
            horizontalRelationControl.Click += RelationControlClick;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            Resize += Form1_Resize;
            pictureBox1.MouseMove += Form1_MouseMove;

            _selectedPointIndex = -1;
            _lastPosition = new Point(-1, -1);
            _mainBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = _mainBitmap;

            _verticalHorizontals = new List<VH[]> {new VH[MaxPoints]};
            _maxSizes = new List<int[]> {new int[MaxPoints]};
            _points = new List<Point[]> {new Point[MaxPoints]};
            _currentPointCounts = new List<int> {0};

            _basicCalculator = new BasicCalculator();
            _positionCalculator = new PositionCalculator(_basicCalculator);
            _formDrawer = new FormDrawer(RectangleWidth);
        }

        private void ConstantLengthRelationControl_CheckedChanged(object sender, EventArgs e)
        {
            if (constantLengthRelationControl.Checked)
                MaxSizes[_rightClickedLineIndex] = 0;
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
                Points[_selectedPointIndex] = new Point(e.X, e.Y);
                Realign(_selectedPointIndex);
            }
        }

        private void MoveAllPoints(int cursorPositionX, int cursorPositionY)
        {
            //Świeżo po naciśnięciu, więc nie ma zapisanej pozycji i nie wiadomo gdzie ruszyć.
            if (_lastPosition.X != -1)
                return;

            int xDiff = cursorPositionX - _lastPosition.X;
            int yDiff = cursorPositionY - _lastPosition.Y;
            var points = Points;
            for (int i = 0; i < CurrentPointCount; i++)
                points[i] = new Point(points[i].X + xDiff, points[i].Y + yDiff);

            _lastPosition = new Point(cursorPositionX, cursorPositionY);
            Redraw();
         }


        private void RelationControlClick(object sender, EventArgs e)
        {
            var currentPointCount = CurrentPointCount;
            var clickedRelation = sender == horizontalRelationControl ? VH.Horizontal : VH.Vertical;
            var control = (ToolStripMenuItem) sender;

            bool clicked = !control.Checked;
            var verticalHorizontals = VerticalHorizontals;
            var nextRelation = verticalHorizontals[(_rightClickedLineIndex + 1) % currentPointCount];
            var prevRelation =
                verticalHorizontals[(_rightClickedLineIndex + currentPointCount - 1) % currentPointCount];

            if (clicked && nextRelation != clickedRelation && prevRelation != clickedRelation)
                verticalHorizontals[_rightClickedLineIndex] = clickedRelation;
            else
                verticalHorizontals[_rightClickedLineIndex] = VH.None;

            Realign(_rightClickedLineIndex);
        }

        private void ConstLengthTextBoxTextChanged(object sender, EventArgs e)
        {

            var maxSizes = MaxSizes;
            var lastLength = maxSizes[_rightClickedLineIndex];

            if (!int.TryParse(constLengthTextBox.Text, out maxSizes[_rightClickedLineIndex]) ||
                maxSizes[_rightClickedLineIndex] <= 0)
            {
                maxSizes[_rightClickedLineIndex] = 0;
            }

            try
            {
                Realign(_rightClickedLineIndex);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Za duża długość", "Error");
                maxSizes[_rightClickedLineIndex] = lastLength;
            }
            rightClickMenu.Close();
        }

        private void ConstLengthTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void AddPoint(object sender, MouseEventArgs e)
        {
            Points[CurrentPointCount] = new Point(e.X, e.Y);
            CurrentPointCount++;
            Realign(CurrentPointCount - 1);
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
            if (!CheckAndDeleteIfNextToPoint(clickPoint))
                CheckAndHandleNextToEdge(clickPoint);
        }

        private bool CheckAndDeleteIfNextToPoint(Point clickPoint)
        {
            var points = Points;
            for (int i = 0; i < CurrentPointCount; ++i)
                if (Math.Abs(points[i].X - clickPoint.X) <= RectangleWidth / 2 &&
                    Math.Abs(points[i].Y - clickPoint.Y) <= RectangleWidth / 2)
                {
                    DeletePoint(i);
                    return true;
                }

            return false;
        }

        private void Redraw()
        {
            _formDrawer.Redraw(CurrentPointCount, MaxSizes, VerticalHorizontals, Points, _mainBitmap,
                _selectedPointIndex);
            pictureBox1.Refresh();
        }

        private void DeletePoint(int index)
        {
            var points = Points;
            var maxSizes = MaxSizes;
            for (int j = index + 1; j < CurrentPointCount; j++)
            {
                points[j - 1] = points[j];
                maxSizes[j - 1] = maxSizes[j];
            }

            CurrentPointCount--;
            Realign(0);
        }

        private void CheckAndHandleNextToEdge(Point clickPoint)
        {
            var currentPointCount = CurrentPointCount;
            var points = Points;
            for (var i = 0; i < currentPointCount; i++)
            {
                int shortDist =
                    _basicCalculator.ShortestDistanceFromSegment(points[i], points[(i + 1) % currentPointCount],
                        clickPoint);

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
            verticalRelationControl.Checked = VerticalHorizontals[edgeIndex] == VH.Vertical;
            horizontalRelationControl.Checked = VerticalHorizontals[edgeIndex] == VH.Horizontal;

            if (MaxSizes[edgeIndex] > 0)
            {
                constLengthTextBox.Text = MaxSizes[edgeIndex].ToString();
                constantLengthRelationControl.Checked = true;
            }
            else
            {
                constLengthTextBox.Text = _basicCalculator
                    .EuclideanDistance(Points[edgeIndex], Points[(edgeIndex + 1) % CurrentPointCount]).ToString();

                constantLengthRelationControl.Checked = false;
            }

            rightClickMenu.Show(pictureBox1, clickPoint);
            Realign(edgeIndex);
        }

        //Jeśli jest zaznaczony punkt to go odznaczamy, umieszczmy w tablicy i odświeżamy wygląd, jeśli nie to szukamy punktu do zaznaczenia
        private void HandleLeftClick(Point clickPoint)
        {
            var points = Points;
            if (_selectedPointIndex != -1)
            {
                points[_selectedPointIndex] = clickPoint;
                Realign(_selectedPointIndex);
                _selectedPointIndex = -1;
            }
            else
            {
                for (int i = 0; i < CurrentPointCount; i++)
                    if (Math.Abs(points[i].X - clickPoint.X) <= RectangleWidth / 2 &&
                        Math.Abs(points[i].Y - clickPoint.Y) <= RectangleWidth / 2)
                    {
                        _selectedPointIndex = i;
                        break;
                    }
            }
            Redraw();
        }

        private void Realign(int startingIndex)
        {
            _positionCalculator.CalculatePointsPosition(Points, VerticalHorizontals, MaxSizes, startingIndex,
                CurrentPointCount);
            Redraw();
        }
    }
}
