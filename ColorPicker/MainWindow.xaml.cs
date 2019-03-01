using System.Windows;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _loading;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ColorPicker_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (_loading)
            {
                return;
            }

            var rgbColor = new RgbColor(ColorPicker.R, ColorPicker.G, ColorPicker.B);

            _loading = true;
            SetCmykColorToSliders(rgbColor.ConvertToCmykColor());
            SetHsvColorToSliders(rgbColor.ConvertToHsvColor());
            SetLabColorToSliders(rgbColor.ConvertToLabColor());
            _loading = false;
        }

        private void CmykColorSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_loading)
            {
                return;
            }
            
            var cmykColor = new CmykColor(CmykColorC.Value, CmykColorM.Value, CmykColorY.Value, CmykColorK.Value);

            _loading = true;
            SetRgbColorToColorPicker(cmykColor.ConvertToRgbColor());
            SetHsvColorToSliders(cmykColor.ConvertToHsvColor());
            SetLabColorToSliders(cmykColor.ConvertToLabColor());
            _loading = false;
        }

        private void LabColorSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_loading)
            {
                return;
            }

            var labColor = new LabColor(LabColorL.Value, LabColorA.Value, LabColorB.Value);

            _loading = true;
            SetRgbColorToColorPicker(labColor.ConvertToRgbColor());
            SetCmykColorToSliders(labColor.ConvertToCmykColor());
            SetHsvColorToSliders(labColor.ConvertToHsvColor());
            _loading = false;
        }

        private void HsvColor_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_loading)
            {
                return;
            }

            var hsvColor = new HsvColor(HsvColorH.Value, HsvColorS.Value, HsvColorV.Value);

            _loading = true;
            SetRgbColorToColorPicker(hsvColor.ConvertToRgbColor());
            SetCmykColorToSliders(hsvColor.ConvertToCmykColor());
            SetLabColorToSliders(hsvColor.ConvertToLabColor());
            _loading = false;
        }

        private void SetCmykColorToSliders(CmykColor cmykColor)
        {
            CmykColorC.Value = cmykColor.C;
            CmykColorM.Value = cmykColor.M;
            CmykColorY.Value = cmykColor.Y;
            CmykColorK.Value = cmykColor.K;
        }

        private void SetRgbColorToColorPicker(RgbColor rgbColor)
        {
            ColorPicker.R = (byte) rgbColor.R;
            ColorPicker.G = (byte) rgbColor.G;
            ColorPicker.B = (byte) rgbColor.B;
        }

        private void SetHsvColorToSliders(HsvColor hsvColor)
        {
            HsvColorH.Value = hsvColor.H;
            HsvColorS.Value = hsvColor.S;
            HsvColorV.Value = hsvColor.V;
        }

        private void SetLabColorToSliders(LabColor labColor)
        {
            LabColorL.Value = labColor.L;
            LabColorA.Value = labColor.A;
            LabColorB.Value = labColor.B;
        }
    }
}
