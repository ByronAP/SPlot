using SplotControl.Utils;
using System;
using System.Windows.Media;

namespace SplotControl.Models
{
    public class DateTimeDataPoint : NotifyBase
    {
        public string ValueDisplayFormat { get => _valueDisplayFormat; set => SetField(ref _valueDisplayFormat, value); }
        private string _valueDisplayFormat = "N0";

        public decimal Value { get => _value; set => SetField(ref _value, value); }
        private decimal _value = 0m;

        public DateTimeOffset DTOffset { get => _dtOffset; set => SetField(ref _dtOffset, value); }
        private DateTimeOffset _dtOffset = DateTimeOffset.UtcNow;

        public string DTOffsetDisplayFormat { get => _dtOffsetDisplayFormat; set => SetField(ref _dtOffsetDisplayFormat, value); }
        private string _dtOffsetDisplayFormat = "G";

        public bool ToLocalDT { get => _toLocalDT; set => SetField(ref _toLocalDT, value); }
        private bool _toLocalDT = true;

        public Brush? FillBrush { get => _fillBrush; set => SetField(ref _fillBrush, value); }
        private Brush? _fillBrush = null;

        public Brush? StrokeBrush { get => _strokeBrush; set => SetField(ref _strokeBrush, value); }
        private Brush? _strokeBrush = null;
    }
}
