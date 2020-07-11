using System.Windows;
using System.Windows.Input;

namespace R6Stats
{
    class ErrorViewModel : BaseViewModel
    {
        private string title;
        private string message;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                Notify();
            }
        }
        public string Message
        {
            get => message;
            set
            {
                message = value;
                Notify();
            }
        }

        public ICommand CloseCommand { get; set; }

        public ErrorViewModel(string title, string message)
        {
            Title = title;
            Message = message;

            System.Media.SystemSounds.Hand.Play();
            CloseCommand = new RelayCommand(o => (o as Window)?.Close());
        }
    }
}
