using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace GK1
{
    public interface IPointColorCalculator
    {
        MyColor GetPixelColor(int x, int y);

        void LoadImage(Bitmap bitmap);
        void LoadBumpMap(Bitmap bitmap);
        void LoadNormalMap(Bitmap bitmap);

        void SetObjectColor(Color color);
        void SetLightColor(Color color);

        void ChangeLightMoving(bool shouldMove);
        void ChangeUseImage(bool useImage);
        void ChangeUseMap(bool useMap);
        void ChangeUseBump(bool useBump);
    }

    public class PointColorCalculator: IPointColorCalculator
    {
        private static readonly Point3D ConstLightVector = new Point3D(0, 0, 1);
        private Point3D _movingLightVector= new Point3D(0, 0, 1);

        private const int TurnTime = 3;
        private const double BaseHeight = 0.5;
        private const double TwoPi = 2 * Math.PI;

        private Point3D[,] _vectorMap;

        private DirectBitmap _imageBitmap;
        private DirectBitmap _vectorMapBitmap;
        private DirectBitmap _bumpBitmap;

        private bool _useImage;
        private bool _useMap;
        private bool _useBump;
        private bool _lightMoves;

        private MyColor _objectColor = new MyColor(255, 255, 255);
        private Point3D _lightColor = new Point3D(0.21, 0.56, 0.24);

        private Timer _timer;

        public PointColorCalculator()
        {
            void RefreshLightVector(object s) => SetLightVector();
            _timer = new Timer(RefreshLightVector, "test",0, 10);
        }

        public MyColor GetPixelColor(int x, int y)
        {
            var objectColor = !_useImage ? _objectColor : _imageBitmap.GetPixel(x, y);

            Point3D vector;
            if (!_useMap || x >= _vectorMap.GetLength(0) || y >= _vectorMap.GetLength(1))
                vector = new Point3D(0, 0, 1);
            else
                vector = _vectorMap[x, y];

            var lightVector = _lightMoves ? _movingLightVector : ConstLightVector;

            double cos = Cosinus(vector, lightVector);
            var r = (byte) (objectColor.R * _lightColor.X * cos);
            var g = (byte) (objectColor.G * _lightColor.Y * cos);
            var b = (byte) (objectColor.B * _lightColor.Z * cos);

            return new MyColor(r, g, b);
        }

        public void LoadImage(Bitmap bitmap)
        {
            _imageBitmap = new DirectBitmap(bitmap);
        }

        public void LoadBumpMap(Bitmap bitmap)
        {
            _bumpBitmap = new DirectBitmap(bitmap);
        }

        public void LoadNormalMap(Bitmap bitmap)
        {
            _vectorMapBitmap = new DirectBitmap(bitmap);
            RefreshVectorMap();
        }

        private void RefreshVectorMap()
        {
            if(!_useMap)
                return;
            
            if(_useBump)
                RefreshVectorWithBumb();
            else
                RefreshVectorNoBump();
        }

        public void SetObjectColor(Color color)
        {
            _objectColor = new MyColor(color.R, color.G, color.B);
        }

        public void SetLightColor(Color color)
        {
            double r = (double) color.R / 255;
            double g = (double) color.G / 255;
            double b = (double) color.B / 255;

            _lightColor = new Point3D(r, g, b);
        }

        public void ChangeLightMoving(bool shouldMove)
        {
            _lightMoves = shouldMove;
        }

        public void ChangeUseImage(bool useImage)
        {
            if (useImage&&_imageBitmap == null)
            {
                MessageBox.Show("Proszę wybrać obraz.");
                return;
            }
            _useImage = useImage;
        }

        private bool CheckMapBump(bool useMap, bool useBump)
        {
            if (useMap&&_vectorMapBitmap == null)
            {
                MessageBox.Show("Proszę wybrać mapę wektorów.");
                return false;
            }
            if (useBump && _bumpBitmap == null)
            {
                MessageBox.Show("Proszę wybrać mapę zaburzeń.");
                return false;
            }
            return true;
        }

        public void ChangeUseMap(bool useMap)
        {
            if (CheckMapBump(useMap, _useBump))
            {
                _useMap = useMap;
                RefreshVectorMap();
            }
        }

        public void ChangeUseBump(bool useBump)
        {
            if (CheckMapBump(_useMap, useBump))
            {
                _useBump = useBump;
                RefreshVectorMap();
            }
        }

        private void SetLightVector()
        {
            var integerPart = DateTime.Now.Second % TurnTime;
            double timeInCycle = integerPart + (double) DateTime.Now.Millisecond / 1000;
            timeInCycle *= TwoPi / TurnTime;

            var sinus = Math.Sin(timeInCycle);
            var cosinus = Math.Cos(timeInCycle);

            var square = 1 + BaseHeight * BaseHeight;
            var sqrt = Math.Sqrt(square);
            _movingLightVector = new Point3D(sinus / sqrt, cosinus / sqrt, BaseHeight / sqrt);
        }

        public double Cosinus(Point3D n, Point3D l)
        {
            var cos = n.X * l.X + n.Y * l.Y + n.Z * l.Z;
            return 0 > cos ? 0 : cos;
        }

        public void RefreshVectorNoBump()
        {
            _vectorMap = new Point3D[_vectorMapBitmap.Width, _vectorMapBitmap.Height];

            for(int i=0;i<_vectorMapBitmap.Width;i++)
                for (int j = 0; j < _vectorMapBitmap.Height; j++)
                {
                    var pixel = _vectorMapBitmap.GetPixel(i, j);
                    double x = pixel.R / 127.5 - 1;
                    double y = pixel.G / 127.5 - 1;
                    double z = pixel.B / 127.5 - 1;
                    var square = x * x + y * y + z * z;
                    var sqrt = Math.Sqrt(square);
                    x = x / sqrt;
                    y = y / sqrt;
                    z = z / sqrt;
                    
                    _vectorMap[i, j] = new Point3D(x, y, z);
                }
        }

        public void RefreshVectorWithBumb()
        {
            int width = _bumpBitmap.Width < _vectorMapBitmap.Width ? _bumpBitmap.Width : _vectorMapBitmap.Width;
            int height = _bumpBitmap.Height < _vectorMapBitmap.Height ? _bumpBitmap.Height : _vectorMapBitmap.Height;

            _vectorMap = new Point3D[width, height];

            for (int i = 0; i < width - 1; i++)
            {
                for (int j = 0; j < height - 1; j++)
                {
                    var pixel = _bumpBitmap.GetPixel(i, j);
                    var nextPixelX = _bumpBitmap.GetPixel(i + 1, j);
                    var nextPixelY = _bumpBitmap.GetPixel(i, j + 1);

                    double dhx = ((double)nextPixelX.R - pixel.R)/256;
                    double dhy = ((double)nextPixelY.R - pixel.R)/256;

                    var vectorMapPixel = _vectorMapBitmap.GetPixel(i, j);
                    double nx = vectorMapPixel.R / 127.5 - 1;
                    double ny = vectorMapPixel.G / 127.5 - 1;
                    double nz = vectorMapPixel.B / 127.5 - 1;

                    double Tx = 1;
                    double Ty = 0;
                    double Tz = -nx;

                    double Bx = 0;
                    double By = 1;
                    double Bz = -ny;

                    double Dx = Tx * dhx + Bx * dhy;
                    double Dy = Ty * dhx + By * dhy;
                    double Dz = Tz * dhx + Bz * dhy;

                    double Nx = nx + Dx;
                    double Ny = ny + Dy;
                    double Nz = nz + Dz;

                    double squareLen = Nx * Nx + Ny * Ny + Nz * Nz;
                    double sqrt = Math.Sqrt(squareLen);
                    _vectorMap[i, j] = new Point3D(Nx / sqrt, Ny / sqrt, Nz / sqrt);
                }
            }
        }
    }

    public struct MyColor
    {
        public readonly byte R;
        public readonly byte G;
        public readonly byte B;

        public MyColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
    }

    public struct Point3D
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
