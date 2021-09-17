using System;
using System.Globalization;
using Xamarin.Forms;

namespace CartoPrime.Custom.Converters
{
    public class TelComprador2MsgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "Tel. não cadastrado"; 
            string tel =  value as string;
            //Verifica se telefone não está disponível e mostra mensagem caso não, se sim mostra o telefone            
            if(String.IsNullOrEmpty(tel))
            {
                return "Tel. não cadastrado";
            }
            else
            {
                return tel;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
