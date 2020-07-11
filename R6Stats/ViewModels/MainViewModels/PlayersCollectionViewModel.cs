using R6API;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace R6Stats
{
    class PlayersCollectionViewModel : BaseViewModel
    {
        #region PrivateFields
        private ObservableCollection<Player> players;
        private Player moreInfoPlayer;
        private bool isDialogOpen;
        private string searchText;
        #endregion

        #region Constructor
        public PlayersCollectionViewModel()
        {
            Players = IoC.Get<Players>().PlayersCollection;
            PlayersView = CollectionViewSource.GetDefaultView(Players);
            PlayersView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            PlayersView.Filter = PlayersFilter;

            InitCommands();
        }
        #endregion

        #region Commands
        public ICommand AddPlayerCommand { get; set; }
        public ICommand ShowStatsCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand SortCommand { get; set; }
        public ICommand MoreInfoCommand { get; set; }
        #endregion

        #region Properties
        public ICollectionView PlayersView { get; set; }
        public ObservableCollection<Player> Players
        {
            get => players;
            set
            {
                players = value;
                Notify();
            }
        }
        public Player MoreInfoPlayer
        {
            get => moreInfoPlayer;
            set
            {
                moreInfoPlayer = value;
                Notify();
            }
        }
        public bool IsDialogOpen
        {
            get => isDialogOpen;
            set
            {
                isDialogOpen = value;
                Notify();
            }
        }
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                PlayersView.Refresh();
                Notify();
            }
        }
        #endregion

        #region Methods
        private void InitCommands()
        {
            AddPlayerCommand = new RelayCommand(o => AddPlayer(), o => !IoC.ProgressBar.IsIndeterminate);
            ShowStatsCommand = new RelayCommand(o => ShowStats(o), o => !IoC.ProgressBar.IsIndeterminate);
            RemoveCommand = new RelayCommand(o => Remove(o), o => !IoC.ProgressBar.IsIndeterminate);
            MoreInfoCommand = new RelayCommand(o => MoreInfo(o), o => !IoC.ProgressBar.IsIndeterminate);
            SortCommand = new RelayCommand(o => Sort(o));
        }

        private void Remove(object username)
        {
            Players.Remove(FindPlayer(username.ToString()));
        }

        private void Sort(object sortPropertyName)
        {
            PlayersView.SortDescriptions.Clear();
            PlayersView.SortDescriptions.Add(new SortDescription(sortPropertyName.ToString(), ListSortDirection.Ascending));
        }

        private void MoreInfo(object player)
        {
            MoreInfoPlayer = FindPlayer(player.ToString());
            IsDialogOpen = true;
        }

        private void EnableProgressBar()
        {
            IoC.ProgressBar.Visibility = Visibility.Visible;
            IoC.ProgressBar.IsIndeterminate = true;
        }

        private void DisableProgressBar()
        {
            IoC.ProgressBar.Visibility = Visibility.Hidden;
            IoC.ProgressBar.IsIndeterminate = false;
        }

        private Player FindPlayer(string username)
        {
            return Players.Where(p => p.Name == username.ToString()).FirstOrDefault();
        }

        private void AddPlayer()
        {
            SearchView search = new SearchView();
            search.ShowDialog();
        }

        private async void ShowStats(object o)
        {
            var player = FindPlayer(o.ToString());

            try
            {
                if (player.Stats is null || player.Operators is null || player.Seasons is null || player.Weapons is null)
                {
                    EnableProgressBar();
                    await player.LoadAllPlayerStatsAsync();
                }
                IoC.ApplicationPage.CurrentPage = ApplicationPage.PlayerView.GetPage(player);
            }
            catch (Exception ex)
            {
                ErrorBox.Show("Huy znaet 4to proizowlo", ex.Message);
            }
            finally
            {
                DisableProgressBar();
            }
        }

        private bool PlayersFilter(object o)
        {
            var player = o as Player;
            bool isContainsName = true;
            if (!string.IsNullOrWhiteSpace(SearchText) && !string.IsNullOrEmpty(SearchText))
            {
                isContainsName = player.Name.ToLower().Contains(SearchText.ToLower());
            }
            return isContainsName;
        }
        #endregion
    }
}
