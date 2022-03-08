using Peernet.Browser.Plugins.MediaPlayer.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Peernet.Browser.Plugins.MediaPlayer
{
    /// <summary>
    /// Interaction logic for FileStreamWindow.xaml
    /// </summary>
    public partial class FileStreamWindow : Window
    {
        private bool _isOverBottomTool;
        private long _videoLength;
        
        public bool IsMuted
        {
            get { return (bool)GetValue(IsMutedProperty); }
            set { SetValue(IsMutedProperty, value); }
        }

        public static readonly DependencyProperty IsMutedProperty =
            DependencyProperty.Register("IsMuted", typeof(bool), typeof(FileStreamWindow), new PropertyMetadata(false));

        public FileStreamWindow(FileStreamViewModel fileStreamViewModel)
        {
            ContentRendered += Window_ContentRendered;
            Initialized += Window_Initialized;
            InitializeComponent();
            DataContext = fileStreamViewModel;
            MouseDown += Window_MouseDown;
            Preview.Loaded += PreviewOnLoaded;
            Unloaded += Preview_Unloaded;
            PART_MouseOver_Area.MouseEnter += PART_MouseOver_Area_MouseEnter;
            PART_MouseOver_Area.MouseLeave += PART_MouseOver_Area_MouseLeave;
            PART_Slider.DropValueChanged += PART_Slider_DropValueChanged;
        }

        private void PART_Slider_DropValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.SetVideoCurrentTime(e.NewValue, true);
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetVideoCurrentTime(double newPosition, bool isMoveToPoint = false)
        {
            if (this.PART_Time_Current != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.PART_Time_Current.Text = TimeSpan.FromMilliseconds(newPosition).ToString("hh\\:mm\\:ss");
                });
            }
            if (this.PART_Slider != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.PART_Slider.Value = newPosition;
                });
            }
            if (isMoveToPoint)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Preview.MediaPlayer.Time = (long)newPosition;
                });
            }
        }

        private void MediaPlayer_LengthChanged(object sender, LibVLCSharp.Shared.MediaPlayerLengthChangedEventArgs e)
        {
            this._videoLength = e.Length;
            this.SetVideoTotalTime(e.Length);
        }

        private void MediaPlayer_Paused(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.PART_Btn_Play.Visibility = Visibility.Visible;
                this.PART_Btn_Pause.Visibility = Visibility.Collapsed;
            });
        }

        private void MediaPlayer_Playing(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                this.PART_Btn_Play.Visibility = Visibility.Collapsed;
                this.PART_Btn_Pause.Visibility = Visibility.Visible;
                this.PART_Video_Time.Visibility = Visibility.Visible;
                this.PART_Slider.Visibility = Visibility.Visible;
                this.PART_Btn_Stop.IsEnabled = true;

                VisualStateManager.GoToState(this, "HideVideoTool", true);
            });
        }

        private void MediaPlayer_PositionChanged(object sender, LibVLCSharp.Shared.MediaPlayerPositionChangedEventArgs e)
        {
            this.SetVideoCurrentTime(e.Position * _videoLength);
        }

        private void MediaPlayer_Stopped(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                this.PART_Btn_Play.Visibility = Visibility.Visible;
                this.PART_Btn_Pause.Visibility = Visibility.Collapsed;
                this.PART_Video_Time.Visibility = Visibility.Collapsed;
                this.PART_Slider.Visibility = Visibility.Collapsed;

                this.PART_Btn_Stop.IsEnabled = false;

                VisualStateManager.GoToState(this, "ShowVideoTool", true);
            });
        }

        private void PART_MouseOver_Area_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Preview.MediaPlayer.IsPlaying)
            {
                _isOverBottomTool = true;
                VisualStateManager.GoToState(this, "ShowVideoTool", true);
            }
        }

        private void PART_MouseOver_Area_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Preview.MediaPlayer.IsPlaying)
            {
                _isOverBottomTool = false;
                Task.Delay(1000).ContinueWith((t) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (!this._isOverBottomTool)
                        {
                            VisualStateManager.GoToState(this, "HideVideoTool", true);
                        }
                    });
                });
            }
        }

        private void PART_Volume_Slider_DropValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.Preview.MediaPlayer.Volume = (int)(e.NewValue * 100 / PART_Volume_Slider.Maximum);
        }

        private void PauseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Preview.MediaPlayer.Pause();
        }

        private void PlayButton_OnClick(object sender, RoutedEventArgs e)
        {
            Preview.MediaPlayer.Play();
        }

        private void Preview_Unloaded(object sender, RoutedEventArgs e)
        {
            Preview.MediaPlayer.Stop();
            Preview.MediaPlayer.Dispose();
        }

        private void PreviewOnLoaded(object sender, RoutedEventArgs e)
        {
            Preview.MediaPlayer = ((FileStreamViewModel)DataContext).MediaPlayer;
            SetInitialVolume();
            SubscribeToEvents();
            Preview.MediaPlayer.Play();
        }

        private void SetInitialVolume()
        {
            var initialValue = 50;
            SetVolumeInPercentage(initialValue);
        }

        private void SetVideoTotalTime(long newLength)
        {
            if (this.PART_Time_Total != null)
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    this.PART_Time_Total.Text = TimeSpan.FromMilliseconds(newLength).ToString("hh\\:mm\\:ss");
                });
            }
            if (this.PART_Slider != null)
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    this.PART_Slider.Maximum = newLength;
                });
            }
        }

        private void StopButton_OnClick(object sender, RoutedEventArgs e)
        {
            Preview.MediaPlayer.Stop();
        }

        private void SubscribeToEvents()
        {
            Preview.MediaPlayer.Playing += MediaPlayer_Playing;
            Preview.MediaPlayer.Paused += MediaPlayer_Paused;
            Preview.MediaPlayer.Stopped += MediaPlayer_Stopped;
            Preview.MediaPlayer.LengthChanged += MediaPlayer_LengthChanged;
            Preview.MediaPlayer.PositionChanged += MediaPlayer_PositionChanged;
            Preview.MediaPlayer.EndReached += MediaPlayer_EndReached;
        }

        private void MediaPlayer_EndReached(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Preview.MediaPlayer.Stop();
            });
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

        private void PART_Btn_Volume_Click(object sender, RoutedEventArgs e)
        {
            Preview.MediaPlayer.ToggleMute();
            IsMuted = Preview.MediaPlayer.Mute;
        }

        private void SetVolumeInPercentage(int percentage)
        {
            this.PART_Volume_Slider.Value = (PART_Volume_Slider.Maximum * percentage) / 100;
        }
    }
}