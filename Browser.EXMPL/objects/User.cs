using System.Collections.Generic;

namespace Browser.EXMPL.objects {
    public class User {
        public User(string nickName, string password) {
            UserName = nickName;
            Password = password;
        }
        public string UserName { get; }
        private string Password { get; }
        private List<string> History { get; set; }
    }
}