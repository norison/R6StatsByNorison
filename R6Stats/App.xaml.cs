using R6API;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace R6Stats
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IoC.Setup();

            Current.MainWindow = new MainView();
            Current.MainWindow.Show();
        }
    }
}
