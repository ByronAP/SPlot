using SplotControl.Utils;
using System.Windows;
using System.Windows.Media;

namespace SplotControl.Models
{
    public class GridOptions : NotifyBase
    {
        public bool ShowXLines { get => _showXLines; set => SetField(ref _showXLines, value); }
        private bool _showXLines = true;

        public bool ShowYLines { get => _showYLines; set => SetField(ref _showYLines, value); }
        private bool _showYLines = true;

        public uint GridLineCount { get => _gridLineCount; set => SetField(ref _gridLineCount, value); }
        private uint _gridLineCount = 8;

        public double GridLineWidth { get => _gridLineWidth; set => SetField(ref _gridLineWidth, value); }
        private double _gridLineWidth = 4d;

        public Brush GridBrush { get => _gridBrush; set => SetField(ref _gridBrush, value); }
        private Brush _gridBrush = SystemColors.InactiveBorderBrush;

        public bool ShowYLineLabels { get => _showYLineLabels; set => SetField(ref _showYLineLabels, value); }
        private bool _showYLineLabels = true;

        public bool ShowXLineLabels { get => _showXLineLabels; set => SetField(ref _showXLineLabels, value); }
        private bool _showXLineLabels = true;

        public uint FontSize { get => _fontSize; set => SetField(ref _fontSize, value); }
        private uint _fontSize = 12;

        public FontFamily FontFamily { get => _fontFamily; set => SetField(ref _fontFamily, value); }
        private FontFamily _fontFamily = new FontFamily("Segoe UI");

        public FontStyle FontStyle { get => _fontStyle; set => SetField(ref _fontStyle, value); }
        private FontStyle _fontStyle = FontStyles.Normal;

        public FontWeight FontWeight {  get => _fontWeight; set => SetField(ref _fontWeight, value); }
        private FontWeight _fontWeight = FontWeights.Regular;

        public FontStretch FontStretch { get => _fontStretch; set => SetField(ref _fontStretch, value); }
        private FontStretch _fontStretch = FontStretches.Normal;

        public Brush LineLabelBrush { get => _lineLabelBrush; set => SetField(ref _lineLabelBrush, value); }
        private Brush _lineLabelBrush = SystemColors.InactiveCaptionBrush;

        public string LineLabelFormat { get => _lineLabelFormat; set => SetField(ref _lineLabelFormat, value); }
        private string _lineLabelFormat = "N0";
    }
}
