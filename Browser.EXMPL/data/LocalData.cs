using System.Collections.Generic;

namespace Browser.EXMPL.data {
    public static class LocalData {
        static LocalData() {
            History = new List<string>();
        }
        public static List<string> History { get; set; }
    }
}