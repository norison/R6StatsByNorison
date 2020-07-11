using System;
using System.Windows;

namespace R6Stats
{
    static class Localization
    {
        public static ResourceDictionary GetRussian()
        {
            var ruRU = new Uri("Resources/Localization/ru-RU.xaml", UriKind.Relative);
            return Application.LoadComponent(ruRU) as ResourceDictionary;
        }

        public static ResourceDictionary GetEnglish()
        {
            var enUS = new Uri("Resources/Localization/en-US.xaml", UriKind.Relative);
            return Application.LoadComponent(enUS) as ResourceDictionary;
        }

    }
}
