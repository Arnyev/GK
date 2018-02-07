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
        private DirectBitmap _mainBitmap;
        private bool _moveKeyPressed;
        private readonly IPolygonData[] _polygons;
        private const int PolygonCount = 2;
        private readonly IFormDrawer _formDrawer;
        private int _selectedPolygonIndex;
        private int _selectedLineIndex;
        private int _selectedPointIndex = -1;
        private Point _currentMousePoint;
        private UsageData _usageData = new UsageData();
        private IPolygonData CurrentPolygon => _polygons[_selectedPolygonIndex];

        private readonly IPointColorCalculator _pointColorCalculator;

        public MainForm()
        {
            InitializeComponent();

            pictureBox.MouseDoubleClick += (s, e) => CurrentPolygon.AddPoint(new Point(e.X, e.Y));
            pictureBox.MouseClick += PictureBox1_MouseClick;

            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            Resize += Form1_Resize;
            KeyPreview = true;
            pictureBox.MouseMove += Form1_MouseMove;

            _currentMousePoint = new Point(-1, -1);

            _mainBitmap = new DirectBitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = _mainBitmap.Bitmap;

            _polygons = new IPolygonData[PolygonCount];
            _polygons[0] = new PolygonData(MinimumDistance);
            _polygons[1] = new PolygonData(MinimumDistance);
            _pointColorCalculator=new PointColorCalculator();

            _polygons[0].AddPoint(new Point(100, 600));
            _polygons[0].AddPoint(new Point(600, 200));
            _polygons[0].AddPoint(new Point(1500, 100));
            _polygons[0].AddPoint(new Point(1000,1000));
            _polygons[0].AddPoint(new Point(100,800));


            _polygons[1].AddPoint(new Point(0, 0));
            _polygons[1].AddPoint(new Point(1000, 300));
            _polygons[1].AddPoint(new Point(700, 400));

            _polygons[1].AddPoint(new Point(0, 300));


            _formDrawer = new FormDrawer(MinimumDistance, new PolygonFiller(_pointColorCalculator),
                new WeilerAthertonCalculator());

            SetLeftPanelEvents();
            SetTimerRedraw();
            InitColorCalculatorAndControlsState();
        }

        private void SetLeftPanelEvents()
        {
            lightColorButton.Click += LightColorButton_Click;
            objectColorButton.Click += ObjectColorButton_Click;

            objectTextureButton.Click += ObjectTextureButton_Click;
            vectorTextureButton.Click += VectorTextureButton_Click;
            bumpTextureButton.Click += BumpTextureButton_Click;

            objectColorRadioButton.CheckedChanged += (s, e) =>
                _pointColorCalculator.ChangeUseImage(!objectColorRadioButton.Checked);

            vectorConstRadioButton.CheckedChanged +=
                (s, e) => _pointColorCalculator.ChangeUseMap(!vectorConstRadioButton.Checked);

            bumpNoneRadioButton.CheckedChanged +=
                (s, e) => _pointColorCalculator.ChangeUseBump(!bumpNoneRadioButton.Checked);

            moveLightConstRadioButton.CheckedChanged += (s, e) =>
                _pointColorCalculator.ChangeLightMoving(!moveLightConstRadioButton.Checked);

            moveLightTextBox.TextChanged += OnRadiusTextChanged;

            bumbTrack.ValueChanged += (s, e) => _pointColorCalculator.ChangeBumpCoef((double)bumbTrack.Value / 1000);
            distributedTrack.ValueChanged += (s, e) => _pointColorCalculator.ChangeDistributedCoef((double)distributedTrack.Value / 100);
            mirrorTrack.ValueChanged += (s, e) => _pointColorCalculator.ChangeMirrorCoef((double)mirrorTrack.Value / 100);
            cosinusTrack.ValueChanged += (s, e) => _pointColorCalculator.ChangeCosExp(cosinusTrack.Value * 2);
        }

        private void OnRadiusTextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(moveLightTextBox.Text, out double radius))
                _pointColorCalculator.ChangeRadius(radius);
        }

        private void VectorTextureButton_Click(object sender, EventArgs e)
        {
            var bitmap = GetBitmapOpenFile();
            if (bitmap != null)
            {
                _pointColorCalculator.LoadNormalMap(bitmap);
                vectorTextureBox.Image = bitmap;
            }
        }

        private void BumpTextureButton_Click(object sender, EventArgs e)
        {
            var bitmap = GetBitmapOpenFile();
            if (bitmap != null)
            {
                _pointColorCalculator.LoadBumpMap(bitmap);
                bumpTextureBox.Image = bitmap;
            }
        }

        private void ObjectTextureButton_Click(object sender, EventArgs e)
        {
            var bitmap = GetBitmapOpenFile();
            if (bitmap != null)
            {
                _pointColorCalculator.LoadImage(bitmap);
                objectTextureBox.Image = bitmap;
            }
        }

        private void SetTimerRedraw()
        {
            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += (s, e) => Redraw();
            timer.Start();
        }
        private void InitColorCalculatorAndControlsState()
        {
            _pointColorCalculator.SetObjectColor(objectColorLabel.BackColor);
            _pointColorCalculator.SetLightColor(lightColorLabel.BackColor);

            _pointColorCalculator.ChangeUseImage(false);
            objectColorRadioButton.Checked = true;

            _pointColorCalculator.ChangeUseMap(false);
            vectorConstRadioButton.Checked = true;

            _pointColorCalculator.ChangeUseBump(false);
            bumpNoneRadioButton.Checked = true;

            _pointColorCalculator.ChangeLightMoving(false);
            moveLightConstRadioButton.Checked = true;

            var bumpmap = (Bitmap)Image.FromFile("bumpmap.jpg");
            _pointColorCalculator.LoadBumpMap(bumpmap);
            bumpTextureBox.Image = bumpmap;

            var normalmap = (Bitmap)Image.FromFile("normalmap.png");
            _pointColorCalculator.LoadNormalMap(normalmap);
            vectorTextureBox.Image = normalmap;

            bumbTrack.Value = 10;
            distributedTrack.Value = 10;
            mirrorTrack.Value = 10;
            cosinusTrack.Value = 10;
        }

        private void ObjectColorButton_Click(object sender, EventArgs e)
        {
            var color = GetColorFromWindow(objectColorLabel.BackColor);
            objectColorLabel.BackColor = color;
            _pointColorCalculator.SetObjectColor(color);
        }

        private void LightColorButton_Click(object sender, EventArgs e)
        {
            var color= GetColorFromWindow(lightColorLabel.BackColor);
            lightColorLabel.BackColor = color;
            _pointColorCalculator.SetLightColor(color);
        }

        private Color GetColorFromWindow(Color color)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = color;

            if (MyDialog.ShowDialog() == DialogResult.OK)
               return  MyDialog.Color;
   
            return color;
        }
        private Bitmap GetBitmapOpenFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                try
                {
                    return (Bitmap) Image.FromFile(openFileDialog.FileName, true);
                }
                catch
                {
                }
            return null;
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
            _mainBitmap = new DirectBitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = _mainBitmap.Bitmap;
            //Redraw();
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
                //Redraw();

            }

            else if (_selectedPointIndex != -1)
            {
                CurrentPolygon.MovePoint(_selectedPointIndex, new Point(e.X, e.Y), _usageData);
                //Redraw();
            }
        }

            private void RelationControlClick(object sender, EventArgs e)
        {
            var clickedRelation = sender == horizontalRelationControl ? VH.Horizontal : VH.Vertical;
            var control = (ToolStripMenuItem) sender;

            bool clicked = !control.Checked;
            CurrentPolygon.ChangeRelation(_selectedLineIndex, clicked ? clickedRelation : VH.None);

            //Redraw();
        }

        private void ConstLengthTextBoxTextChanged(object sender, EventArgs e)
        {
            int.TryParse(constLengthTextBox.Text, out var newMaxSize);
            if (newMaxSize < 0)
                newMaxSize = 0;

            CurrentPolygon.ChangeMaxSize(_selectedLineIndex, newMaxSize);

            rightClickMenu.Close();
            //Redraw();
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
                PolygonCount, _usageData);
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

            //Redraw();
        }
    }
}
