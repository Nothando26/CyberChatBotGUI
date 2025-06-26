using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

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

        // Task structure includes Title, Description, DueDate, and Completion status
        private List<TaskItem> tasks = new List<TaskItem>();

        // Reminders structure (message and due time)
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

            if (inQuizMode)
            {
                HandleQuizAnswer(input);
            }
            else if (waitingForCyberChoice)
            {
                if (input.ToLower().Contains("learn more"))
                {
                    Log($"Here's some more detailed info on {lastCyberTopic}: [Add detailed info here or link resources!]");
                    waitingForCyberChoice = false;
                }
                else if (input.ToLower().Contains("quiz"))
                {
                    StartQuiz();
                    waitingForCyberChoice = false;
                }
                else
                {
                    Log("Please type 'learn more' to get details or 'quiz' to start a quiz.");
                }
            }
            else if (input.ToLower().StartsWith("add task"))
            {
                if (!HandleAddTask(input))
                {
                    Log("❌ Invalid task format. Use: add task: [title], description: [desc], due: [yyyy-MM-dd] or 'none' for no due date, optionally add reminder: [in X days/hours/minutes]");
                }
            }
            else if (input.ToLower() == "view tasks")
            {
                ShowTasks();
            }
            else if (input.ToLower().StartsWith("delete task"))
            {
                if (!HandleDeleteTask(input))
                {
                    Log("❌ To delete a task, type: delete task [task number]");
                }
            }
            else if (input.ToLower().StartsWith("complete task"))
            {
                if (!HandleCompleteTask(input))
                {
                    Log("❌ To mark a task complete, type: complete task [task number]");
                }
            }
            else if (input.ToLower().Contains("quiz"))
            {
                StartQuiz();
            }
            else if (MatchReminder(input))
            {
                // Reminder handled inside MatchReminder
            }
            else
            {
                string matchedCyberKeyword = GetCyberKeyword(input);
                if (matchedCyberKeyword != null)
                {
                    lastCyberTopic = matchedCyberKeyword;
                    Log("💡 " + cybersecurityExplanations[matchedCyberKeyword]);
                    Log("Would you like to learn more about this topic or take a quiz? (Type 'learn more' or 'quiz')");
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
            // Example: "remind me in 3 days to backup files"
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

        // Handles adding tasks with optional reminders
        private bool HandleAddTask(string input)
        {
            // Accept input like:
            // add task: Buy groceries, description: Buy milk and eggs, due: 2025-07-01, reminder: in 2 days
            // or missing parts

            // Parse with Regex for optional fields
            var regex = new Regex(@"add task: (?<title>[^,]+), description: (?<desc>[^,]+), due: (?<due>[^,]+)(, reminder: (?<reminder>.+))?", RegexOptions.IgnoreCase);
            var match = regex.Match(input);

            if (!match.Success) return false;

            string title = match.Groups["title"].Value.Trim();
            string desc = match.Groups["desc"].Value.Trim();
            string dueStr = match.Groups["due"].Value.Trim();
            string reminderStr = match.Groups["reminder"].Value.Trim();

            DateTime? dueDate = null;
            if (!string.Equals(dueStr, "none", StringComparison.OrdinalIgnoreCase))
            {
                if (!DateTime.TryParse(dueStr, out DateTime parsedDue))
                {
                    Log("❌ Invalid due date format. Use yyyy-MM-dd or 'none'.");
                    return true; // We handled it by showing error
                }
                dueDate = parsedDue;
            }

            tasks.Add(new TaskItem(title, desc, dueDate, false));
            Log($"✅ Task added: {title} - {desc} {(dueDate.HasValue ? $"(Due: {dueDate.Value.ToShortDateString()})" : "")}");

            // Handle reminder if provided
            if (!string.IsNullOrEmpty(reminderStr))
            {
                // Example reminderStr: "in 2 days", "in 5 hours"
                var remMatch = Regex.Match(reminderStr.ToLower(), @"in (\d+) (seconds|minutes|hours|days|weeks)");
                if (remMatch.Success)
                {
                    int qty = int.Parse(remMatch.Groups[1].Value);
                    string unit = remMatch.Groups[2].Value;

                    DateTime due = DateTime.Now;
                    switch (unit)
                    {
                        case "seconds": due = due.AddSeconds(qty); break;
                        case "minutes": due = due.AddMinutes(qty); break;
                        case "hours": due = due.AddHours(qty); break;
                        case "days": due = due.AddDays(qty); break;
                        case "weeks": due = due.AddDays(qty * 7); break;
                    }

                    reminders.Add(($"Task reminder: {title}", due));
                    Log($"⏰ Reminder set for task '{title}' in {qty} {unit}.");
                }
                else
                {
                    Log("❌ Could not parse reminder time. Use 'in X days/hours/minutes'.");
                }
            }

            return true;
        }

        private void ShowTasks()
        {
            if (tasks.Count == 0)
            {
                Log("📋 No tasks available.");
                return;
            }

            Log("📋 Your tasks:");
            for (int i = 0; i < tasks.Count; i++)
            {
                var t = tasks[i];
                Log($"{i + 1}. {(t.IsComplete ? "✔️" : "❌")} {t.Title} - {t.Description}" +
                    (t.DueDate.HasValue ? $" (Due: {t.DueDate.Value.ToShortDateString()})" : ""));
            }
        }

        private bool HandleDeleteTask(string input)
        {
            // input format: delete task 2
            var regex = new Regex(@"delete task (\d+)", RegexOptions.IgnoreCase);
            var match = regex.Match(input);
            if (!match.Success) return false;

            int idx = int.Parse(match.Groups[1].Value) - 1;
            if (idx < 0 || idx >= tasks.Count)
            {
                Log("❌ Invalid task number.");
                return true;
            }

            var removed = tasks[idx];
            tasks.RemoveAt(idx);
            Log($"🗑️ Deleted task: {removed.Title}");
            return true;
        }

        private bool HandleCompleteTask(string input)
        {
            // input format: complete task 1
            var regex = new Regex(@"complete task (\d+)", RegexOptions.IgnoreCase);
            var match = regex.Match(input);
            if (!match.Success) return false;

            int idx = int.Parse(match.Groups[1].Value) - 1;
            if (idx < 0 || idx >= tasks.Count)
            {
                Log("❌ Invalid task number.");
                return true;
            }

            tasks[idx].IsComplete = true;
            Log($"✅ Marked task as complete: {tasks[idx].Title}");
            return true;
        }

        // Task data structure
        private class TaskItem
        {
            public string Title { get; }
            public string Description { get; }
            public DateTime? DueDate { get; }
            public bool IsComplete { get; set; }

            public TaskItem(string title, string desc, DateTime? dueDate, bool isComplete)
            {
                Title = title;
                Description = desc;
                DueDate = dueDate;
                IsComplete = isComplete;
            }
        }
    }
}

