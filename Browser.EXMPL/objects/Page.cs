using System.Collections.Generic;
using System.Windows.Controls;

namespace Browser.EXMPL.objects {
    public class Page {
        public Page() => LocalHistory = new List<string>();
        public string GetUrl(int position) => LocalHistory[position];
        public void SetUrl(string url) => LocalHistory.Add(url);
        public WebBrowser WebBrowser { get; set; }
        private List<string> LocalHistory { get; set; }
        public void Search(string url) => WebBrowser.Navigate(url);
    }
}