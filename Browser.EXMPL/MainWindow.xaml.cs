using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Browser.EXMPL.data;
using Browser.EXMPL.gui;
using Page = Browser.EXMPL.objects.Page;

namespace Browser.EXMPL {
    public partial class MainWindow
    {
        private const string HomePage = "google.com";
        private const string Prefix = "https://";
        public MainWindow() {
            InitializeComponent();
            
            Tabs    = new ObservableCollection<TabItem> { NewWindow.GetNewTab(0,this), NewWindow.GetEmptyTab() };
            Windows = new List<Page> { new() };
            
            Pages.ItemsSource = Tabs;
        }
        private ObservableCollection<TabItem> Tabs { get; set; }
        private List<Page> Windows { get; set; }
        public void DeletePage(object sender, RoutedEventArgs e) {
            try {
                if (Tabs.Count == 2) return;
                var index = Pages.SelectedIndex;
                
                Tabs.RemoveAt(index);
                Windows.RemoveAt(index);
                Pages.SelectedItem = Pages.Items[index - 1];
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }
        
        private const int TempSize = 3;

        private void OnPageChanged(object sender, SelectionChangedEventArgs e) {
            try {
                if (e.Source is not TabControl) return;
                
                if (Pages.SelectedIndex == Windows.Count) {
                    Tabs.Remove(Tabs.Last());
                    
                    Tabs.Add( NewWindow.GetNewTab(Tabs.Count,this));
                    Tabs.Add( NewWindow.GetEmptyTab());
                    Windows.Add(new Page());

                    Pages.SelectedItem = Pages.Items[^2];
                }
                e.Handled = true;
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }
        
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

        public void ShowHistory(object sender, RoutedEventArgs e) {
            try {
                Tabs.Remove(Tabs.Last());
                        
                Tabs.Add( NewWindow.GetHistoryTab(Tabs.Count, this));
                Tabs.Add( NewWindow.GetEmptyTab());
                Windows.Add(new Page());

                Pages.SelectedItem = Pages.Items[^2];
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }
        
        public void Previous(object sender, RoutedEventArgs e) {
            try {
                var links = Windows[Pages.SelectedIndex].LocalHistory;
                var browser        = (Pages.SelectedContent as Grid)!.Children[2] as WebBrowser;
                
                if (Windows[Pages.SelectedIndex].LocalHistory.Count == 1) return;
                links.Remove(links[^1]);
                browser!.Navigate(links[^1]);
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }
        
        private void AddToLocalHistory(string link) {
            Windows[Pages.SelectedIndex].LocalHistory.Add(link);
            
            if (Windows[Pages.SelectedIndex].LocalHistory.Count < TempSize) return;
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