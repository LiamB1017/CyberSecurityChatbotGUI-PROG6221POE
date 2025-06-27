using CyberSecurityChatbotGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            string userMessage = UserInput.Text.ToLower();
            ChatLog.Text += $"\nUser: {userMessage}";

            string botResponse = ProcessUserInput(userMessage);
            ChatLog.Text += $"\nBot: {botResponse}";

            UserInput.Text = "";
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
