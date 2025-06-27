using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatbotGUI
{
    class TaskItem
    {
        public string Title { get; internal set; }
        public string Description { get; internal set; }
        public DateTime? ReminderDate { get; internal set; }
        public bool IsCompleted { get; internal set; }

        internal void Add(TaskItem task)
        {
            throw new NotImplementedException();
        }
    }
}
public class TaskItem
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? ReminderDate { get; set; }
    public bool IsCompleted { get; set; }

    public override string ToString()
    {
        string status = IsCompleted ? "✅" : "❌";
        string reminder = ReminderDate.HasValue ? $" (Remind: {ReminderDate.Value.ToShortDateString()})" : "";
        return $"{status} {Title}{reminder}";
    }
}
