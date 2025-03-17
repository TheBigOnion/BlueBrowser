using BlueBrowser.helpers;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Windows.Media.Media3D;

namespace BlueBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.SizeChanged += MainWindowSizeChange;
            setupTab(ref tabItem1, 1);

        }

        // Event handler for the "New Tab" button click
        private void NewTabButton_Click(object sender, RoutedEventArgs e)
        {
            int tabId = tabControl.Items.Count + 1;
            // Create a new TabItem
            TabItem newTab = new TabItem();
            newTab.Header = $"Tab {tabId}";  
            newTab.Name = $"tabItem{tabId}";

            setupTab(ref newTab, tabId);

            // Add the new tab to the TabControl
            tabControl.Items.Insert(tabControl.Items.Count - 1, newTab);

            tabControl.SelectedItem = newTab;

            MainWindowSizeChange(this, null);
        }

        private void setupTab(ref TabItem newTab, int tabId)
        {


            StackPanel stackPanel = new StackPanel();
            stackPanel.Name = $"stackPanel{tabId}";
            stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            stackPanel.VerticalAlignment = VerticalAlignment.Top;
           

            UrlBar urlBar = new UrlBar();

            WebView2 webView = new WebView2();
            webView.Name = $"webView{tabId}";
            webView.Margin = new Thickness(3, 3, 3, 3);
            webView.Width = mainWindow.Width - 50;
            
            InitializeWebView2(webView, urlBar.txtUrl.Text);

            Grid viewGrid = new Grid();

            viewGrid.Name = $"webGrid{tabId}";

            viewGrid.HorizontalAlignment = HorizontalAlignment.Left;
            viewGrid.VerticalAlignment = VerticalAlignment.Top;
            viewGrid.Margin = new Thickness(10, 1, 10, 20);
            

            viewGrid.Children.Add(webView);
            stackPanel.Children.Add(urlBar);
            stackPanel.Children.Add(viewGrid);

            newTab.Content = stackPanel;
            newTab.Header = urlBar.txtUrl.Text;
            

        }
        private void MainWindowSizeChange(object sender, SizeChangedEventArgs e)
        {
            double mainWindowWidth = this.ActualWidth;
            double mainWindowHeight = this.ActualHeight;


            AdjustControlSizes(mainWindowWidth, mainWindowHeight);
        }

        private void AdjustControlSizes(double width, double height)
        {
            tabControl.Height = height;
            tabControl.Width = width;

            foreach (var tab in tabControl.Items)
            {

                if (tab != null && tab is TabItem tabItem)
                {


                    string nameRemoved = tabItem.Name.Replace("tabItem", "");
                    int tabId = 0;
                    bool gotTabID = int.TryParse(nameRemoved, out tabId);

                    if (gotTabID)
                    {
                        UrlBar urlBarControl = Finder.FindVisualChild<UrlBar>(this, "urlBar" + tabId.ToString());
                        if (urlBarControl != null)
                        {
                            urlBarControl.Width = width - 50;
                        }

                        StackPanel stackPanelControl = Finder.FindVisualChild<StackPanel>(this, "stackPanel" + tabId.ToString());
                        if (stackPanelControl != null)
                        {
                            stackPanelControl.Width = width - 50;
                           
                        }

                        Grid gridControl = Finder.FindVisualChild<Grid>(this, "grid" + tabId.ToString());
                        if (gridControl != null)
                        {
                            gridControl.Width = width - 50;
                            
                        }


                        WebView2 webViewControl = Finder.FindVisualChild<WebView2>(this, "webView" + tabId.ToString());
                        if (webViewControl != null)
                        {
                            webViewControl.Width = width - 50;
                            webViewControl.Height = height - 150;
                        }
                    }


                }
            }


        }


        private async void InitializeWebView2(WebView2 webView, string url)
        {
            // Ensure WebView2 is initialized
            await webView.EnsureCoreWebView2Async(null);
            // Navigate to a website
            webView.CoreWebView2.Navigate(url);
        }


 
       



}

}