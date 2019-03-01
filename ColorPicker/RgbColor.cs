using System;

namespace ColorPicker
{
    public struct RgbColor
    {
        public RgbColor(double r, double g, double b)
        {
            R = r;
            G = g;
            B = b;
        }

        public double R { get; set; }

        public double G { get; set; }

        public double B { get; set; }

        public CmykColor ConvertToCmykColor()
        {
            double rValueFromZeroToOne = this.R / 255;
            double gValueFromZeroToOne = this.G / 255;
            double bValueFromZeroToOne = this.B / 255;

            var resultCmykColor = new CmykColor();
            double colorK = 1 - Math.Max(Math.Max(rValueFromZeroToOne, gValueFromZeroToOne), bValueFromZeroToOne);

            if (Math.Abs(1 - colorK) < 1e-10)
            {
                resultCmykColor.C = 0;
                resultCmykColor.M = 0;
                resultCmykColor.Y = 0;
            }
            else
            {
                resultCmykColor.C = (1 - rValueFromZeroToOne - colorK) / (1 - colorK) * 100;
                resultCmykColor.M = (1 - gValueFromZeroToOne - colorK) / (1 - colorK) * 100;
                resultCmykColor.Y = (1 - bValueFromZeroToOne - colorK) / (1 - colorK) * 100;
            }

            resultCmykColor.K = colorK * 100;

            return resultCmykColor;
        }

        public HsvColor ConvertToHsvColor()
        {
            double rValueFromZeroToOne = this.R / 255;
            double gValueFromZeroToOne = this.G / 255;
            double bValueFromZeroToOne = this.B / 255;

            var hsvColor = new HsvColor();
            double cMax = Math.Max(Math.Max(rValueFromZeroToOne, gValueFromZeroToOne), bValueFromZeroToOne);
            double cMin = Math.Min(Math.Min(rValueFromZeroToOne, gValueFromZeroToOne), bValueFromZeroToOne);
            double delta = cMax - cMin;

            if (Math.Abs(delta) < 1e-10)
            {
                hsvColor.H = 0;
                hsvColor.S = 0;

                if (Math.Abs(cMax) < 1e-10)
                {
                    hsvColor.V = 0;
                }
                else if (Math.Abs(cMax - 255) < 1e-10)
                {
                    hsvColor.V = 100;
                }
                else
                {
                    hsvColor.V = cMax * 100;
                }

                return hsvColor;
            }
            if (Math.Abs(cMax - rValueFromZeroToOne) < 1e-10)
            {
                hsvColor.H = 60 * (((gValueFromZeroToOne - bValueFromZeroToOne) / delta) % 6);
            }
            else if (Math.Abs(cMax - gValueFromZeroToOne) < 1e-10)
            {
                hsvColor.H = 60 * ((bValueFromZeroToOne - rValueFromZeroToOne) / delta + 2);
            }
            else
            {
                hsvColor.H = 60 * ((rValueFromZeroToOne - gValueFromZeroToOne) / delta + 4);
            }

            if (hsvColor.H < 0)
            {
                hsvColor.H = 360 + hsvColor.H;
            }
            hsvColor.S = delta / cMax * 100;
            hsvColor.V = cMax * 100;

            return hsvColor;
        }

        public LabColor ConvertToLabColor() => this.ConvertToXyzColor().ConvertToLabColor();

        private XyzColor ConvertToXyzColor()
        {
            double rValueFromZeroToOne = this.R / 255;
            double gValueFromZeroToOne = this.G / 255;
            double bValueFromZeroToOne = this.B / 255;

            double r = PivotRgb(rValueFromZeroToOne);
            double g = PivotRgb(gValueFromZeroToOne);
            double b = PivotRgb(bValueFromZeroToOne);

            var xyzColor = new XyzColor
            {
                X = r * 0.4124 + g * 0.3576 + b * 0.1805,
                Y = r * 0.2126 + g * 0.7152 + b * 0.0722,
                Z = r * 0.0193 + g * 0.1192 + b * 0.9505
            };

            return xyzColor;
        }

        private double PivotRgb(double n)
        {
            return (n > 0.04045 ? Math.Pow((n + 0.055) / 1.055, 2.4) : n / 12.92) * 100;
        }
    }
}