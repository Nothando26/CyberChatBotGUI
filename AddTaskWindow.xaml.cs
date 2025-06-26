using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace CyberChatBotPOEPart1
{
    public partial class AddTaskWindow : Window
    {
        // ✅ Task list
        private List<TaskItem> tasks = new List<TaskItem>();

        // ✅ Reminder list
        private List<(string Message, DateTime Time)> reminders = new List<(string, DateTime)>();

        public AddTaskWindow()
        {
            InitializeComponent();
        }

        // ✅ Add task logic
        private bool HandleAddTask(string input)
        {
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
                    return true;
                }
                dueDate = parsedDue;
            }

            tasks.Add(new TaskItem(title, desc, dueDate, false));
            Log($"✅ Task added: {title} - {desc} {(dueDate.HasValue ? $"(Due: {dueDate.Value.ToShortDateString()})" : "")}");

            // ✅ Parse reminder
            if (!string.IsNullOrEmpty(reminderStr))
            {
                var remMatch = Regex.Match(reminderStr.ToLower(), @"in (\d+) (seconds|minutes|hours|days|weeks)");
                if (remMatch.Success)
                {
                    int qty = int.Parse(remMatch.Groups[1].Value);
                    string unit = remMatch.Groups[2].Value;

                    DateTime reminderTime = DateTime.Now;
                    switch (unit)
                    {
                        case "seconds": reminderTime = reminderTime.AddSeconds(qty); break;
                        case "minutes": reminderTime = reminderTime.AddMinutes(qty); break;
                        case "hours": reminderTime = reminderTime.AddHours(qty); break;
                        case "days": reminderTime = reminderTime.AddDays(qty); break;
                        case "weeks": reminderTime = reminderTime.AddDays(qty * 7); break;
                    }

                    reminders.Add(($"Task reminder: {title}", reminderTime));
                    Log($"⏰ Reminder set for task '{title}' in {qty} {unit}.");
                }
                else
                {
                    Log("❌ Could not parse reminder time. Use 'in X days/hours/minutes'.");
                }
            }

            return true;
        }

        // ✅ Show all tasks
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

        // ✅ Delete a task
        private bool HandleDeleteTask(string input)
        {
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

        // ✅ Mark task complete
        private bool HandleCompleteTask(string input)
        {
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

        // ✅ Logging helper method (you can redirect this to a textbox or console output)
        private void Log(string message)
        {
            MessageBox.Show(message); // Replace with output to UI component if needed
        }

        // ✅ Task structure
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
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleTextBox.Text.Trim();
            string desc = TaskDescriptionTextBox.Text.Trim();
            string dueStr = TaskDueDateTextBox.Text.Trim();

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(desc))
            {
                Log("❌ Title and Description cannot be empty.");
                return;
            }

            DateTime? dueDate = null;
            if (!string.IsNullOrEmpty(dueStr))
            {
                if (!DateTime.TryParse(dueStr, out DateTime parsedDue))
                {
                    Log("❌ Invalid due date format. Use yyyy-MM-dd or leave empty.");
                    return;
                }
                dueDate = parsedDue;
            }

            tasks.Add(new TaskItem(title, desc, dueDate, false));
            Log($"✅ Task added: {title} - {desc}" + (dueDate.HasValue ? $" (Due: {dueDate.Value.ToShortDateString()})" : ""));

            // Clear inputs after adding
            TaskTitleTextBox.Clear();
            TaskDescriptionTextBox.Clear();
            TaskDueDateTextBox.Clear();
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TaskNumberTextBox.Text.Trim(), out int idx))
            {
                Log("❌ Please enter a valid task number.");
                return;
            }

            idx -= 1; // zero-based index

            if (idx < 0 || idx >= tasks.Count)
            {
                Log("❌ Invalid task number.");
                return;
            }

            var removed = tasks[idx];
            tasks.RemoveAt(idx);
            Log($"🗑️ Deleted task: {removed.Title}");

            TaskNumberTextBox.Clear();
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TaskNumberTextBox.Text.Trim(), out int idx))
            {
                Log("❌ Please enter a valid task number.");
                return;
            }

            idx -= 1; // zero-based index

            if (idx < 0 || idx >= tasks.Count)
            {
                Log("❌ Invalid task number.");
                return;
            }

            tasks[idx].IsComplete = true;
            Log($"✅ Marked task as complete: {tasks[idx].Title}");

            TaskNumberTextBox.Clear();
        }

        private void ShowTasks_Click(object sender, RoutedEventArgs e)
        {
            ShowTasks();
        }
    }
}
