using Ninject;
using R6API;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace R6Stats
{
    static class IoC
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static ApplicationPageViewModel ApplicationPage => Get<ApplicationPageViewModel>();
        public static ProgressBarViewModel ProgressBar => Get<ProgressBarViewModel>();

        public static void Setup()
        {
            try { Task.Run(async () => await API.GetAPI().Connect()).Wait(); }
            catch { ErrorBox.Show("Ti dolbaeb? Vklu4i internet", "Connection error"); Application.Current.Shutdown(); }
            SetBindings();
        }

        private static void SetBindings()
        {
            Kernel.Bind<MainViewModel>().ToSelf();
            Kernel.Bind<IDataService<ObservableCollection<Player>>>().To<JsonService<ObservableCollection<Player>>>().WithPropertyValue("Path", "players.json");
            Kernel.Bind<ApplicationPageViewModel>().ToConstant(new ApplicationPageViewModel());
            Kernel.Bind<Players>().ToSelf().InSingletonScope();
            Kernel.Bind<ObservableCollection<Player>>().ToSelf();
            Kernel.Bind<ProgressBarViewModel>().ToConstant(new ProgressBarViewModel());
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
