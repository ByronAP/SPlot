using SplotControl.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SplotControl.Renderer
{
    internal static class GridRenderer
    {
        internal static void Render(Canvas plotCanvas, GridOptions gridOptions, double maxPointValue)
        {
            if (plotCanvas.ActualHeight <= 0) return;

            if (gridOptions.ShowYLines) RenderYLines(plotCanvas, gridOptions, maxPointValue);

            if (gridOptions.ShowXLines) RenderXLines(plotCanvas, gridOptions, maxPointValue);
        }

        internal static double CalculateXGutter(Canvas plotCanvas, GridOptions gridOptions, double maxPointValue)
        {
            if (plotCanvas.ActualHeight <= 0) return 0d;

            var ppdip = VisualTreeHelper.GetDpi(plotCanvas).PixelsPerDip;

            var stringToMeasure = maxPointValue.ToString(gridOptions.LineLabelFormat);

            var stringSize = Utils.Drawing.MeasureString(stringToMeasure, ppdip, gridOptions.FontFamily, gridOptions.FontStyle, gridOptions.FontWeight, gridOptions.FontStretch, gridOptions.FontSize);

            return stringSize.Width + 1;
        }

        private static void RenderYLines(Canvas plotCanvas, GridOptions gridOptions, double maxPointValue)
        {
        }

        private static void RenderXLines(Canvas plotCanvas, GridOptions gridOptions, double maxPointValue)
        {
            if (plotCanvas.ActualHeight <= 0) return;

            var canvasHeight = plotCanvas.ActualHeight - (gridOptions.GridLineWidth * (gridOptions.GridLineCount));
            var canvasWidth = plotCanvas.ActualWidth;
            var itemHeight = (canvasHeight / gridOptions.GridLineCount);
            var lineItemPoints = maxPointValue / gridOptions.GridLineCount;
            var linePointValue = maxPointValue;

            var gridElements = new List<UIElement>();

            for (var i = 0; i < gridOptions.GridLineCount+1; i++)
            {
                var line = new Line();
                line.Fill = gridOptions.GridBrush;
                line.Stroke = gridOptions.GridBrush;
                line.StrokeThickness = gridOptions.GridLineWidth;
                line.X1 = 0d;
                line.Y1 =  (itemHeight * i) + (gridOptions.GridLineWidth * i);
                line.X2 = canvasWidth;
                line.Y2 = line.Y1;

                gridElements.Add(line);

                if(gridOptions.ShowXLineLabels)
                {
                    var text = new TextBlock();
                    var topPosition = line.Y2 - gridOptions.GridLineWidth - gridOptions.FontSize - (gridOptions.FontSize / 2);
                    text.Text = linePointValue.ToString(gridOptions.LineLabelFormat);
                    text.FontSize = gridOptions.FontSize;
                    text.Foreground = gridOptions.LineLabelBrush;
                    text.Background = Utils.Drawing.TransparentBrush;
                    text.SetValue(Canvas.LeftProperty, 1d);
                    text.SetValue(Canvas.TopProperty, topPosition);

                    gridElements.Add(text);
                }
                
                linePointValue -= lineItemPoints;
            }

            foreach (var element in gridElements) _ = plotCanvas.Children.Add(element);
        }
    }
}
