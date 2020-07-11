using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace R6Stats
{
    /// <summary>
    /// Логика взаимодействия для OperatorsView.xaml
    /// </summary>
    public partial class OperatorsView : Page
    {
        public OperatorsView()
        {
            InitializeComponent();
            AtkListBox.PreviewMouseWheel += ListBox_PreviewMouseWheel;
            DefListBox.PreviewMouseWheel += ListBox_PreviewMouseWheel;
        }

        private void ListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is ListBox && !e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = MouseWheelEvent,
                    Source = sender
                };
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }
    }
}
