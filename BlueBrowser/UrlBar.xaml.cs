using BlueBrowser.helpers;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace BlueBrowser
{
    /// <summary>
    /// Interaction logic for UrlBar.xaml
    /// </summary>
    public partial class UrlBar : UserControl
    {
        public UrlBar()
        {
            InitializeComponent();
        }
   

        private void NavGo(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            

            TabItem activeTab = GetActiveTab();

            string nameRemoved = activeTab.Name.Replace("tabItem", "");
            int tabId = 0;
            bool gotTabID = int.TryParse(nameRemoved, out tabId);

            var content = activeTab.Content;

            //tabControl.SelectedItem 
            WebView2 webViewControl = Finder.FindVisualChild<WebView2>(activeTab.Parent, "webView" + tabId.ToString());

            webViewControl.Source = new Uri(txtUrl.Text);

        }

        private void NavBack(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();


            TabItem activeTab = GetActiveTab();

            string nameRemoved = activeTab.Name.Replace("tabItem", "");
            int tabId = 0;
            bool gotTabID = int.TryParse(nameRemoved, out tabId);

            var content = activeTab.Content;

            //tabControl.SelectedItem 
            WebView2 webViewControl = Finder.FindVisualChild<WebView2>(activeTab.Parent, "webView" + tabId.ToString());

            webViewControl.CoreWebView2.GoBack();

        }


        public TabItem GetActiveTab()
        {
            // Traverse up the visual tree to find the parent Window
            DependencyObject parent = VisualTreeHelper.GetParent(this);


            while (parent != null)
            {
                if (parent is TabControl)
                {
                    TabControl tabControl = (TabControl)parent;
                    TabItem? activeTab = tabControl.SelectedItem as TabItem;
                    if (activeTab != null)
                    {
                        return activeTab;
                    }

                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }
    }


}
