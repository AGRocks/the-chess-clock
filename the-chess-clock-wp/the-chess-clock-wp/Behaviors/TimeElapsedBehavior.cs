using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace the_chess_clock_wp.Behaviors
{
    public class TimeElapsedStateBehavior : DependencyObject
    {
        public static bool GetTimeElapsed(UIElement obj)
        {
            return (bool)obj.GetValue(TimeElapsedProperty);
        }

        public static void SetTimeElapsed(UIElement obj, bool value)
        {
            obj.SetValue(TimeElapsedProperty, value);
        }

        // Using a DependencyProperty as the backing store for TimeElapsed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeElapsedProperty =
            DependencyProperty.RegisterAttached("TimeElapsed", typeof(bool), typeof(TimeElapsedStateBehavior), new PropertyMetadata(false,
                (s, e) =>
                {
                    var button = s as Button;
                    if (button != null && (bool)e.NewValue)
                    {
                        VisualStateManager.GoToState(button, "TimeElapsed", true);
                    }
                }));
    }
}
