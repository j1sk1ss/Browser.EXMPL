using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Browser.EXMPL.data;
using Browser.EXMPL.gui;
using Browser.EXMPL.windows;
using Page = Browser.EXMPL.objects.Page;

namespace Browser.EXMPL {
    public partial class MainWindow : Window
    {
        private const string HomePage = "google.com";
        private const string Prefix = "https://";
        public MainWindow() {
            InitializeComponent();
            
            Tabs    = new ObservableCollection<TabItem> { NewWindow.GetNewTab(0,this) };
            Windows = new List<Page> { new() };
            
            Pages.ItemsSource = Tabs;
        }
        private ObservableCollection<TabItem> Tabs { get; set; }
        private List<Page> Windows { get; set; }
        public void NewPage(object sender, RoutedEventArgs e) {
            Tabs.Add( NewWindow.GetNewTab(Tabs.Count,this));
            Windows.Add(new Page());

            Pages.SelectedItem = Pages.Items[^1];
        }

        public void DeletePage(object sender, RoutedEventArgs e) {
            var index = int.Parse((Pages.SelectedContent as Grid)!.Name.Split("_")[1]);
            if (Tabs.Count == 1) return; 
            
            Tabs.RemoveAt(index);
            Windows.RemoveAt(index);
        }
        private const int TempSize = 3;

        public void Search(object sender, RoutedEventArgs e) {
            try {
                var browser   = (Pages.SelectedContent as Grid)!.Children[2] as WebBrowser;
                var userInput = (Pages.SelectedContent as Grid)!.Children[0] as TextBox;

                var link = $"{Prefix}{(userInput!.Text == "" ? HomePage : userInput!.Text)}";
                (Pages.SelectedItem as TabItem)!.Header = link;
                AddToLocalHistory(link);
                LocalData.History.Add(link);
                
                browser!.Navigate(link);
                HideScriptErrors(browser, true);
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }
        public static void ShowHistory(object sender, RoutedEventArgs e) => new History().Show();
        public void Previous(object sender, RoutedEventArgs e) {
            var links = Windows[Pages.SelectedIndex].LocalHistory;
            var browser   = (Pages.SelectedContent as Grid)!.Children[2] as WebBrowser;
            
            
            if (Windows[Pages.SelectedIndex].LocalHistory.Count < 3) return;
            browser!.Navigate(links[1]);
        }
        private void AddToLocalHistory(string link) {
            Windows[Pages.SelectedIndex].LocalHistory.Add(link);
            
            if (Windows[Pages.SelectedIndex].LocalHistory.Count < 3) return;
            Windows[Pages.SelectedIndex].LocalHistory.RemoveAt(0);
        }
        private static void HideScriptErrors(WebBrowser wb, bool hide) {
            var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", 
                BindingFlags.Instance | BindingFlags.NonPublic);
            
            if (fiComWebBrowser == null) return;
            var objComWebBrowser = fiComWebBrowser.GetValue(wb);
            objComWebBrowser?.GetType().InvokeMember("Silent", 
                BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
        }
    }
}