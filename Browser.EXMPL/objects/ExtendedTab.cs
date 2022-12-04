using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Browser.EXMPL.objects {
    public class ExtendedTab : INotifyPropertyChanged {
        
        private string _header;
        public string Header {
            get => _header;
            set {
                _header = value;
                OnPropertyChanged();
            }
        }

        private bool _isPlaceholder;
        public bool IsPlaceholder {
            get => _isPlaceholder;
            set {
                _isPlaceholder = value;
                OnPropertyChanged();
            }
        }

        private Content _content;
        public Content Content {
            get => _content;
            set {
                _content = value;
                OnPropertyChanged();
            }
        }

        public void ToDefaultWindow() {
            Header = "Новая вкладка";
            IsPlaceholder = false;
            Content = new Content(this);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string property = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

    public class Content {
        public Content(ExtendedTab extendedTab) {
            Page = new Page {
                
            };
        }
        private Page Page { get; set; }
    }
}