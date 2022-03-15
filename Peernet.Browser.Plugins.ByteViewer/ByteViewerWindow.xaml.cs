using Peernet.Browser.Plugins.ImageViewer.ViewModels;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace Peernet.Browser.Plugins.ImageViewer
{
    /// <summary>
    /// Interaction logic for TextViewerWindow.xaml
    /// </summary>
    public partial class ByteViewerWindow : Window
    {
        public ByteViewerWindow(ByteViewerViewModel imageViewerViewModel)
        {
            ContentRendered += Window_ContentRendered;
            Initialized += Window_Initialized;
            InitializeComponent();
            MouseDown += Window_MouseDown;
            DataContext = imageViewerViewModel;
            Closed += ByteViewerWindow_Closed;

            // It is just to make sure WpfHexaEditor assembly got loaded
            _ = WpfHexaEditor.Properties.Resources.EBCDIC;
        }

        private void ByteViewerWindow_Closed(object sender, EventArgs e)
        {
            ((ByteViewerViewModel)DataContext).FileStream?.Dispose();
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