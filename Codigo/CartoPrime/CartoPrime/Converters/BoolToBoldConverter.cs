using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CartoPrime.Converters
{
    public class BoolToBoldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FontAttributes font = FontAttributes.None;

            if ((bool)value)
            {
                font = FontAttributes.Bold;
            }
            else
            {
                font = FontAttributes.None;
            }

            return font;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
