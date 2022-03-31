using Peernet.Browser.Plugins.HTMLViewer.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace Peernet.Browser.Plugins.HTMLViewer
{
    /// <summary>
    /// Interaction logic for TextViewerWindow.xaml
    /// </summary>
    public partial class HTMLViewerWindow : Window
    {
        public HTMLViewerWindow(HTMLViewerViewModel viewModel)
        {
            ContentRendered += Window_ContentRendered;
            Initialized += Window_Initialized;
            InitializeComponent();
            MouseDown += Window_MouseDown;
            DataContext = viewModel;
            webBrowser.NavigateToString(viewModel.Content);
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Topmost = false;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Topmost = true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}