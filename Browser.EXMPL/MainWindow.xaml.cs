using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Browser.EXMPL.gui;
using Browser.EXMPL.objects;

namespace Browser.EXMPL {
    public partial class MainWindow {
        private readonly ObservableCollection<ExtendedTab> _tabs = new();
        public MainWindow() {
            InitializeComponent();
            
            _tabs.Add(NewWindow.GetNewTab());
            _tabs.Add(NewWindow.GetPlus());

            Pages.ItemsSource = _tabs;
            Pages.SelectionChanged += UserChangeTab;
        }

        private void UserChangeTab(object sender, SelectionChangedEventArgs e) {
            if (e.Source is not TabControl) return;
            var selectedTab = Pages.SelectedIndex;
            
            if (selectedTab == 0 || selectedTab != _tabs.Count - 1) return; 
            
            _tabs.Last().ToDefaultWindow();
            _tabs.Add(NewWindow.GetPlus());
        }
        
        private void OnTabCloseClick(object sender, RoutedEventArgs e) {
            var tab = (sender as Button)!.DataContext as ExtendedTab;
            if (_tabs.Count <= 2) return;
            
            var index = _tabs.IndexOf(tab);
            if(index == _tabs.Count - 2) Pages.SelectedIndex--;
            
            _tabs.RemoveAt(index);
        }
    }
}