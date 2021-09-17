using System;
using System.Globalization;
using Xamarin.Forms;

namespace CartoPrime.Custom.Converters
{
    public class TitleCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            culture = culture ?? CultureInfo.CurrentCulture;
            return culture.TextInfo.ToTitleCase(value?.ToString()?.ToLower(culture));
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
