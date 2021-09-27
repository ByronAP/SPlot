using SplotControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SplotControl.Renderer
{
    internal static class ColumnSeriesRenderer
    {
        internal static void Render(Canvas plotCanvas, ColumnSeries series, double maxPointValue, double xGutter)
        {
            if (plotCanvas.ActualHeight <= 0 || !series.Points.Any()) return;

            var orderedPoints = series.Reverse ? series.Points.OrderByDescending(x => x.DTOffset) : series.Points.OrderBy(x => x.DTOffset);

            // calculate the max width of each item
            var itemWidth = (plotCanvas.ActualWidth - xGutter) / orderedPoints.Count();
            var canvasHeight = plotCanvas.ActualHeight - (series.PointStrokeThickness / 2);
            var heightPerPoint = (canvasHeight - itemWidth) / maxPointValue;

            var uiPointElements = new List<UIElement>();
            var ttlWidth = xGutter;

            foreach (var point in orderedPoints)
            {
                var pointDT = point.ToLocalDT ? point.DTOffset.ToLocalTime() : point.DTOffset;
                var toolTipText = series.ToolTipTemplate.Replace("{0}", pointDT.ToString(point.DTOffsetDisplayFormat));
                toolTipText = toolTipText.Replace("{1}", point.Value.ToString(point.ValueDisplayFormat));
                var pointTop = canvasHeight - (heightPerPoint * Convert.ToDouble(point.Value)) - itemWidth;

                var columnRectangle = new Rectangle();
                columnRectangle.SetValue(Canvas.LeftProperty, ttlWidth);
                columnRectangle.SetValue(Canvas.TopProperty, pointTop);
                columnRectangle.Width = itemWidth / 2;
                columnRectangle.Height = canvasHeight - pointTop - series.PointStrokeThickness;
                columnRectangle.Stroke = series.PointStrokeBrush;
                columnRectangle.StrokeThickness = series.PointStrokeThickness;
                columnRectangle.Fill = series.PointFillBrush;
                columnRectangle.ToolTip = toolTipText;
                columnRectangle.SetValue(ToolTipService.InitialShowDelayProperty, 0);

                uiPointElements.Add(columnRectangle);

                ttlWidth += itemWidth;
            }

            foreach (var element in uiPointElements) _ = plotCanvas.Children.Add(element);
        }
    }
}
