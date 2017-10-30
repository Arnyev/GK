using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace GK1
{
    public partial class MainForm : Form
    {
        public const int MinimumDistance = 8;
        private Bitmap _mainBitmap;
        private bool _moveKeyPressed;
        private readonly IPolygonData[] _polygons;
        private const int PolygonCount = 2;
        private readonly IFormDrawer _formDrawer;
        private int _selectedPolygonIndex;
        private int _selectedLineIndex;
        private int _selectedPointIndex = -1;
        private Point _currentMousePoint;

        private IPolygonData CurrentPolygon => _polygons[_selectedPolygonIndex];

        public MainForm()
        {
            InitializeComponent();

            pictureBox.MouseDoubleClick += (s, e) => CurrentPolygon.AddPoint(new Point(e.X, e.Y));
            pictureBox.MouseClick += PictureBox1_MouseClick;
            constLengthTextBox.KeyPress += ConstLengthTextBoxKeyPress;
            constLengthTextBox.LostFocus += ConstLengthTextBoxTextChanged;
            constantLengthRelationControl.Click += ConstantLengthRelationControl_CheckedChanged;
            verticalRelationControl.Click += RelationControlClick;
            horizontalRelationControl.Click += RelationControlClick;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            Resize += Form1_Resize;
            pictureBox.MouseMove += Form1_MouseMove;

            saveButton.Click += SaveButton_Click;
            loadButton.Click += LoadButton_Click;

            saveButton.KeyDown += Form1_KeyDown;
            loadButton.KeyDown += Form1_KeyDown;

            saveButton.KeyUp += Form1_KeyUp;
            loadButton.KeyUp += Form1_KeyUp;

            _currentMousePoint = new Point(-1, -1);
            _mainBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = _mainBitmap;
            _polygons = new IPolygonData[PolygonCount];
            _polygons[0] = new PolygonData(MinimumDistance);
            _polygons[1] = new PolygonData(MinimumDistance);
            _formDrawer = new FormDrawer(MinimumDistance);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() != DialogResult.OK)
                return;

            var serializer = new BinaryFormatter();
            string path = fileDialog.FileName;
            var file = File.Open(path, FileMode.Open);

            var polygonsDto = (PolygonDTO[]) serializer.Deserialize(file);
            for (int i = 0; i < PolygonCount; i++)
                _polygons[i].SetDataFromDto(polygonsDto[i]);

            file.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var fileDialog = new SaveFileDialog();
            if (fileDialog.ShowDialog() != DialogResult.OK)
                return;

            var serializer = new BinaryFormatter();

            var path = fileDialog.FileName;
            var file = File.Create(path);

            var polygonsDto = new PolygonDTO[PolygonCount];
            for (int i = 0; i < PolygonCount; i++)
                polygonsDto[i] = _polygons[i].GetPolygonDTO();

            serializer.Serialize(file, polygonsDto);
            file.Close();
        }

        private void ConstantLengthRelationControl_CheckedChanged(object sender, EventArgs e)
        {
            if (constantLengthRelationControl.Checked)
                CurrentPolygon.ChangeMaxSize(_selectedLineIndex, 0);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            _mainBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = _mainBitmap;
            Redraw();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            _moveKeyPressed = false;
            _currentMousePoint = new Point(-1, -1);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.M)
                _moveKeyPressed = true;
            else if (e.KeyCode == Keys.D1)
                _selectedPolygonIndex = _selectedPolygonIndex == 0 ? 1 : 0;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_moveKeyPressed)
            {
                if (_currentMousePoint.X != -1)
                    CurrentPolygon.MovePolygon(e.X - _currentMousePoint.X, e.Y - _currentMousePoint.Y);
                _currentMousePoint = new Point(e.X, e.Y);
            }

            else if (_selectedPointIndex != -1)
                CurrentPolygon.MovePoint(_selectedPointIndex, new Point(e.X, e.Y));

            Redraw();
        }

        private void RelationControlClick(object sender, EventArgs e)
        {
            var clickedRelation = sender == horizontalRelationControl ? VH.Horizontal : VH.Vertical;
            var control = (ToolStripMenuItem) sender;

            bool clicked = !control.Checked;
            CurrentPolygon.ChangeRelation(_selectedLineIndex, clicked ? clickedRelation : VH.None);

            Redraw();
        }

        private void ConstLengthTextBoxTextChanged(object sender, EventArgs e)
        {
            int.TryParse(constLengthTextBox.Text, out var newMaxSize);
            if (newMaxSize < 0)
                newMaxSize = 0;

            CurrentPolygon.ChangeMaxSize(_selectedLineIndex, newMaxSize);

            rightClickMenu.Close();
            Redraw();
        }

        private void ConstLengthTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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
            int closePointIndex = CurrentPolygon.CheckIfNextToExistingPoint(clickPoint);
            if (closePointIndex < 0)
                return false;

            CurrentPolygon.DeletePoint(closePointIndex);
            return true;
        }

        private void Redraw()
        {
            _formDrawer.Redraw(_polygons, _mainBitmap, CurrentPolygon.GetPoint(_selectedPointIndex),
                PolygonCount);
            pictureBox.Refresh();
        }

        private void CheckAndHandleNextToEdge(Point clickPoint)
        {
            var edgeIndex = CurrentPolygon.CheckIfNextToExistingEdge(clickPoint);
            if (edgeIndex < 0)
                return;

            ShowRightClickMenuForEdge(edgeIndex, clickPoint);
        }

        private void ShowRightClickMenuForEdge(int edgeIndex, Point clickPoint)
        {
            _selectedLineIndex = edgeIndex;
            CurrentPolygon.GetRelations(edgeIndex, out var verticalHorizontal, out int maxSize,
                out int currentDistance);
            verticalRelationControl.Checked = verticalHorizontal == VH.Vertical;
            horizontalRelationControl.Checked = verticalHorizontal == VH.Horizontal;

            if (maxSize > 0)
            {
                constLengthTextBox.Text = maxSize.ToString();
                constantLengthRelationControl.Checked = true;
            }
            else
            {
                constLengthTextBox.Text = currentDistance.ToString();
                constantLengthRelationControl.Checked = false;
            }
            rightClickMenu.Show(pictureBox, clickPoint);
        }

        private void HandleLeftClick(Point clickPoint)
        {
            if (_selectedPointIndex != -1)
                _selectedPointIndex = -1;
            else
                _selectedPointIndex = CurrentPolygon.CheckIfNextToExistingPoint(clickPoint);

            Redraw();
        }
    }
}
