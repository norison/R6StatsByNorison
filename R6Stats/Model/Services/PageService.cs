using System;
using System.Windows.Controls;

namespace R6Stats
{
    static class PageService
    {
        public static Page GetPage(this ApplicationPage page)
        {
            var view = GetView(page);
            var viewModel = GetViewModel(page);
            view.DataContext = viewModel;
            return view;
        }

        public static Page GetPage<T>(this ApplicationPage page, T message)
        {
            var view = GetView(page);
            var viewModel = GetViewModel(page);
            (viewModel as IMessage<T>)?.SendMessage(message);
            view.DataContext = viewModel;
            return view;
        }

        public static void ResendMessage<T>(this Page page, T message)
        {
            (page.DataContext as IMessage<T>)?.SendMessage(message);
        }

        private static Page GetView(ApplicationPage page)
        {
            Type viewType = Type.GetType($"R6Stats.{page.ToString()}");
            return Activator.CreateInstance(viewType) as Page;
        }

        private static BaseViewModel GetViewModel(ApplicationPage page)
        {
            Type viewModelType = Type.GetType($"R6Stats.{page.ToString()}Model");
            return Activator.CreateInstance(viewModelType) as BaseViewModel;
        }
    }
}
