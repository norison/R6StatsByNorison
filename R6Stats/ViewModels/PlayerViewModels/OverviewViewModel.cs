using R6API;
using System;
using System.Windows.Threading;

namespace R6Stats
{
    class OverviewViewModel : BaseViewModel, IMessage<Stats>
    {
        #region Private Fields
        private Stats stats;
        #endregion

        #region Properties
        public Stats Stats
        {
            get => stats;
            set
            {
                stats = value;
                Notify();
            }
        }
        #endregion

        #region Methods
        public void SendMessage(Stats message)
        {
            Stats = message ?? new Stats();
        }
        #endregion

    }
}
