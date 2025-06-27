using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CyberSecurityChatbotGUI
{
    /// <summary>  
    /// Interaction logic for LogWindow.xaml  
    /// </summary>  
    public partial class LogWindow : Window
    {
        public LogWindow(List<string> activityLog)
        {
            InitializeComponent();
            Log = activityLog;
            ShowRecent(Log);
        }

        public List<string> Log { get; private set; }

        private void ShowRecent(List<string> log)
        {
            var recent = log.Count > 10 ? log.GetRange(log.Count - 10, 10) : log;
            foreach (var item in recent)
            {
                LogList.Items.Add(item);
            }
        }
    }
}

