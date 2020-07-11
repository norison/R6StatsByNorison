namespace R6Stats
{
    class ViewModelLocator
    {
        public MainViewModel MainViewModel { get; }
        public ViewModelLocator() => MainViewModel = IoC.Get<MainViewModel>();
    }
}
