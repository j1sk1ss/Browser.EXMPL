using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Browser.EXMPL.data;

namespace Browser.EXMPL.gui {
    public static class NewWindow {
        public static TabItem GetEmptyTab() {
            return new TabItem {
                Height = 20,
                Width = 20,
                FontSize = 14,
                Header = "+"
            };
        }

        public static TabItem GetHistoryTab(int number, MainWindow mainWindow) {
            var newTab = new TabItem {
                Height = 20,
                Width = 120,
                FontSize = 14,
                Header = "История"
            };

            var scroll = new ScrollViewer {
                Content = new Label().Content = string.Join("\n", LocalData.History)
            };

            var newGrid = new Grid
            {
                Name = $"grid_{number}"
            };
            newGrid.Children.Add(scroll);
            
            var deletePageButton = new Button {
                Height = 23,
                Width = 23,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0,10,20,0),
                Content = "-"
            };
            deletePageButton.Click += mainWindow.DeletePage;
            newGrid.Children.Add(deletePageButton);

            newTab.Content = newGrid;
            
            return newTab;
        }
        
        public static TabItem GetNewTab(int number, MainWindow mainWindow) {
            var newTab = new TabItem {
                Height = 20,
                Width = 120 + number.ToString().Length,
                FontSize = 14,
                Header = $"Новая вкладка({number})"
            };
            
            var newGrid = new Grid {
                Name = $"grid_{number}",
                Children = {
                    new TextBox {
                        Height = 23,
                        Width = 660,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(0,10,60,0)
                    },
                    new Line {
                        X1 = 0,
                        Y1 = 50,
                        X2 = 800,
                        Y2 = 50,
                        Stroke = Brushes.Black
                    }
                }
            };

            var web = new WebBrowser {
                Name = $"Web_{number}",
                Height = 370,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom
            };
            web.Navigate("https://google.com");
            newGrid.Children.Add(web);
            
            var previousPageButton = new Button {
                Name = $"pb_{number}",
                Height = 23,
                Width = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(50,10,0,0),
                Content = "<"
            };
            previousPageButton.Click += mainWindow.Previous;
            
            var deletePageButton = new Button {
                Name = $"db_{number}",
                Height = 23,
                Width = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(25,10,0,0),
                Content = "-"
            };
            deletePageButton.Click += mainWindow.DeletePage;
            
            var searchButton = new Button {
                Name = $"sb_{number}",
                Height = 23,
                Width = 30,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0,10,26,0),
                FontSize = 6,
                Content = "ПОИСК"
            };
            searchButton.Click += mainWindow.Search;
            
            var settingsButton = new Button {
                Name = $"st_{number}",
                Height = 23,
                Width = 10,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0,10,6,0),
                Content = ":"
            };
            settingsButton.Click += mainWindow.ShowHistory;

            newGrid.Children.Add(previousPageButton);
            newGrid.Children.Add(deletePageButton);
            newGrid.Children.Add(searchButton);
            newGrid.Children.Add(settingsButton);

            newTab.Content = newGrid;
            
            return newTab;
        }
    }
}