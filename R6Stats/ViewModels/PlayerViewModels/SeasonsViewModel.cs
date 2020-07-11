using R6API;
using System.Collections.Generic;

namespace R6Stats
{
    class SeasonsViewModel : BaseViewModel, IMessage<List<Season>>
    {
        private List<Season> seasons;

        public List<Season> Seasons
        {
            get => seasons;
            set
            {
                seasons = value;
                Notify();
            }
        }

        public void SendMessage(List<Season> message)
        {
            Seasons = message as List<Season> ?? new List<Season>();
        }
    }
}
