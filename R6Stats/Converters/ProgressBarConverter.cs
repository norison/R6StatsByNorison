using System;
using System.Globalization;
using System.Windows.Data;

namespace R6Stats
{
    class ProgressBarConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var previous = (float)values[0];
                var next = (float)values[1];
                var mmr = (float)values[2];

                if (previous == 0)
                    return (double)0;
                else if (next == 0)
                    return (double)100;

                

               return (double)((mmr - previous) * 100 / (next - previous));

            }
            catch
            {
                return (double)0;
            }


        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
