using System.Windows;

namespace R6Stats
{
    class ProgressBarViewModel : BaseViewModel
    {
        private Visibility visibility = Visibility.Hidden;
        private bool isIndeterminate = false;

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                Notify();
            }
        }
        public bool IsIndeterminate
        {
            get => isIndeterminate;
            set
            {
                isIndeterminate = value;
                Notify();
            }
        }
    }
}
