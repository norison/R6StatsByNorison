namespace R6Stats
{
    class ApplicationPageLocator
    {
        public static ApplicationPageLocator Instance { get; private set; } = new ApplicationPageLocator();

        public static ApplicationPageViewModel ApplicationPageViewModel => IoC.ApplicationPage;
    }
}
