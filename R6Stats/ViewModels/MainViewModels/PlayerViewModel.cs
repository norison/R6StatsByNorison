using R6API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace R6Stats
{
    class PlayerViewModel : BaseViewModel, IMessage<Player>
    {
        #region Private Fields
        private Player player;
        private Rank actualRank;
        private Page currentPage;
        private DispatcherTimer timer;
        private TimeSpan lastUpdate;
        #endregion

        #region Constructor
        public PlayerViewModel()
        {
            InitCommands();
        }
        #endregion

        #region Commands
        public ICommand BackCommand { get; set; }
        public ICommand OverviewCommand { get; set; }
        public ICommand OperatorsCommand { get; set; }
        public ICommand SeasonsCommand { get; set; }
        public ICommand WeaponsCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        #endregion

        #region Properties
        public Player Player
        {
            get => player;
            set
            {
                player = value;
                Notify();
            }
        }
        public Rank ActualRank
        {
            get => actualRank;
            set
            {
                actualRank = value;
                Notify();
            }
        }
        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                Notify();
            }
        }
        public TimeSpan LastUpdate
        {
            get => lastUpdate;
            set
            {
                lastUpdate = value;
                Notify();
            }
        }
        #endregion

        #region Methods
        private void InitCommands()
        {
            BackCommand = new RelayCommand(o => GoBack(o), o => !IoC.ProgressBar.IsIndeterminate);
            OverviewCommand = new RelayCommand(o => Overview(o), o => !IoC.ProgressBar.IsIndeterminate);
            OperatorsCommand = new RelayCommand(o => Operators(o), o => !IoC.ProgressBar.IsIndeterminate);
            SeasonsCommand = new RelayCommand(o => Seasons(o), o => !IoC.ProgressBar.IsIndeterminate);
            WeaponsCommand = new RelayCommand(o => Weapons(o), o => !IoC.ProgressBar.IsIndeterminate);
            UpdateCommand = new RelayCommand(o => Update(), o => !IoC.ProgressBar.IsIndeterminate);
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

        private void GoBack(object page)
        {
            (page as Page)?.NavigationService.GoBack();
        }

        private void Overview(object o = null)
        {
            CurrentPage = ApplicationPage.OverviewView.GetPage(Player.Stats);
            (o as Frame)?.RemoveBackEntry();
        }

        private void Seasons(object o)
        {
            CurrentPage = ApplicationPage.SeasonsView.GetPage(Player.Seasons);
            (o as Frame)?.RemoveBackEntry();
        }

        private void Operators(object o)
        {
            CurrentPage = ApplicationPage.OperatorsView.GetPage(Player.Operators);
            (o as Frame)?.RemoveBackEntry();
        }

        private void Weapons(object o)
        {
            CurrentPage = ApplicationPage.WeaponsView.GetPage(Player.Weapons);
            (o as Frame)?.RemoveBackEntry();
        }

        private Rank GetActualRank()
        {
            var lastSeason = Player.Seasons.Where(season => season.Id == Season.LatestSeason).FirstOrDefault();
            return lastSeason.Ranks.Aggregate((rank1, rank2) => rank1.MaxMMR > rank2.MaxMMR ? rank1 : rank2);
        }

        private void StartTimer()
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (sender, args) =>
            {
                LastUpdate = new TimeSpan(((DateTime.Now.Ticks - Player.UpdatedTime.Ticks)));
            };
            timer.Start();
        }

        private async void Update()
        {
            try
            {
                EnableProgressBar();
                await player.LoadAllPlayerStatsAsync();

                Player = Player;
                ActualRank = GetActualRank();
                CurrentPage.ResendMessage(Player.Stats);
                CurrentPage.ResendMessage(Player.Operators);
                CurrentPage.ResendMessage(Player.Seasons);
                CurrentPage.ResendMessage(Player.Weapons);
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

        public void SendMessage(Player message)
        {
            Player = message ?? new Player();

            Overview();
            ActualRank = GetActualRank();
            StartTimer();
        }
        #endregion
    }
}
