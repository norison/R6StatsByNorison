using System;
using System.Globalization;
using System.Windows.Data;

namespace R6Stats
{
    class ThemeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)parameter == "Light" && Properties.Settings.Default.Theme == "Light")
                return true;
            else if ((string)parameter == "Dark" && Properties.Settings.Default.Theme == "Dark")
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
