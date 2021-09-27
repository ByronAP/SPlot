using SplotControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SplotControl.Renderer
{
    internal static class LineSeriesRenderer
    {
        internal static void Render(Canvas plotCanvas, LineSeries series, double maxPointValue, double xGutter)
        {
            if (plotCanvas.ActualHeight <= 0 || series.Points == null || !series.Points.Any()) return;

            var orderedPoints = series.Reverse ? series.Points.OrderByDescending(x => x.DTOffset) : series.Points.OrderBy(x => x.DTOffset);

            // calculate the max width of each item
            var itemWidth = (plotCanvas.ActualWidth - xGutter) / orderedPoints.Count();

            var canvasHeight = plotCanvas.ActualHeight - (itemWidth / 2);
            var pointsPerItem = canvasHeight / maxPointValue;

            var uiLineElements = new List<UIElement>();
            var uiPointElements = new List<UIElement>();
            var ttlWidth = xGutter;

            var lastPoint = new Point(0, 0);
            foreach (var point in orderedPoints)
            {
                var pointDT = point.ToLocalDT ? point.DTOffset.ToLocalTime() : point.DTOffset;
                var toolTipText = series.ToolTipTemplate.Replace("{0}", pointDT.ToString(point.DTOffsetDisplayFormat));
                toolTipText = toolTipText.Replace("{1}", point.Value.ToString(point.ValueDisplayFormat));
                var pointTop = canvasHeight - (pointsPerItem * Convert.ToDouble(point.Value)) - (itemWidth / 2);

                var visibleEllipse = new Ellipse();
                visibleEllipse.SetValue(Canvas.LeftProperty, ttlWidth);
                visibleEllipse.SetValue(Canvas.TopProperty, pointTop);
                visibleEllipse.Width = itemWidth;
                visibleEllipse.Height = visibleEllipse.Width;
                visibleEllipse.Stroke = point.StrokeBrush == null ? series.PointStrokeBrush : point.StrokeBrush;
                visibleEllipse.StrokeThickness = series.PointStrokeThickness;
                visibleEllipse.Fill = point.FillBrush == null ? series.PointFillBrush : point.FillBrush;
                visibleEllipse.ToolTip = toolTipText;
                visibleEllipse.SetValue(ToolTipService.InitialShowDelayProperty, 0);

                uiPointElements.Add(visibleEllipse);

                if (lastPoint.X + lastPoint.Y != 0)
                {
                    var line = new Line();
                    line.Fill = series.LineFillBrush;
                    line.Stroke = series.LineStrokeBrush;
                    line.StrokeThickness = series.AutoLineThickness ? itemWidth / 6 : series.LineStrokeThickness;
                    line.X1 = lastPoint.X;
                    line.Y1 = lastPoint.Y;
                    line.X2 = ttlWidth + (itemWidth / 2);
                    line.Y2 = pointTop + (itemWidth / 2);

                    uiLineElements.Add(line);
                }

                lastPoint.X = ttlWidth + (itemWidth / 2);
                lastPoint.Y = pointTop + (itemWidth / 2);
                ttlWidth += itemWidth;
            }

            foreach (var element in uiLineElements) _ = plotCanvas.Children.Add(element);
            foreach (var element in uiPointElements) _ = plotCanvas.Children.Add(element);
        }

    }
}
