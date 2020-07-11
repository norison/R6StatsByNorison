namespace R6Stats
{
    static class ErrorBox
    {
        public static void Show(string title, string message)
        {
            ErrorViewModel errorViewModel = new ErrorViewModel(title, message);

            ErrorView errorView = new ErrorView
            {
                DataContext = errorViewModel
            };

            errorView.ShowDialog();
        }
    }
}
