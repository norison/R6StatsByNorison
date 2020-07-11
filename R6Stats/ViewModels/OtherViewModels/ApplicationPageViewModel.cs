using System.Windows.Controls;

namespace R6Stats
{
    class ApplicationPageViewModel : BaseViewModel
    {
        private Page currentPage;
        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                Notify();
            }
        }
    }
}
