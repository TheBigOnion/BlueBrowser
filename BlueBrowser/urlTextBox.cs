using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BlueBrowser
{
    public class urlTextBox: TextBox
    {
        public urlTextBox()
        {
            // Set some default properties if needed
            this.Background = Brushes.LightYellow;
            this.Height = 180;
            this.Text = "https://www.duckduckgo.com";
        }

        protected override void OnGotFocus(System.Windows.RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            // Change background color when the TextBox gets focus
            this.Background = Brushes.LightGreen;
            
        }


        protected override void OnLostFocus(System.Windows.RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            // Revert background color when the TextBox loses focus
            this.Background = Brushes.SteelBlue;
        }

        
    }
}
