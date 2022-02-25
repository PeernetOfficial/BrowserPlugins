using LibVLCSharp.Shared;
using LibVLCSharp.WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Peernet.Browser.Plugins.MediaPlayer.Controls
{
    public class VideoPlayer : Control
    {
        private const int InitVolume = 200;

        private VideoView PART_VideView;

        private Button PART_Btn_Play;

        private Button PART_Btn_Pause;

        private Button PART_Btn_Stop;

        private Button PART_Btn_Next;

        private Button PART_Btn_Previous;

        private Run PART_Time_Current;

        private Run PART_Time_Total;

        private TextBlock PART_Video_Time;

        private Border PART_Bottom_Tool;

        private VideoSlider PART_Slider;

        private Button PART_Btn_Slower;

        private Button PART_Btn_Faster;

        private Border PART_PlayInfo;

        private Button PART_Btn_OpenFile;

        private Border PART_MouseOver_Area;

        private Slider PART_Volume_Slider;


        private long _videoLength;

        private long _currentPosition;
        private bool _isOverBottomTool;


        public LibVLCSharp.Shared.MediaPlayer MediaPlayer
        {
            get => (LibVLCSharp.Shared.MediaPlayer)GetValue(MediaPlayerProperty);
            set { SetValue(MediaPlayerProperty, value); }
        }

        public static readonly DependencyProperty MediaPlayerProperty =
            DependencyProperty.Register("MediaPlayer", typeof(LibVLCSharp.Shared.MediaPlayer), typeof(VideoPlayer), new PropertyMetadata(null));

        public VLCState VideoState
        {
            get
            {
                if (this.VlcIsNotNull())
                {
                    return this.PART_VideView.MediaPlayer.State;
                }
                return VLCState.Stopped;
            }
        }

        private bool _isVlcControlLoading;
        public bool IsVlcControlLoading
        {
            get { return _isVlcControlLoading; }
            set { _isVlcControlLoading = value; }
        }

        private bool _isVideoLoading;
        public bool IsVideoLoading
        {
            get { return _isVideoLoading; }
            set { _isVideoLoading = value; }
        }

        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            private set { SetValue(IsPlayingProperty, value); }
        }

        public static readonly DependencyProperty IsPlayingProperty =
            DependencyProperty.Register("IsPlaying", typeof(bool), typeof(VideoPlayer), new PropertyMetadata(false));


        //public ObservableCollection<VideoInfo> PlayList
        //{
        //    get { return (ObservableCollection<VideoInfo>)GetValue(PlayListProperty); }
        //    set { SetValue(PlayListProperty, value); }
        //}

        //public static readonly DependencyProperty PlayListProperty =
        //    DependencyProperty.Register("PlayList", typeof(ObservableCollection<VideoInfo>), typeof(VideoPlayer), new PropertyMetadata(null));

        static VideoPlayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VideoPlayer), new FrameworkPropertyMetadata(typeof(VideoPlayer)));
        }

        public override void OnApplyTemplate()
        {
            this.UnRegisterEvent();

            base.OnApplyTemplate();
            
            this.PART_VideView = this.GetTemplateChild("PART_VideoView") as VideoView;
            if (this.PART_VideView != null)
            {
                var vlcLibDirectory = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

                var options = new string[]
                {
                    // VLC options can be given here. Please refer to the VLC command line documentation.
                };

                Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.IsVlcControlLoading = true;
                    });
                    this.PART_VideView.MediaPlayer = MediaPlayer;
                }).ContinueWith((t) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.IsVlcControlLoading = false;
                        this.RegisterEvent();
                        if (this.PART_Volume_Slider != null)
                        {
                            this.PART_Volume_Slider.Maximum = InitVolume;
                            this.PART_Volume_Slider.Minimum = 0;
                        }
                    });
                });
            }

            this.PART_Btn_Play = this.GetTemplateChild("PART_Btn_Play") as Button;
            this.PART_Btn_Pause = this.GetTemplateChild("PART_Btn_Pause") as Button;
            this.PART_Btn_Stop = this.GetTemplateChild("PART_Btn_Stop") as Button;
            this.PART_Btn_Next = this.GetTemplateChild("PART_Btn_Next") as Button;
            this.PART_Btn_Previous = this.GetTemplateChild("PART_Btn_Previous") as Button;
            this.PART_Time_Current = this.GetTemplateChild("PART_Time_Current") as Run;
            this.PART_Time_Total = this.GetTemplateChild("PART_Time_Total") as Run;
            this.PART_Video_Time = this.GetTemplateChild("PART_Video_Time") as TextBlock;
            this.PART_Bottom_Tool = this.GetTemplateChild("PART_Bottom_Tool") as Border;
            this.PART_Slider = this.GetTemplateChild("PART_Slider") as VideoSlider;
            this.PART_Btn_Slower = this.GetTemplateChild("PART_Btn_Slower") as Button;
            this.PART_Btn_Faster = this.GetTemplateChild("PART_Btn_Faster") as Button;
            this.PART_MouseOver_Area = this.GetTemplateChild("PART_MouseOver_Area") as Border;
            this.PART_Volume_Slider = this.GetTemplateChild("PART_Volume_Slider") as Slider;
            this.PART_PlayInfo = this.GetTemplateChild("PART_PlayInfo") as Border;
            this.PART_Btn_OpenFile = this.GetTemplateChild("PART_Btn_OpenFile") as Button;
            this.VideoStopped();
        }

        /// <summary>
        /// 注册
        /// </summary>
        private void RegisterEvent()
        {
            if (this.VlcIsNotNull())
            {
                this.GetVlcMediaPlayer().LengthChanged += VideoPlayer_LengthChanged; ;
                this.GetVlcMediaPlayer().PositionChanged += MediaPlayer_PositionChanged;
                this.GetVlcMediaPlayer().Playing += VideoPlayer_Playing; ;
                this.GetVlcMediaPlayer().Paused += VideoPlayer_Paused; ;
                this.GetVlcMediaPlayer().Stopped += VideoPlayer_Stopped; ;
            }

            if (this.PART_Btn_Play != null)
            {
                this.PART_Btn_Play.Click += PART_Btn_Play_Click;
            }

            if (this.PART_Btn_Pause != null)
            {
                this.PART_Btn_Pause.Click += PART_Btn_Pause_Click;
            }

            if (this.PART_Btn_Stop != null)
            {
                this.PART_Btn_Stop.Click += PART_Btn_Stop_Click;
            }

            if (this.PART_Slider != null)
            {
                this.PART_Slider.DropValueChanged += PART_Slider_DropValueChanged;
            }

            if (this.PART_MouseOver_Area != null)
            {
                //this.PART_Bottom_Tool.AddHandler(Control.MouseEnterEvent, new MouseEventHandler(PART_MouseOver_Area_MouseEnter));
                //this.PART_Bottom_Tool.AddHandler(Control.MouseLeaveEvent, new MouseEventHandler(PART_MouseOver_Area_MouseLeave));
                this.PART_MouseOver_Area.MouseEnter += PART_MouseOver_Area_MouseEnter;
                this.PART_MouseOver_Area.MouseLeave += PART_MouseOver_Area_MouseLeave;
            }

            if (this.PART_Btn_Slower != null)
            {
                //this.PART_Btn_Slower.MouseLeftButtonDown += PART_Btn_Slower_MouseLeftButtonDown;
                //this.PART_Btn_Slower.MouseLeftButtonUp += PART_Btn_Slower_MouseLeftButtonUp;
                this.PART_Btn_Slower.Click += PART_Btn_Slower_Click;
            }

            if (this.PART_Btn_Faster != null)
            {
                //this.PART_Btn_Faster.MouseLeftButtonDown += PART_Btn_Faster_MouseLeftButtonDown;
                //this.PART_Btn_Faster.MouseLeftButtonUp += PART_Btn_Faster_MouseLeftButtonUp;
                this.PART_Btn_Faster.Click += PART_Btn_Faster_Click;
            }

            if (this.PART_Btn_OpenFile != null)
            {
                this.PART_Btn_OpenFile.Click += PART_Btn_OpenFile_Click;
            }
        }

        private void VideoPlayer_Paused(object sender, EventArgs e)
        {
            VideoPaused();
        }

        private void VideoPlayer_Stopped(object sender, EventArgs e)
        {
            VideoStopped();
        }

        private void VideoPlayer_Playing(object sender, EventArgs e)
        {
            VideoIsPlaying();
        }

        private void VideoPlayer_LengthChanged(object sender, MediaPlayerLengthChangedEventArgs e)
        {
            _videoLength = e.Length;
            SetVideoTotalTime(e.Length);
        }

        private void PART_Btn_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (PART_PlayInfo != null)
            {
                if (PART_PlayInfo.Visibility == Visibility.Collapsed)
                {
                    PART_PlayInfo.Visibility = Visibility.Visible;
                }
                else
                {
                    PART_PlayInfo.Visibility = Visibility.Collapsed;
                }
            }

        }

        private void PART_Btn_Slower_Click(object sender, RoutedEventArgs e)
        {
            this.GetVlcMediaPlayer().Time = this.GetVlcMediaPlayer().Time >= 2000 ? this.GetVlcMediaPlayer().Time - 2000 : 0;
        }

        private void PART_Btn_Faster_Click(object sender, RoutedEventArgs e)
        {
            this.GetVlcMediaPlayer().Time = this.GetVlcMediaPlayer().Time + 2000 <= this._videoLength ? this.GetVlcMediaPlayer().Time + 2000 : this._videoLength;
        }

        private void PART_Btn_Faster_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.GetVlcMediaPlayer().SetRate(2);
        }

        private void UnRegisterEvent()
        {
            if (this.PART_Btn_Play != null)
            {
                this.PART_Btn_Play.Click -= PART_Btn_Play_Click;
            }

            if (this.PART_Btn_Pause != null)
            {
                this.PART_Btn_Pause.Click -= PART_Btn_Pause_Click;
            }

            if (this.PART_Btn_Stop != null)
            {
                this.PART_Btn_Stop.Click -= PART_Btn_Stop_Click;
            }
        }

        private void PART_Btn_Play_Click(object sender, RoutedEventArgs e)
        {
            if (!this.VlcIsNotNull()) return;

            if (this.GetVlcMediaPlayer().State == VLCState.Paused)
            {
                this.GetVlcMediaPlayer().Play();
            }
            else if (this.GetVlcMediaPlayer().State == VLCState.Stopped || this.GetVlcMediaPlayer().State == VLCState.NothingSpecial)
            {
                //this.PART_VideoView.SourceProvider.MediaPlayer.Play(new FileInfo(@"D:\迅雷下载\复仇者联盟4：终局之战.mp4"));
            }
        }

        private void PART_Btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            this.PART_VideView.MediaPlayer.Pause();
        }

        private void PART_Btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                this.PART_VideView.MediaPlayer.Stop();
            });
        }

        private void MediaPlayer_PositionChanged(object sender, MediaPlayerPositionChangedEventArgs e)
        {
            SetVideoCurrentTime(e.Position * _videoLength);
        }

        private void PART_Slider_DropValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //if (this.VlcIsNotNull())
            //{
            //    this.GetVlcMediaPlayer().Pause();
            //}
            this.SetVideoCurrentTime(e.NewValue, true);
        }

        private void PART_MouseOver_Area_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IsPlaying)
            {
                _isOverBottomTool = true;
                VisualStateManager.GoToState(this, "ShowVideoTool", true);
            }
        }

        private void PART_MouseOver_Area_MouseLeave(object sender, MouseEventArgs e)
        {
            if (IsPlaying)
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

        private bool VlcIsNotNull()
        {
            return this.PART_VideView != null && this.PART_VideView.MediaPlayer != null;
        }

        private LibVLCSharp.Shared.MediaPlayer GetVlcMediaPlayer()
        {
            return this.PART_VideView.MediaPlayer;
        }

        private void SetVideoTotalTime(long newLength)
        {
            if (this.PART_Time_Total != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.PART_Time_Total.Text = TimeSpan.FromMilliseconds(newLength).ToString("hh\\:mm\\:ss");
                });
            }
            if (this.PART_Slider != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.PART_Slider.Maximum = newLength;
                });
            }
        }

        private void SetVideoCurrentTime(double newPosition, bool isMoveToPoint = false)
        {
            this._currentPosition = (long)newPosition;
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
            if (this.VlcIsNotNull() && isMoveToPoint)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.GetVlcMediaPlayer().Time = (long)newPosition;
                });
            }
        }

        private void VideoIsPlaying()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.IsPlaying = true;
                this.PART_Btn_Play.Visibility = Visibility.Collapsed;
                this.PART_Btn_Pause.Visibility = Visibility.Visible;
                this.PART_Video_Time.Visibility = Visibility.Visible;
                this.PART_Btn_Slower.Visibility = Visibility.Visible;
                this.PART_Btn_Faster.Visibility = Visibility.Visible;
                this.PART_Slider.Visibility = Visibility.Visible;

                this.PART_Btn_Stop.IsEnabled = true;
                this.PART_Btn_Next.IsEnabled = true;
                this.PART_Btn_Previous.IsEnabled = true;

                VisualStateManager.GoToState(this, "HideVideoTool", true);
            });
        }

        private void VideoPaused()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.PART_Btn_Play.Visibility = Visibility.Visible;
                this.PART_Btn_Pause.Visibility = Visibility.Collapsed;
            });
        }

        private void VideoStopped()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.IsPlaying = false;

                this.PART_Btn_Play.Visibility = Visibility.Visible;
                this.PART_Btn_Pause.Visibility = Visibility.Collapsed;
                this.PART_Video_Time.Visibility = Visibility.Collapsed;
                this.PART_Btn_Slower.Visibility = Visibility.Collapsed;
                this.PART_Btn_Faster.Visibility = Visibility.Collapsed;
                this.PART_Slider.Visibility = Visibility.Collapsed;

                this.PART_Btn_Stop.IsEnabled = false;
                this.PART_Btn_Next.IsEnabled = false;
                this.PART_Btn_Previous.IsEnabled = false;

                VisualStateManager.GoToState(this, "ShowVideoTool", true);
            });
        }

        private bool IsVideoStoped()
        {
            if (this.VlcIsNotNull())
            {
                return this.GetVlcMediaPlayer().State == VLCState.Stopped;
            }
            return true;
        }

        public void Play()
        {
            this.PART_Btn_Play_Click(this, new RoutedEventArgs());
        }

        public void Stop()
        {
            this.PART_Btn_Stop_Click(this, new RoutedEventArgs());
        }

        public void Pause()
        {
            this.PART_Btn_Pause_Click(this, new RoutedEventArgs());
        }
    }
}
