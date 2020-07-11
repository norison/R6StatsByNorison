using R6API;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace R6Stats
{
    class MainViewModel : BaseViewModel
    {
        #region Private Fields
        private readonly IDataService<ObservableCollection<Player>> playersService;
        #endregion

        #region Constructor
        public MainViewModel(IDataService<ObservableCollection<Player>> playersService)
        {
            this.playersService = playersService;
            InitCommands();
        }
        #endregion

        #region Commands
        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ChangeThemeCommand { get; set; }
        public ICommand ChangeLangCommand { get; set; }
        #endregion

        #region Properties
        #endregion

        #region Methods
        private void InitCommands()
        {
            MinimizeCommand = new RelayCommand(o => Minimize(o));
            MaximizeCommand = new RelayCommand(o => Maximize(o));
            ExitCommand = new RelayCommand(o => Close(o));
            LoadCommand = new RelayCommand(o => Load());
            SaveCommand = new RelayCommand(o => Save());
            ChangeThemeCommand = new RelayCommand(o => ChangeTheme(o));
            ChangeLangCommand = new RelayCommand(o => ChangeLang(o));
        }

        private void Minimize(object window) => (window as Window).WindowState = WindowState.Minimized;

        private void Maximize(object window)
        {
            var wnd = window as Window;
            wnd.WindowState = wnd.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void Close(object window) => (window as Window)?.Close();

        private void Load()
        {
            Settings.SetSettings();
            IoC.Get<Players>().PlayersCollection = playersService.Load();
            IoC.ApplicationPage.CurrentPage = ApplicationPage.PlayersCollectionView.GetPage();
        }

        private void Save()
        {
            playersService.Save(IoC.Get<Players>().PlayersCollection);
            Settings.SaveSettings();
        }

        private void ChangeTheme(object theme)
        {
            Settings.ChangeTheme(theme.ToString());
        }

        private void ChangeLang(object lang)
        {
            Settings.ChangeLanguage(lang.ToString());
        }
        #endregion
    }
}
