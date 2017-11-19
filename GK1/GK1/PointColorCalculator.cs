using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK1
{
    public interface IPointColorCalculator
    {
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

    public class PointColorCalculator
    {
        private Point3D[,] _vectorMap;
        private Point3D _lightVector = new Point3D(0.5, 0.5, 0.71);
        private Point3D _lightColor = new Point3D(0.21, 0.56, 0.24);

        private Bitmap ImageBitmap;
        private Bitmap VectorMapBitmap;
        private Bitmap BumpBitmap;

        private bool _useImage;
        private bool _useMap;
        private bool _useBump;
        private bool _lightMoves;

        private static Color constColor=Color.FromArgb(100,100,100);

        public MyColor GetPixelColor(int x, int y)
        {
            var objectColor = !_useImage ? constColor : ImageBitmap.GetPixel(x, y);

            Point3D vector;
            if (!_useMap || x >= _vectorMap.GetLength(0) || y >= _vectorMap.GetLength(1))
                vector = new Point3D(0, 0, 1);
            else
                vector = _vectorMap[x, y];

            double cos = Cosinus(vector, _lightVector);
            var r = (byte) (objectColor.R * _lightColor.X * cos);
            var g = (byte) (objectColor.G * _lightColor.Y * cos);
            var b = (byte) (objectColor.B * _lightColor.Z * cos);

            return new MyColor(r, g, b);
        }

        public double Cosinus(Point3D n, Point3D l)
        {
            var cos = n.X * l.X + n.Y * l.Y + n.Z * l.Z;
            return 0 > cos ? 0 : cos;
        }

        public void RefreshVectorNoBump()
        {
            _vectorMap = new Point3D[VectorMapBitmap.Width, VectorMapBitmap.Height];

            for(int i=0;i<VectorMapBitmap.Width;i++)
                for (int j = 0; j < VectorMapBitmap.Height; j++)
                {
                    var pixel = VectorMapBitmap.GetPixel(i, j);
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
            int width = BumpBitmap.Width < VectorMapBitmap.Width ? BumpBitmap.Width : VectorMapBitmap.Width;
            int height = BumpBitmap.Height < VectorMapBitmap.Height ? BumpBitmap.Height : VectorMapBitmap.Height;

            _vectorMap = new Point3D[width, height];

            for (int i = 0; i < width - 1; i++)
            {
                for (int j = 0; j < height - 1; j++)
                {
                    var pixel = BumpBitmap.GetPixel(i, j);
                    var nextPixelX = BumpBitmap.GetPixel(i + 1, j);
                    var nextPixelY = BumpBitmap.GetPixel(i, j + 1);

                    int dhx = nextPixelX.R - pixel.R;
                    int dhy = nextPixelY.R - pixel.R;

                    var vectorMapPixel = VectorMapBitmap.GetPixel(i, j);
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

        public void LoadImageTexture()
        {
            
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
