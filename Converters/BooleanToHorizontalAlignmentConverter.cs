using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace TaskSchedulerDemo.Converters
{
    public class BooleanToHorizontalAlignmentConverter : IValueConverter
    {
        public HorizontalAlignment TrueValue { get; set; }
        public HorizontalAlignment FalseValue { get; set; }

        public BooleanToHorizontalAlignmentConverter()
        {
            TrueValue = HorizontalAlignment.Right; // User messages align right
            FalseValue = HorizontalAlignment.Left; // AI messages align left
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
