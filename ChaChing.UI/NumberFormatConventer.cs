using System;
using System.Globalization;
using System.Windows.Data;

namespace ChaChing.UI
{
    public class NumberFormatConventer : IValueConverter
    {
        private readonly CultureInfo _desiredCultureInfo = new CultureInfo("pl-PL");
        private readonly CultureInfo _currentCultureInfo = new CultureInfo(CultureInfo.CurrentCulture.Name);

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return FormatInput(value, _desiredCultureInfo, _currentCultureInfo);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return FormatInput(value, _currentCultureInfo, _desiredCultureInfo);
        }

        private string FormatInput(object value, CultureInfo from, CultureInfo to)
        {
            var number = value?.ToString() ?? "0";
            var isParsed = decimal.TryParse(number,NumberStyles.Any, from, out var result);
            var formatedNumber = isParsed ? result.ToString("N2", to): number;
            return formatedNumber;
        }
    }
}
