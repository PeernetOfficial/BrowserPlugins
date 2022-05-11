using Peernet.Browser.Plugins.TextViewer.ViewModels;
using Peernet.SDK.WPF;
using System;
using System.Windows.Input;

namespace Peernet.Browser.Plugins.TextViewer
{
    /// <summary>
    /// Interaction logic for TextViewerWindow.xaml
    /// </summary>
    public partial class TextViewerWindow : PeernetWindow
    {
        public TextViewerWindow(TextViewerViewModel textViewerViewModel)
        {
            ContentRendered += Window_ContentRendered;
            Initialized += Window_Initialized;
            InitializeComponent();
            MouseDown += Window_MouseDown;
            DataContext = textViewerViewModel;
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
