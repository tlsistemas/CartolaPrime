using System;
using System.Globalization;
using Xamarin.Forms;

namespace CartoPrime.Custom.Converters
{
    public class DataEntregaToLocalDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? dataServer = value as DateTime?;
            if (dataServer != null)
            {
                DateTime dataConvertida = DateTime.SpecifyKind(dataServer.Value, DateTimeKind.Utc);
                string _dateResult = dataServer.Value.ToLocalTime().ToString("dd/MM/yyyy hh:mm tt");
                return _dateResult;
            }            
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
