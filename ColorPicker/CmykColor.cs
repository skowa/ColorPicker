namespace ColorPicker
{
    public struct CmykColor
    {
        public CmykColor(double c, double m, double y, double k)
        {
            C = c;
            M = m;
            Y = y;
            K = k;
        }

        public double C { get; set; }

        public double M { get; set; }

        public double Y { get; set; }

        public double K { get; set; }

        public RgbColor ConvertToRgbColor()
        {
            return new RgbColor
            {
                R = (byte) (255 * (1 - this.C / 100) * (1 - this.K / 100)),
                G = (byte) (255 * (1 - this.M / 100) * (1 - this.K / 100)),
                B = (byte) (255 * (1 - this.Y / 100) * (1 - this.K / 100))
            };
        }

        public HsvColor ConvertToHsvColor() => this.ConvertToRgbColor().ConvertToHsvColor();

        public LabColor ConvertToLabColor() => this.ConvertToRgbColor().ConvertToLabColor();
    }
}