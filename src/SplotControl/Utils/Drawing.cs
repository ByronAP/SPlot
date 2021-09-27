using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace SplotControl.Utils
{
    internal static class Drawing
    {
        public static Brush TransparentBrush = new SolidColorBrush(Colors.Transparent);

        public static Size MeasureString(string candidate, double pixelsPerDip, FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, double fontSize)
        {
            var formattedText = new FormattedText(candidate, CultureInfo.CurrentUICulture,
                                                  FlowDirection.LeftToRight,
                                                  new Typeface(
                                                      fontFamily,
                                                      fontStyle,
                                                      fontWeight,
                                                      fontStretch),
                                                  fontSize,
                                                  Brushes.Black,
                                                  pixelsPerDip);

            return new Size(formattedText.Width, formattedText.Height);
        }
    }
}
