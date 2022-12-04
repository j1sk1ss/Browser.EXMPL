using Browser.EXMPL.objects;

namespace Browser.EXMPL.gui {
    public static class NewWindow {
        public static ExtendedTab GetNewTab() {
            var tab = new ExtendedTab {
                Header = "Новая вкладка",
            };
            tab.Content = new Content(tab);
            return tab;
        }
        
        public static ExtendedTab GetPlus() {
            var tab = new ExtendedTab {
                Header = "+",
                IsPlaceholder = true
            };

            return tab;
        }
    }
}