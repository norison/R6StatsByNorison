using R6API;
using System.Collections.Generic;

namespace R6Stats
{
    class WeaponsViewModel : BaseViewModel, IMessage<List<Weapon>>
    {
        private List<Weapon> weapons;

        public List<Weapon> Weapons
        {
            get => weapons;
            set
            {
                weapons = value;
                Notify();
            }
        }

        public void SendMessage(List<Weapon> message)
        {
            Weapons = message ?? new List<Weapon>();
        }
    }
}
