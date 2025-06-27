using System;
using System.Collections.Generic;
using System.Deployment.Internal;
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
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private List<TaskItem> TaskList = new List<TaskItem>();

        public TaskWindow()
        {
        }

        public TaskWindow(List<String> ActivityLog)
        {
            InitializeComponent();
            TaskListBox.ItemsSource = TaskList;
        }
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleBox.Text;
            string desc = TaskDescBox.Text;
            DateTime? reminder = TaskReminderPicker.SelectedDate;

            if (string.IsNullOrWhiteSpace(title))
            {
                TaskListBox.Items.Add($"{title} - {desc} - {reminder?.ToShortDateString()}");
            }
            
            TaskItem task = new TaskItem
            {
                Title = title,
                Description = desc,
                ReminderDate = reminder,
                IsCompleted = false
            };

            TaskList.Add(task);
            TaskListBox.Items.Refresh();
            TaskTitleBox.Text = "";
            TaskDescBox.Text = "";
            TaskReminderPicker.SelectedDate = null;
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem is TaskItem task)
            {
                task.IsCompleted = true;
                TaskListBox.Items.Refresh();
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem is TaskItem task)
            {
                TaskList.Remove(task);
                TaskListBox.Items.Refresh();
            }
        }
    }
}
