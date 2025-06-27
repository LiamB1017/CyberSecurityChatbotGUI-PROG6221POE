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
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        private List<Question> questions;
        private int currentQuestionIndex = 0;
        private int score = 0;
        public QuizWindow()
        {
        }

        public QuizWindow(List<String> ActivityLog)
        {
            InitializeComponent();
            LoadQuestions();
            ShowQuestion();
        }
        private void LoadQuestions()
        {
            questions = new List<Question>
            {
                new Question
                {
                    Text = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "Reply with your password", "Delete the email", "Report it as phishing", "Ignore it" },
                    CorrectOptionIndex = 2,
                    Explanation = "Never share your password. Report suspicious emails as phishing."
                },
                new Question
                {
                    Text = "True or False: 'password123' is a strong password.",
                    Options = new List<string> { "True", "False" },
                    CorrectOptionIndex = 1,
                    Explanation = "'password123' is extremely weak and commonly guessed."
                },
                new Question
                {
                    Text = "What is two-factor authentication (2FA)?",
                    Options = new List<string> {
                        "A password typed twice",
                        "An extra login step using a code or device",
                        "Using two accounts",
                        "A backup password"
                    },
                    CorrectOptionIndex = 1,
                    Explanation = "2FA adds an extra layer of security by requiring a second factor."
                },
                new Question
                {
                    Text = "Which of the following is a secure way to store passwords?",
                    Options = new List<string> {
                        "Write them on paper",
                        "Store in browser",
                        "Use a password manager",
                        "Save in plain text files"
                    },
                    CorrectOptionIndex = 2,
                    Explanation = "Password managers securely store and encrypt your passwords."
                },
                new Question
                {
                    Text = "What is phishing?",
                    Options = new List<string> {
                        "A way to catch fish online",
                        "A hacking attempt via fake communication",
                        "A type of virus",
                        "A secure login method"
                    },
                    CorrectOptionIndex = 1,
                    Explanation = "Phishing tricks users into giving away personal info."
                },
                new Question
                {
                    Text = "True or False: Public Wi-Fi is always safe to use without VPN.",
                    Options = new List<string> { "True", "False" },
                    CorrectOptionIndex = 1,
                    Explanation = "Public Wi-Fi can be risky without encryption or a VPN."
                },
                new Question
                {
                    Text = "Which file extension could be dangerous to open in an email?",
                    Options = new List<string> { ".jpg", ".pdf", ".exe", ".txt" },
                    CorrectOptionIndex = 2,
                    Explanation = ".exe files can execute harmful code."
                },
                new Question
                {
                    Text = "What is social engineering?",
                    Options = new List<string> {
                        "Building social apps",
                        "Manipulating people to give up information",
                        "Fixing broken devices",
                        "Engineering social media campaigns"
                    },
                    CorrectOptionIndex = 1,
                    Explanation = "Social engineering relies on human interaction to steal data."
                },
                new Question
                {
                    Text = "Which password is the most secure?",
                    Options = new List<string> { "12345678", "mydog", "MyName2024", "L#8f!93zQ@" },
                    CorrectOptionIndex = 3,
                    Explanation = "Long, complex passwords with symbols are the most secure."
                },
                new Question
                {
                    Text = "What does HTTPS mean?",
                    Options = new List<string> {
                        "It’s a typo",
                        "A secure version of HTTP",
                        "A gaming protocol",
                        "A programming language"
                    },
                    CorrectOptionIndex = 1,
                    Explanation = "HTTPS encrypts data between your browser and the website."
                }
            };
        }

        private void ShowQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
            {
                QuestionText.Text = $"Quiz Complete! You scored {score} out of {questions.Count}.";
                OptionsPanel.Children.Clear();
                NextButton.IsEnabled = false;

                FeedbackText.Text = score >= 7
                    ? "Great job! You're a cybersecurity pro! 🔐"
                    : "Keep learning to stay safe online! 🔍";
                return;
            }

            var current = questions[currentQuestionIndex];
            QuestionText.Text = current.Text;
            OptionsPanel.Children.Clear();

            for (int i = 0; i < current.Options.Count; i++)
            {
                RadioButton rb = new RadioButton
                {
                    Content = current.Options[i],
                    GroupName = "OptionsGroup",
                    Tag = i,
                    Margin = new Thickness(0, 5, 0, 0)
                };
                OptionsPanel.Children.Add(rb);
            }

            FeedbackText.Text = "";
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = -1;

            foreach (var child in OptionsPanel.Children)
            {
                if (child is RadioButton rb && rb.IsChecked == true)
                {
                    selected = (int)rb.Tag;
                    break;
                }
            }

            if (selected == -1)
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            var current = questions[currentQuestionIndex];

            if (selected == current.CorrectOptionIndex)
            {
                score++;
                FeedbackText.Text = "Correct! ✅ " + current.Explanation;
            }
            else
            {
                FeedbackText.Text = "Incorrect ❌ " + current.Explanation;
            }

            currentQuestionIndex++;
            ShowQuestion();
        }
    }
}
    
