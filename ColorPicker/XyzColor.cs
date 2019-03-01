using System;
using System.Diagnostics;

namespace ColorPicker
{
    internal struct XyzColor
    {
        internal XyzColor(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        internal double X { get; set; }

        internal double Y { get; set; }

        internal double Z { get; set; }

        internal RgbColor ConvertToRgbColor()
        {
            double xValueFromZeroToOne = this.X / 100;
            double yValueFromZeroToOne = this.Y / 100;
            double zValueFromZeroToOne = this.Z / 100;

            double r = xValueFromZeroToOne * 3.2406 + yValueFromZeroToOne * -1.5372 + zValueFromZeroToOne * -0.4986;
            double g = xValueFromZeroToOne * -0.9689 + yValueFromZeroToOne * 1.8758 + zValueFromZeroToOne * 0.0415;
            double b = xValueFromZeroToOne * 0.0557 + yValueFromZeroToOne * -0.2040 + zValueFromZeroToOne * 1.0570;

            r = r > 0.0031308 ? 1.055 * Math.Pow(r, 1 / 2.4) - 0.055 : 12.92 * r;
            g = g > 0.0031308 ? 1.055 * Math.Pow(g, 1 / 2.4) - 0.055 : 12.92 * g;
            b = b > 0.0031308 ? 1.055 * Math.Pow(b, 1 / 2.4) - 0.055 : 12.92 * b;

            return new RgbColor
            {
                R = ToRgb(r),
                G = ToRgb(g),
                B = ToRgb(b)
            };
        }

        internal LabColor ConvertToLabColor()
        {

            var x = PivotXyz(this.X / 95.047);
            var y = PivotXyz(this.Y / 100.0);
            var z = PivotXyz(this.Z / 108.883);

            return new LabColor
            {
                L = Math.Max(0, 116 * y - 16),
                A = 500 * (x - y),
                B = 200 * (y - z)
            };
        }

        private double PivotXyz(double n)
        {
            double i = CubicRoot(n);
            return n > 0.008856 ? i : 7.787 * n + 16.0 / 116;
        }

        private double CubicRoot(double n) => Math.Pow(n, (1.0 / 3.0));

        private int ToRgb(double n)
        {
            return Math.Min(255, Math.Max(0, (int)(n * 255)));
        }
    }
}