using CyberSecurityChatbotGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CyberSecurityChatbotGUI
{
    public partial class MainWindow : Window
    {
        private List<string> activityLog = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }
        // Core chatbot logic

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInput.Text.Trim();
            ChatLog.Text += $"\n\nYou: {input}";

            string response = InterpretInput(input);
            ChatLog.Text += $"\nBot: {response}";

            UserInput.Clear();
        }
        private string InterpretInput(string input)
        {
            input = input.ToLower();

            if (input.Contains("add task") || input.Contains("set task") || input.Contains("remind me"))
            {
                // NLP - simulate auto-adding a task based on input
                string taskDescription = ExtractTaskFromInput(input);
                TaskItem task = new TaskItem
                {
                    Title = taskDescription,
                    Description = $"Auto-created from message: '{input}'",
                    ReminderDate = null,
                    IsCompleted = false
                };
                task.Add(task);
                activityLog.Add($"Task added via NLP: '{task.Title}'");
                return $"Task added: '{task.Title}'. Would you like to set a reminder?";
            }
            else if (input.Contains("start quiz") || input.Contains("quiz"))
            {
                OpenQuiz_Click(null, null);
                activityLog.Add("User started the quiz.");
                return "Launching the cybersecurity quiz now! 🎮";
            }
            else if (input.Contains("show log") || input.Contains("activity log") || input.Contains("what have you done"))
            {
                LogWindow logWindow = new LogWindow(activityLog);
                logWindow.ShowDialog();
                return "Here’s a summary of recent actions.";
            }
            else if (input.Contains("password") && input.Contains("remind"))
            {
                TaskItem task = new TaskItem
                {
                    Title = "Update your password",
                    Description = "Don’t forget to update your password!",
                    ReminderDate = DateTime.Now.AddDays(1),
                    IsCompleted = false
                };
                task.Add(task);
                activityLog.Add($"Reminder task auto-added: {task.Title}");
                return "Got it! I'll remind you tomorrow to update your password.";
            }

            // Fallback response
            return "I'm here to help with tasks, reminders, quizzes, and cybersecurity tips!";
        }

        private void OpenQuiz_Click(object value1, object value2)
        {
            throw new NotImplementedException();
        }

        private string ExtractTaskFromInput(string input)
        {
            // Simulate NLP parsing for task name
            string lower = input.ToLower();

            if (lower.Contains("enable two-factor"))
                return "Enable 2FA";

            if (lower.Contains("review") && lower.Contains("privacy"))
                return "Review Privacy Settings";

            if (lower.Contains("update password"))
                return "Update your Password";

            if (lower.Contains("backup"))
                return "Backup your data";

            // Default fallback
            return "General Cybersecurity Task";
        }



        private string ProcessUserInput(string input)
        {
            if (input.Contains("quiz"))
            {
                activityLog.Add("Quiz started");
                new QuizWindow().ShowDialog();
                return "Launching the cybersecurity quiz!";
            }

            if (input.Contains("task") || input.Contains("remind"))
            {
                new TaskWindow().ShowDialog();
                return "Opening Task Assistant...";
            }

            if (input.Contains("activity") || input.Contains("done for me"))
            {
                new LogWindow(activityLog).ShowDialog();
                return "Here’s what I’ve been doing.";
            }

            // Sentiment & keywords (basic simulation)
            if (input.Contains("phishing")) return "Phishing is when someone tries to steal your info by pretending to be someone else.";
            if (input.Contains("password")) return "Use strong passwords. Consider using a password manager.";
            if (input.Contains("privacy")) return "Review your privacy settings regularly.";
            if (input.Contains("hi") || input.Contains("hello")) return "Hey there! How can I help you stay safe online?";
            if (input.Contains("thanks")) return "You're welcome! 😊";

            return "I'm not sure I understand. Try saying something about tasks, quiz, or a topic like phishing.";
        }



        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserInput.Text.Trim();
            if (string.IsNullOrEmpty(userInput)) return;

            AddToChat($"You: {userInput}");
            UserInput.Clear();

            string response = GenerateResponse(userInput);
            AddToChat($"Bot: {response}");
        }


        private string GenerateResponse(string input)
        {
            string lowerInput = input.ToLower();

            // NLP-style detection
            if (lowerInput.Contains("remind") && lowerInput.Contains("password"))
            {
                activityLog.Add("Reminder task for 'Update password' added.");
                return "Reminder set to update your password.";
            }
            if (lowerInput.Contains("task") || lowerInput.Contains("add task"))
            {
                activityLog.Add("User asked to add/view a task.");
                return "📝 You can add tasks in the Task Assistant. Click the 'Task Assistant' button below!";
            }
            if (lowerInput.Contains("quiz") || lowerInput.Contains("test"))
            {
                activityLog.Add("Quiz accessed via chat.");
                return "🎮 Let's test your cybersecurity knowledge! Click the 'Quiz Game' button to begin.";
            }
            if (lowerInput.Contains("what have you done") || lowerInput.Contains("activity log") || lowerInput.Contains("show log"))
            {
                var recent = activityLog.Skip(Math.Max(0, activityLog.Count - 5));
                return "📋 Here's what I've done:\n" + string.Join("\n", recent.Select((a, i) => $"{i + 1}. {a}"));
            }

            // Keyword-based chatbot from Parts 1 and 2
            if (lowerInput.Contains("phishing"))
                return "Phishing is a fraudulent attempt to obtain sensitive information. Never click on suspicious links!";
            if (lowerInput.Contains("password"))
                return "Use strong, unique passwords and enable two-factor authentication (2FA) when possible.";
            if (lowerInput.Contains("privacy"))
                return "Review your privacy settings regularly and limit the personal info you share online.";
            if (lowerInput.Contains("hi") || lowerInput.Contains("hello"))
                return "Hello! How can I help you stay safe online today?";
            if (lowerInput.Contains("thank"))
                return "You're welcome! Stay cyber-safe! 😊";

            return "I'm still learning. Try asking about cybersecurity or use the buttons below to explore more features.";
        }
        private void ActivityLogButton_Click(object sender, RoutedEventArgs e)
        {
            LogWindow logWindow = new LogWindow(activityLog);
            logWindow.ShowDialog(); // Use ShowDialog() to prevent app shutdown
        }


        private void AddToChat(string message)
        {
            ChatLog.Text += message + "\n\n";
        }

        // Navigation Buttons
        private void OpenTaskWindow_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow taskWindow = new TaskWindow(activityLog);
            taskWindow.ShowDialog();
        }

        private void OpenQuizWindow_Click(object sender, RoutedEventArgs e)
        {
            QuizWindow quizWindow = new QuizWindow(activityLog);
            quizWindow.ShowDialog();
        }

        private void OpenLogWindow_Click(object sender, RoutedEventArgs e)
        {
            LogWindow logWindow = new LogWindow(activityLog);
            logWindow.ShowDialog();
        }
    }
}
