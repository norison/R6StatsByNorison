using System;
using System.Globalization;
using System.Windows.Data;

namespace R6Stats
{
    class LanguageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)parameter == "en-US" && Properties.Settings.Default.Language == "en-US")
                return true;
            else if ((string)parameter == "ru-RU" && Properties.Settings.Default.Language == "ru-RU")
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
