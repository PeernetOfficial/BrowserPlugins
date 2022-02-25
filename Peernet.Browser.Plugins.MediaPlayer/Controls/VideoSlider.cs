using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Peernet.Browser.Plugins.MediaPlayer.Controls
{
    public class VideoSlider : Slider
    {
        public static readonly DependencyProperty DecreaseColorProperty =
            DependencyProperty.Register("DecreaseColor", typeof(SolidColorBrush), typeof(VideoSlider));

        public static readonly RoutedEvent DropValueChangedEvent = EventManager.RegisterRoutedEvent("DropValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(VideoSlider));

        public static readonly DependencyProperty IncreaseColorProperty =
            DependencyProperty.Register("IncreaseColor", typeof(SolidColorBrush), typeof(VideoSlider));

        // Using a DependencyProperty as the backing store for IsVideoVisibleWhenPressThumb.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsVideoVisibleWhenPressThumbProperty =
            DependencyProperty.Register("IsVideoVisibleWhenPressThumb", typeof(bool), typeof(VideoSlider), new PropertyMetadata(false));

        private bool _thumbIsPressed;
        private Thumb PART_Thumb;
        private Track PART_Track;

        static VideoSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VideoSlider), new FrameworkPropertyMetadata(typeof(VideoSlider)));
        }

        public VideoSlider()
        {
            // Refresh(this);
        }

        public event RoutedPropertyChangedEventHandler<double> DropValueChanged
        {
            add
            {
                this.AddHandler(DropValueChangedEvent, value);
            }
            remove
            {
                this.RemoveHandler(DropValueChangedEvent, value);
            }
        }

        public SolidColorBrush DecreaseColor
        {
            get { return (SolidColorBrush)GetValue(DecreaseColorProperty); }
            set { SetValue(DecreaseColorProperty, value); }
        }

        public SolidColorBrush IncreaseColor
        {
            get { return (SolidColorBrush)GetValue(IncreaseColorProperty); }
            set { SetValue(IncreaseColorProperty, value); }
        }

        public bool IsVideoVisibleWhenPressThumb
        {
            get { return (bool)GetValue(IsVideoVisibleWhenPressThumbProperty); }
            set { SetValue(IsVideoVisibleWhenPressThumbProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Thumb = GetTemplateChild("PART_Thumb") as Thumb;
            PART_Track = GetTemplateChild("PART_Track") as Track;
            if (PART_Thumb != null)
            {
                PART_Thumb.PreviewMouseLeftButtonDown += PART_Thumb_PreviewMouseLeftButtonDown;
                PART_Thumb.PreviewMouseLeftButtonUp += PART_Thumb_PreviewMouseLeftButtonUp;
            }
            if (PART_Track != null)
            {
                PART_Track.MouseLeftButtonDown += PART_Track_MouseLeftButtonDown;
                PART_Track.MouseLeftButtonUp += PART_Track_MouseLeftButtonUp;
            }
            ValueChanged += AduFlatSilder_ValueChanged;
        }

        public virtual void OnDropValueChanged(double oldValue, double newValue)
        {
            RoutedPropertyChangedEventArgs<double> arg = new RoutedPropertyChangedEventArgs<double>(oldValue, newValue, DropValueChangedEvent);
            this.RaiseEvent(arg);
        }

        private void AduFlatSilder_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsVideoVisibleWhenPressThumb && _thumbIsPressed)
            {
                OnDropValueChanged(Value, Value);
            }
        }

        private void PART_Thumb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _thumbIsPressed = IsVideoVisibleWhenPressThumb && true;
        }

        private void PART_Thumb_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (IsVideoVisibleWhenPressThumb)
            {
                return;
            }

            OnDropValueChanged(Value, Value);
        }

        private void PART_Track_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _thumbIsPressed = IsVideoVisibleWhenPressThumb && true;
        }

        private void PART_Track_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (IsVideoVisibleWhenPressThumb)
            {
                return;
            }

            OnDropValueChanged(Value, Value);
        }
    }
}