using SplotControl.Models;
using SplotControl.Renderer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SplotControl
{
    public partial class SPlotControl : UserControl, INotifyPropertyChanging, INotifyPropertyChanged
    {
        public GridOptions GridOptions { get => _gridOptions; set => SetField(ref _gridOptions, value); }
        private GridOptions _gridOptions = new();

        public string HeaderText { get => _headerText; set => SetField(ref _headerText, value); }
        private string _headerText = "Demo Header Text";

        public ColumnSeries ColumnSeries { get => _columnSeries; set => SetField(ref _columnSeries, value); }
        private ColumnSeries _columnSeries = new();

        public IEnumerable<LineSeries> LineSeries { get => _lineSeries; set => SetField(ref _lineSeries, value); }
        private IEnumerable<LineSeries> _lineSeries = new List<LineSeries>();

        private readonly DispatcherTimer _resizingTimer;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SPlotControl()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            InitializeComponent();

            _resizingTimer = new DispatcherTimer();
            _resizingTimer.Tick += _resizingTimer_Tick;
            _resizingTimer.Interval = TimeSpan.FromMilliseconds(250);
        }

        private void _resizingTimer_Tick(object? sender, EventArgs e)
        {
            _resizingTimer.Stop();
            Render();
        }

        public void Render()
        {
            var timingStopWatch = new Stopwatch();
            timingStopWatch.Start();
            if (PlotCanvas.ActualHeight <= 0) return;

            var maxValue = CalculateMaxSeriesValue();

            if (maxValue <= 0) return;

            var xGutter = 0d;

            PlotCanvas.Children.Clear();

            if (GridOptions != null)
            {
                if (GridOptions.ShowXLineLabels) xGutter = GridRenderer.CalculateXGutter(PlotCanvas, GridOptions, maxValue);

                GridRenderer.Render(PlotCanvas, GridOptions, maxValue);
            }

            if (ColumnSeries != null && ColumnSeries.Points != null && ColumnSeries.Points.Any())
            {
                ColumnSeriesRenderer.Render(PlotCanvas, ColumnSeries, maxValue, xGutter);
            }

            if (LineSeries != null && LineSeries.Any())
            {
                foreach (var series in LineSeries) LineSeriesRenderer.Render(PlotCanvas, series, maxValue, xGutter);
            }
            timingStopWatch.Stop();
            Debug.Print("Rendering took {0}Ms", timingStopWatch.Elapsed.TotalMilliseconds.ToString());
        }

        private double CalculateMaxSeriesValue()
        {
            var maxLinePointValue = 0d;
            try
            {
                if (LineSeries != null && LineSeries.Any())
                {
                    maxLinePointValue = Convert.ToDouble(LineSeries.Max(x => x?.Points?.Max(y => y.Value)));
                }
            }
            catch
            {
                // ignore
            }
            var maxColumnPointValue = 0d;
            try
            {
                if (ColumnSeries != null && ColumnSeries.Points != null && ColumnSeries.Points.Any())
                {
                    maxColumnPointValue = Convert.ToDouble(ColumnSeries.Points?.Max(x => x?.Value));
                }
            }
            catch
            {
                // ignore
            }

            return maxLinePointValue > maxColumnPointValue ? maxLinePointValue : maxColumnPointValue;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _resizingTimer.Start();
        }

        #region Notify Change

#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.

        protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            OnPropertyChanging(propertyName);
            field = value;
            OnPropertyChanged(propertyName);
            Render();
            return true;
        }
        #endregion
    }
}