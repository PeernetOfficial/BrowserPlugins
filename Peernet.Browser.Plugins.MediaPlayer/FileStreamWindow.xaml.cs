using Peernet.Browser.Plugins.MediaPlayer.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace Peernet.Browser.Plugins.MediaPlayer
{
    /// <summary>
    /// Interaction logic for FileStreamWindow.xaml
    /// </summary>
    public partial class FileStreamWindow : Window
    {
        public FileStreamWindow(FileStreamViewModel fileStreamViewModel)
        {
            ContentRendered += Window_ContentRendered;
            Initialized += Window_Initialized;
            InitializeComponent();
            DataContext = fileStreamViewModel;
            MouseDown += Window_MouseDown;
            //  WindowStartupLocation = App.Current.MainWindow.WindowStartupLocation;
            //Preview.Loaded += PreviewOnLoaded;
            Unloaded += Preview_Unloaded;
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PauseButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Preview.MediaPlayer.Pause();
        }

        private void PlayButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Preview.MediaPlayer.Play();
        }

        private void Preview_Unloaded(object sender, RoutedEventArgs e)
        {
            //Preview.MediaPlayer.Stop();
            //Preview.MediaPlayer.Dispose();
        }

        private void PreviewOnLoaded(object sender, RoutedEventArgs e)
        {
            //Preview.MediaPlayer = ((FileStreamViewModel)DataContext).MediaPlayer;
            //Preview.MediaPlayer.Play();
        }

        private void StopButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Preview.MediaPlayer.Stop();
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