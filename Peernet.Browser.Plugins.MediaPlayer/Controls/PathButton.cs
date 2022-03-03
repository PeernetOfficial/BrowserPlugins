using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Peernet.Browser.Plugins.MediaPlayer.Controls
{
    public class PathButton : Button
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius"
              , typeof(CornerRadius), typeof(PathButton));

        public static readonly DependencyProperty DisabledPathColorProperty =
           DependencyProperty.Register("DisabledPathColor", typeof(Brush), typeof(PathButton));

        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register("MouseOverBackground"
            , typeof(Brush), typeof(PathButton));

        public static readonly DependencyProperty MouseOverPathColorProperty = DependencyProperty.Register("MouseOverPathColor"
            , typeof(Brush), typeof(PathButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(40, 139, 225))));

        public static readonly DependencyProperty NormalPathColorProperty = DependencyProperty.Register("NormalPathColor"
            , typeof(Brush), typeof(PathButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(40, 139, 225))));

        public static readonly DependencyProperty PathDataProperty = DependencyProperty.Register("PathData"
            , typeof(Geometry), typeof(PathButton));

        public static readonly DependencyProperty PathWidthProperty = DependencyProperty.Register("PathWidth"
                                                            , typeof(double), typeof(PathButton), new FrameworkPropertyMetadata(13d));

        public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.Register("PressedBackground"
            , typeof(Brush), typeof(PathButton));

        public static readonly DependencyProperty PressedPathColorProperty = DependencyProperty.Register("PressedPathColor"
            , typeof(Brush), typeof(PathButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(36, 127, 207))));

        static PathButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PathButton), new FrameworkPropertyMetadata(typeof(PathButton)));
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public Brush DisabledPathColor
        {
            get { return (Brush)GetValue(DisabledPathColorProperty); }
            set { SetValue(DisabledPathColorProperty, value); }
        }

        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        public Brush MouseOverPathColor
        {
            get { return (Brush)GetValue(MouseOverPathColorProperty); }
            set { SetValue(MouseOverPathColorProperty, value); }
        }

        public Brush NormalPathColor
        {
            get { return (Brush)GetValue(NormalPathColorProperty); }
            set { SetValue(NormalPathColorProperty, value); }
        }

        public Geometry PathData
        {
            get { return (Geometry)GetValue(PathDataProperty); }
            set { SetValue(PathDataProperty, value); }
        }

        public double PathWidth
        {
            get { return (double)GetValue(PathWidthProperty); }
            set { SetValue(PathWidthProperty, value); }
        }

        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        public Brush PressedPathColor
        {
            get { return (Brush)GetValue(PressedPathColorProperty); }
            set { SetValue(PressedPathColorProperty, value); }
        }
    }
}