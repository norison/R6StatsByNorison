namespace R6Stats
{
    class ProgressBarLocator
    {
        public static ProgressBarLocator Instance { get; private set; } = new ProgressBarLocator();

        public static ProgressBarViewModel ProgressBarViewModel => IoC.ProgressBar;
    }
}
