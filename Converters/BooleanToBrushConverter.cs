using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TaskSchedulerDemo.Converters
{
    public class BooleanToBrushConverter : IValueConverter
    {
        public Brush TrueValue { get; set; }
        public Brush FalseValue { get; set; }

        public BooleanToBrushConverter()
        {
            TrueValue = new SolidColorBrush(Color.FromRgb(230, 243, 255)); // Light blue for user messages
            FalseValue = new SolidColorBrush(Color.FromRgb(240, 240, 240)); // Light gray for AI messages
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool boolValue))
                return FalseValue;

            return boolValue ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
