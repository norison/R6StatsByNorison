using System.Collections.ObjectModel;
using System.Windows;

namespace R6Stats
{
    static class Settings
    {
        public static void SetSettings()
        {
            ChangeTheme(Properties.Settings.Default.Theme);
            ChangeLanguage(Properties.Settings.Default.Language);
        }

        public static void SaveSettings() => Properties.Settings.Default.Save();

        public static void ChangeTheme(string themeName)
        {
            var appResources = Application.Current.Resources.MergedDictionaries;

            if (themeName == "Light")
            {
                foreach (var resource in Theme.GetDarkTheme())
                    appResources.Remove(resource);
                foreach (var resource in Theme.GetLightTheme())
                    appResources.Add(resource);
            }
            else
            {
                foreach (var resource in Theme.GetLightTheme())
                    appResources.Remove(resource);
                foreach (var resource in Theme.GetDarkTheme())
                    appResources.Add(resource);
            }

            Properties.Settings.Default.Theme = themeName;
        }

        public static void ChangeLanguage(string languageName)
        {
            var appResources = Application.Current.Resources.MergedDictionaries;

            if (languageName == "ru-RU")
            {
                appResources.Remove(Localization.GetEnglish());
                appResources.Add(Localization.GetRussian());
            }
            else
            {
                appResources.Remove(Localization.GetRussian());
                appResources.Add(Localization.GetEnglish());
            }

            Properties.Settings.Default.Language = languageName;
        }
    }
}
