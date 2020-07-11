using R6API;
using System.Collections.ObjectModel;

namespace R6Stats
{
    class Players
    {
       public ObservableCollection<Player> PlayersCollection { get; set; }

        public Players(ObservableCollection<Player> players)
        {
            PlayersCollection = players;
        }
    }
}
