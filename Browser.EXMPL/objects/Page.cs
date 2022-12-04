using System.Collections.Generic;

namespace Browser.EXMPL.objects {
    public class Page {
        public Page() {
            LocalHistory = new List<string>{"","",""};
        }
        public List<string> LocalHistory { get; set; }
    }
}