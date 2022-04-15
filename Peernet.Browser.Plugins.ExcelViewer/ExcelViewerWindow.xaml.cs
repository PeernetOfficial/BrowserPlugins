using Peernet.Browser.Plugins.ExcelViewer.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace Peernet.Browser.Plugins.ExcelViewer
{
    /// <summary>
    /// Interaction logic for TextViewerWindow.xaml
    /// </summary>
    public partial class ExcelViewerWindow : Window
    {
        public ExcelViewerWindow(ExcelViewerViewModel textViewerViewModel)
        {
            ContentRendered += Window_ContentRendered;
            Initialized += Window_Initialized;
            InitializeComponent();
            MouseDown += Window_MouseDown;
            DataContext = textViewerViewModel;
            Closed += PDFViewerWindow_Closed;
        }

        private void PDFViewerWindow_Closed(object sender, EventArgs e)
        {
            ((ExcelViewerViewModel)DataContext).FileStream?.Dispose();
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
