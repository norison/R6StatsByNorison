using System;
using System.Globalization;
using System.Windows.Data;
using static R6API.Enums;

namespace R6Stats
{
    class PlatformConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "uplay":
                    return Platform.UPLAY;
                case "xbl":
                    return Platform.XBOX;
                case "psn":
                    return Platform.PLAYSTATION;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
