using System;

namespace ColorPicker
{
    public struct LabColor
    {
        public LabColor(double l, double a, double b)
        {
            L = l;
            A = a;
            B = b;
        }

        public double L { get; set; }

        public double A { get; set; }

        public double B { get; set; }

        public RgbColor ConvertToRgbColor() => this.ConvertToXyzColor().ConvertToRgbColor();

        public CmykColor ConvertToCmykColor() => this.ConvertToRgbColor().ConvertToCmykColor();

        public HsvColor ConvertToHsvColor() => this.ConvertToRgbColor().ConvertToHsvColor();

        private XyzColor ConvertToXyzColor()
        {
            double y = (this.L + 16) / 116.0;
            double x = this.A / 500.0 + y;
            double z = y - this.B / 200.0;

            y = Math.Pow(y, 3) > 0.008856 ? Math.Pow(y, 3) : (y - 16.0 / 116) / 7.787;
            x = Math.Pow(x, 3) > 0.008856 ? Math.Pow(x, 3) : (x - 16.0 / 116) / 7.787;
            z = Math.Pow(z, 3) > 0.008856 ? Math.Pow(z, 3) : (z - 16.0 / 116) / 7.787;

            return new XyzColor
            {
                X = 95.047 * x,
                Y = 100.0 * y,
                Z = 108.883 * z
            };
        }
    }
}