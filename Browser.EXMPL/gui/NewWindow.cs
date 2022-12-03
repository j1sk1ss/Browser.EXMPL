using System.Windows.Controls;

namespace Browser.EXMPL.gui
{
    public static class NewWindow
    {
        public static TabItem GetNewTab()
        {
            var newTab = new TabItem {
                Height = 20,
                Width = 20,
                FontSize = 14,
                Header = "Новая вкладка",
            };
            
            return null;
        }
    }
}