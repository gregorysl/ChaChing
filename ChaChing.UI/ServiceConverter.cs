using System;
using System.Globalization;
using System.Windows.Data;
using ChaChing.UI.ConverterReference;

namespace ChaChing.UI
{
    public class ServiceConverter: IValueConverter
    {
        private readonly ConverterServiceClient _converter = new ConverterServiceClient();

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var number = value?.ToString();
            var request = new NumberToEnglishRequest(number);
            var response = _converter.NumberToEnglish(request);

            return response.NumberToEnglishResult;
        }
 
        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
