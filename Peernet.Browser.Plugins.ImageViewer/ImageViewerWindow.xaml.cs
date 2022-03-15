using Peernet.Browser.Plugins.ImageViewer.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Peernet.Browser.Plugins.ImageViewer
{
    /// <summary>
    /// Interaction logic for TextViewerWindow.xaml
    /// </summary>
    public partial class ImageViewerWindow : Window
    {
        public ImageViewerWindow(ImageViewerViewModel imageViewerViewModel)
        {
            ContentRendered += Window_ContentRendered;
            Initialized += Window_Initialized;
            InitializeComponent();
            MouseDown += Window_MouseDown;
            DataContext = imageViewerViewModel;
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