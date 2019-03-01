using System;
using System.Diagnostics;

namespace ColorPicker
{
    public struct HsvColor
    {
        public HsvColor(double h, double s, double v)
        {
            H = h;
            S = s;
            V = v;
        }

        public double H { get; set; }

        public double S { get; set; }

        public double V { get; set; }

        public RgbColor ConvertToRgbColor()
        {
            double sValueFromZeroToOne = this.S / 100;
            double vValueFromZeroToOne = this.V / 100;
            double c = vValueFromZeroToOne * sValueFromZeroToOne;
            double x = c * (1 - Math.Abs(this.H / 60 % 2 - 1));
            double m = vValueFromZeroToOne - c;

            var rgbColor = new RgbColor();
            double rColorIntermediate = 0, gColorIntermediate = 0, bColorIntermediate = 0;

            switch (this.H)
            {
                case double degree when degree >= 0 && degree < 60:
                    rColorIntermediate = c;
                    gColorIntermediate = x;
                    break;
                case double degree when degree >= 60 && degree < 120:
                    rColorIntermediate = x;
                    gColorIntermediate = c;
                    break;
                case double degree when degree >= 120 && degree < 180:
                    gColorIntermediate = c;
                    bColorIntermediate = x;
                    break;
                case double degree when degree >= 180 && degree < 240:
                    gColorIntermediate = x;
                    bColorIntermediate = c;
                    break;
                case double degree when degree >= 240 && degree < 300:
                    rColorIntermediate = x;
                    bColorIntermediate = c;
                    break;
                case double degree when degree >= 300 && degree < 361:
                    rColorIntermediate = c;
                    bColorIntermediate = x;
                    break;
            }

            rgbColor.R = (rColorIntermediate + m) * 255;
            rgbColor.G = (gColorIntermediate + m) * 255;
            rgbColor.B = (bColorIntermediate + m) * 255;
            
            return rgbColor;
        }

        public CmykColor ConvertToCmykColor() => this.ConvertToRgbColor().ConvertToCmykColor();

        public LabColor ConvertToLabColor() => this.ConvertToRgbColor().ConvertToLabColor();
    }
}