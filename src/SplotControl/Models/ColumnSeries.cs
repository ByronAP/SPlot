using SplotControl.Utils;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace SplotControl.Models
{
    public class ColumnSeries : NotifyBase
    {
        public string Name { get => _name; set => SetField(ref _name, value); }
        private string _name = string.Empty;

        public ObservableCollection<DateTimeDataPoint> Points { get => _points; set => SetField(ref _points, value); }
        private ObservableCollection<DateTimeDataPoint> _points = new();

        public bool Reverse { get => _reverse; set => SetField(ref _reverse, value); }
        private bool _reverse;

        public Brush PointStrokeBrush { get => _pointStrokeBrush; set => SetField(ref _pointStrokeBrush, value); }
        private Brush _pointStrokeBrush = SystemColors.WindowFrameBrush;

        public double PointStrokeThickness { get => _pointStrokeThickness; set => SetField(ref _pointStrokeThickness, value); }
        private double _pointStrokeThickness = 1;

        public Brush PointFillBrush { get => _pointFillBrush; set => SetField(ref _pointFillBrush, value); }
        private Brush _pointFillBrush = SystemColors.WindowFrameBrush;

        public string ToolTipTemplate { get => _toolTipTemplate; set => SetField(ref _toolTipTemplate, value); }
        private string _toolTipTemplate = "{0}\r\n{1}";
    }
}
