using R6API;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static R6API.Enums;

namespace R6Stats
{
    class SearchViewModel : BaseViewModel
    {
        #region Private Fields
        private Visibility visibility;
        private bool isIndeterminate;
        #endregion

        #region Constructor
        public SearchViewModel()
        {
            DisableProgressBar();
            InitCommands();
        }
        #endregion

        #region Commands
        public ICommand ExitCommand { get; set; }
        public ICommand PlatformCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        #endregion

        #region Properties
        public string Username { get; set; } = "";
        public Platform Platform { get; set; } = Platform.UPLAY;

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                Notify();
            }
        }
        public bool IsIndeterminate
        {
            get => isIndeterminate;
            set
            {
                isIndeterminate = value;
                Notify();
            }
        }
        #endregion

        #region Methods
        private void InitCommands()
        {
            ExitCommand = new RelayCommand(o => Exit(o));
            PlatformCommand = new RelayCommand(o => Platform = (Platform)o);
            SearchCommand = new RelayCommand(o => Search(o), o => IsIndeterminate != true && Username != "");
        }

        private void Exit(object window) => (window as Window)?.Close();

        private async void Search(object o)
        {
            try
            {
                EnableProgressBar();

                var player = await API.GetAPI().GetPlayer(Username, Platform);

                var players = IoC.Get<Players>().PlayersCollection;

                if (players.Where(p => p.Name == player.Name).FirstOrDefault() is null)
                    players.Add(player);
                else
                    throw new Exception("Player already added!");

                (o as Window)?.Close();
            }
            catch (Exception ex)
            {
                ErrorBox.Show("Error", ex.Message);
            }
            finally
            {
                DisableProgressBar();
            }
        }

        private void EnableProgressBar()
        {
            IsIndeterminate = true;
            Visibility = Visibility.Visible;
        }

        private void DisableProgressBar()
        {
            IsIndeterminate = false;
            Visibility = Visibility.Hidden;
        }
        #endregion
    }
}
