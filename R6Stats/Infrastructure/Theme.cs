using System;
using System.Windows;

namespace R6Stats
{
    static class Theme
    {
        public static ResourceDictionary[] GetLightTheme()
        {
            return new ResourceDictionary[]
            {
                new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml") },
                new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml") },
                new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.LightBlue.xaml") },
                new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.LightBlue.xaml") },
                GetLight()
            };
        }
         
        public static ResourceDictionary[] GetDarkTheme()
        {
            
            return new ResourceDictionary[]
            {
                new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml") },
                new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml") },
                new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Yellow.xaml") },
                new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Yellow.xaml") },
                GetDark()
            };
        }

        private static ResourceDictionary GetLight()
        {
            var uri = new Uri("Resources/Styles/Light.xaml", UriKind.Relative);
            return Application.LoadComponent(uri) as ResourceDictionary;
        }

        private static ResourceDictionary GetDark()
        {
            var uri = new Uri("Resources/Styles/Dark.xaml", UriKind.Relative);
            return Application.LoadComponent(uri) as ResourceDictionary;
        }
    }
}
