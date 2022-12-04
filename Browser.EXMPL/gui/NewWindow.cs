using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Browser.EXMPL.gui {
    public static class NewWindow {
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
            
            var button = new Button {
                Height = 23,
                Width = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0,10,0,0),
                Content = "+"
            };
            button.Click += mainWindow.NewPage;
            
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
            settingsButton.Click += MainWindow.ShowHistory;
            
            newGrid.Children.Add(button);
            newGrid.Children.Add(previousPageButton);
            newGrid.Children.Add(deletePageButton);
            newGrid.Children.Add(searchButton);
            newGrid.Children.Add(settingsButton);

            newTab.Content = newGrid;
            
            return newTab;
        }
    }
}