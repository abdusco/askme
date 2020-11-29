using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace AskMe.Wpf
{
    public class PutCaretAtEndOnFocusBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            if (!(AssociatedObject is TextBox tb)) return;
            tb.GotFocus += OnFocused;
        }

        protected override void OnDetaching()
        {
            if (!(AssociatedObject is TextBox tb)) return;
            tb.GotFocus -= OnFocused;
        }

        private void OnFocused(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox tb)) return;
            tb.CaretIndex = tb.Text.Length;
        }
    }
}