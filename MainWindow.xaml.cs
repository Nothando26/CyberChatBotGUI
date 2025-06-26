using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace CyberChatBotPOEPart1
{
    public partial class MainWindow : Window
    {
        private List<string> cybersecurityKeywords = new List<string> { "phishing", "malware", "firewall", "encryption", "password" };

        private Dictionary<string, string> cybersecurityExplanations = new Dictionary<string, string>()
        {
            { "phishing", "Phishing is a cyber attack where attackers try to trick you into giving personal information via fake emails or websites." },
            { "malware", "Malware is malicious software designed to harm or exploit devices and data." },
            { "firewall", "A firewall acts as a barrier between your computer/network and unauthorized access from the internet." },
            { "encryption", "Encryption scrambles data so only authorized parties can read it." },
            { "password", "Passwords protect your accounts; strong ones use a mix of letters, numbers, and symbols." }
        };

        private Dictionary<string, string> emotionalResponses = new Dictionary<string, string>()
        {
            { "happy", "😊 I'm glad you're feeling happy! What made your day great?" },
            { "sad", "😢 I'm sorry to hear you're feeling sad. Want to talk about it?" },
            { "stressed", "😰 Stress can be tough. Remember to take breaks and breathe." },
            { "angry", "😡 It’s okay to feel angry sometimes. Want to share what’s bothering you?" },
            { "excited", "🎉 Excitement is contagious! What’s got you so thrilled?" },
            { "great", "👍 Awesome! I'm happy you're feeling great today!" },
            { "good", "🙂 That’s good to hear! What’s going well?" },
            { "okay", "😌 Glad to hear you’re feeling okay. Let me know if you want to chat more." },
            { "tired", "😴 Being tired is tough. Make sure to get some rest soon." },
            { "anxious", "😟 Anxiety can be hard. I’m here if you want to talk." }
        };

        private List<string> randomResponses = new List<string>
        {
            "Interesting! Tell me more.",
            "Can you explain that further?",
            "Why do you think that is?",
            "That sounds important.",
            "I'm here to listen."
        };

        private List<string> activityLog = new List<string>();

        private List<string> quizQuestions = new List<string>
        {
            "1. True or False: Phishing is a technique used to trick users into revealing personal information.\nA. True\nB. False",
            "2. Which of these is considered malware?\nA. Firewall\nB. Antivirus\nC. Trojan\nD. VPN",
            "3. True or False: A strong password includes numbers, symbols, and both upper and lower-case letters.\nA. True\nB. False",
            "4. Which of the following is used to protect a network?\nA. Cookie\nB. Router\nC. Firewall\nD. Switch",
            "5. True or False: Public Wi-Fi is always safe to use for banking.\nA. True\nB. False",
            "6. Which one is a secure protocol?\nA. HTTP\nB. FTP\nC. HTTPS\nD. Telnet",
            "7. True or False: Keeping software up to date can help prevent security vulnerabilities.\nA. True\nB. False",
            "8. What does 2FA stand for?\nA. Two-Factor Authentication\nB. Two-Faced Authorization\nC. Second Form Access\nD. Twin Firewall Access",
            "9. True or False: Social engineering is a technical hacking method.\nA. True\nB. False",
            "10. What should you do if you receive a suspicious email?\nA. Open it immediately\nB. Delete it or report it\nC. Forward it to friends\nD. Ignore it"
        };

        private List<string> quizCorrectAnswers = new List<string>
        {
            "A", "C", "A", "C", "B", "C", "A", "A", "B", "B"
        };

        private List<string> quizIncorrectFeedback = new List<string>
        {
            "❌ Incorrect. Phishing *does* trick users into revealing personal data.",
            "❌ Incorrect. The correct answer is C. A Trojan is malware.",
            "❌ Incorrect. A strong password should include a mix of characters.",
            "❌ Incorrect. Firewalls protect networks from attacks.",
            "❌ Incorrect. Public Wi-Fi is *not* safe for banking.",
            "❌ Incorrect. HTTPS is secure; the 'S' stands for 'Secure'.",
            "❌ Incorrect. Updates often patch security vulnerabilities.",
            "❌ Incorrect. 2FA means Two-Factor Authentication.",
            "❌ Incorrect. Social engineering is non-technical, based on manipulation.",
            "❌ Incorrect. Suspicious emails should be deleted or reported."
        };

        private int quizScore = 0;
        private int quizIndex = 0;
        private bool inQuizMode = false;

        private bool waitingForCyberChoice = false;
        private string lastCyberTopic = "";

        private string userName;

        // Reminder structure (message and due time)
        private List<(string Message, DateTime DueTime)> reminders = new List<(string, DateTime)>();

        public MainWindow(string userName)
        {
            InitializeComponent();
            this.userName = userName;
            OutputTextBlock.Text = "🤖 Hello! I'm your Cybersecurity Assistant.\nHow are you feeling today?\n";
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInputBox.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            Log($"> {input}");

            // Show activity log command
            if (input.ToLower().Contains("show") && input.ToLower().Contains("activity log"))
            {
                ShowActivityLog();
                UserInputBox.Clear();
                return;
            }

            if (inQuizMode)
            {
                HandleQuizAnswer(input);
            }
            else if (waitingForCyberChoice)
            {
                if (input.ToLower().Contains("learn more"))
                {
                    Log($"Here's some more detailed info on {lastCyberTopic}");
                    waitingForCyberChoice = false;
                }
                else if (input.ToLower().Contains("quiz"))
                {
                    StartQuiz();
                    waitingForCyberChoice = false;
                }
                else
                {
                    Log("Please type 'learn more' to get details or 'quiz' to start the quiz.");
                }
            }
            else if (input.ToLower().StartsWith("add task"))
            {
                OpenTaskWindow();
            }
            else if (input.ToLower() == "view tasks")
            {
                Log("To view tasks, please open the Tasks window.");
            }
            else if (input.ToLower().StartsWith("delete task"))
            {
                Log("Task deletion must be done from the Tasks window.");
            }
            else if (input.ToLower().StartsWith("complete task"))
            {
                Log("Marking tasks complete must be done from the Tasks window.");
            }
            else if (input.ToLower().Contains("quiz"))
            {
                StartQuiz();
            }
            else if (MatchReminder(input))
            {
                // Reminder matched and logged inside MatchReminder
            }
            else
            {
                string matchedCyberKeyword = GetCyberKeyword(input);
                if (matchedCyberKeyword != null)
                {
                    lastCyberTopic = matchedCyberKeyword;
                    Log("💡 " + cybersecurityExplanations[matchedCyberKeyword]);
                    Log("Would you like to learn more about this topic? Type 'learn more' or 'quiz' to start a quiz.");
                    waitingForCyberChoice = true;
                }
                else if (MatchEmotion(input))
                {
                    foreach (var key in emotionalResponses.Keys)
                    {
                        if (input.ToLower().Contains(key))
                        {
                            Log(emotionalResponses[key]);
                            break;
                        }
                    }
                }
                else
                {
                    Random rand = new Random();
                    Log("💬 " + randomResponses[rand.Next(randomResponses.Count)]);
                }
            }

            UserInputBox.Clear();
        }

        private void ShowActivityLog()
        {
            if (activityLog.Count == 0)
            {
                Log("Your activity log is empty.");
                return;
            }

            Log("📜 Here's your activity log:");
            foreach (string entry in activityLog)
            {
                OutputTextBlock.Text += entry + "\n";
            }
        }

        private void OpenTaskWindow()
        {
            try
            {
                AddTaskWindow taskWindow = new AddTaskWindow();
                taskWindow.Owner = this;
                taskWindow.ShowDialog();
                Log("Opened the Task Manager window.");
            }
            catch (Exception ex)
            {
                Log($"❌ Failed to open Task window: {ex.Message}");
            }
        }

        private void Log(string message)
        {
            activityLog.Add(message);
            OutputTextBlock.Text += message + "\n";
        }

        private string GetCyberKeyword(string input)
        {
            foreach (var keyword in cybersecurityKeywords)
                if (input.ToLower().Contains(keyword))
                    return keyword;
            return null;
        }

        private bool MatchEmotion(string input)
        {
            foreach (var key in emotionalResponses.Keys)
                if (input.ToLower().Contains(key)) return true;
            return false;
        }

        private void StartQuiz()
        {
            inQuizMode = true;
            quizScore = 0;
            quizIndex = 0;
            Log("🧠 Starting Cybersecurity Quiz! Answer with A, B, C, D or True/False.");
            ShowQuizQuestion();
        }

        private void ShowQuizQuestion()
        {
            Log(quizQuestions[quizIndex]);
        }

        private void HandleQuizAnswer(string input)
        {
            string answer = input.Trim().ToUpper();

            // Normalize True/False input
            if (answer == "TRUE" || answer == "T") answer = "A";
            else if (answer == "FALSE" || answer == "F") answer = "B";

            if (answer == quizCorrectAnswers[quizIndex])
            {
                quizScore++;
                Log("✅ Correct!");
            }
            else
            {
                Log(quizIncorrectFeedback[quizIndex]);
            }

            quizIndex++;

            if (quizIndex >= quizQuestions.Count)
            {
                inQuizMode = false;
                ShowQuizResults();
            }
            else
            {
                ShowQuizQuestion();
            }
        }

        private void ShowQuizResults()
        {
            Log($"🎉 Quiz complete! You scored {quizScore} out of {quizQuestions.Count}.");
            if (quizScore >= 8)
            {
                Log("Excellent work! You really know your cybersecurity stuff!");
            }
            else if (quizScore >= 5)
            {
                Log("Good job! Keep learning and you'll be an expert in no time.");
            }
            else
            {
                Log("Don't worry, cybersecurity is a big topic. Keep practicing!");
            }
        }

        private bool MatchReminder(string input)
        {
            // Use regex to find "remind me in X units to Y" anywhere in the string, case-insensitive
            Match match = Regex.Match(input.ToLower(), @"remind me in (\d+) (seconds|minutes|hours|days|weeks) to (.+)");
            if (match.Success)
            {
                int quantity = int.Parse(match.Groups[1].Value);
                string unit = match.Groups[2].Value;
                string message = match.Groups[3].Value;

                DateTime due = DateTime.Now;
                switch (unit)
                {
                    case "seconds": due = due.AddSeconds(quantity); break;
                    case "minutes": due = due.AddMinutes(quantity); break;
                    case "hours": due = due.AddHours(quantity); break;
                    case "days": due = due.AddDays(quantity); break;
                    case "weeks": due = due.AddDays(quantity * 7); break;
                }

                reminders.Add((message, due));
                Log($"⏰ Reminder set: '{message}' in {quantity} {unit}.");
                return true;
            }
            return false;
        }
    }
}
