using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using CartoPrime.Resources;
using Xamarin.Forms;

namespace CartoPrime.Custom.Converters
{
    public class StringToFontAwesomeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            Type t = typeof(FontAwesomeSolid);
            FieldInfo[] props = t.GetFields();
            
            foreach (var item in props)
            {
                var itemvalue = item.Name;
                if (value == null) return "";
                if(value.Equals(itemvalue))
                {
                    return item.GetValue(props);
                }
            }            

            return FontAwesomeSolid.CreditCard;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
