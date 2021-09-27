using SplotControl.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SPlotDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        internal void GeneratePlot()
        {
            var max = new Random(Guid.NewGuid().GetHashCode()).Next(10, 400);
            var min = new Random(Guid.NewGuid().GetHashCode()).Next(0, max /9);
            var count = new Random(Guid.NewGuid().GetHashCode()).Next(20, 100);

            var lineSeries1 = new LineSeries();
            var lineSeries2 = new LineSeries()
            {
                LineFillBrush = SystemColors.ActiveBorderBrush,
                LineStrokeBrush = SystemColors.ActiveBorderBrush,
                PointFillBrush = SystemColors.ActiveBorderBrush,
                PointStrokeBrush = SystemColors.ActiveBorderBrush
            };
            var columnSeries = new ColumnSeries();
            for (var i = 0; i < count; i++)
            {
                lineSeries1.Points.Add(new DateTimeDataPoint() { DTOffset = DateTimeOffset.UtcNow.AddMinutes(-i).AddHours(-i), ToLocalDT = true, Value = new Random(Guid.NewGuid().GetHashCode()).Next(min, max) });
                lineSeries2.Points.Add(new DateTimeDataPoint() { DTOffset = DateTimeOffset.UtcNow.AddMinutes(-i), ToLocalDT = true, Value = new Random(Guid.NewGuid().GetHashCode()).Next(min, max) });
                columnSeries.Points.Add(new DateTimeDataPoint() { DTOffset = DateTimeOffset.UtcNow.AddMinutes(-i), ToLocalDT = true, Value = new Random(Guid.NewGuid().GetHashCode()).Next(min, max) });
            }

            PlotControl.LineSeries = new List<LineSeries> { lineSeries1, lineSeries2 };
            PlotControl.ColumnSeries = columnSeries;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GeneratePlot();
        }
    }
}
