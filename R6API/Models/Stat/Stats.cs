namespace R6API
{
    public class Stats
    {
        public CasualStats Casual { get; internal set; }
        public RankedStats Ranked { get; internal set; }
        public GeneralStats General { get; internal set; }
        public BombStats Bomb { get; internal set; }
        public SecureAreaStats SecureArea { get; internal set; }
        public HostageStats Hostage { get; internal set; }
    }
}
