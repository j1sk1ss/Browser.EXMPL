using System.Windows;
using Browser.EXMPL.data;

namespace Browser.EXMPL.windows
{
    public partial class History : Window
    {
        public History()
        {
            InitializeComponent();

            for (var i = 0; i < LocalData.History.Count; i++) HistoryLabel.Content += $"{i + 1}) {LocalData.History[i]}\n";
        }
    }
}