# CyberChatBotGUI
# Program Description: CyberChatBotGUI
CyberChatBotGUI is a desktop-based WPF application designed to simulate a conversational cybersecurity assistant. It combines educational elements (like a cybersecurity quiz), productivity tools (like a task manager and reminder system), and intelligent conversational interaction using basic natural language processing (NLP).

The application’s primary goals are:

To promote cybersecurity awareness in an engaging, interactive way.

To offer productivity features like reminders and task tracking.

To log all user interactions for auditing and review.

To provide mental/emotional check-ins, making the experience personal and supportive.

🌟 Key Features:
1. Cybersecurity ChatBot Interaction
Responds to cybersecurity-related keywords and general conversational inputs.

Uses NLP to detect intents like asking for help, requesting a quiz, or checking mood.

Offers predefined answers and random responses to keep the interaction dynamic.

2. Emotional Check-In
At startup, the bot greets the user and asks about their emotional state.

Uses this to respond empathetically, helping create a more human-centered experience.

3. Cybersecurity Quiz
Includes 10 questions in both multiple choice and true/false formats.

Offers immediate feedback: if a user answers incorrectly, it displays the correct answer along with an explanation.

At the end of the quiz, a score is shown along with encouragement or advice.

4. Task Manager
Users can add tasks by providing a title, description, and due date.

Tasks are stored and displayed to help users stay on top of their goals.

Can later be extended with edit/delete functionality.

5. Reminder System
Accepts natural phrases like “remind me in 3 days” or “remind me at 5pm tomorrow”.

Parses the text, sets a reminder using date/time recognition, and notifies the user at the appropriate time.

6. Activity Logging
All chatbot interactions, task additions, quiz results, and reminders are recorded in an activity log.

This log can be viewed to track progress and review previous actions.

7. Single-Window UI
All features and logic are embedded into MainWindow.xaml.cs, simplifying integration and management.

Uses color-coded text to distinguish between bot messages, user input, alerts, and quiz responses.

📘# User Manual: How to Use CyberChatBotGUI
💻 Launching the Application:
Build and run the application from Visual Studio.

The WPF window will load and display a welcome message from the chatbot.

👋 Getting Started:
The chatbot will greet you and ask how you're feeling.

Type how you feel (e.g., “I’m tired” or “I feel great”) and it will respond supportively.

🧠 Using the ChatBot:
Ask general questions like:

“What is phishing?”

“Give me cybersecurity tips”

“Tell me a fun fact”

The bot responds with relevant, predefined answers or randomly selected feedback.

Try phrases like:

“Start quiz”

“Add task”

“Remind me in 2 days”

🧩 Taking the Quiz:
Type: start quiz

The bot will begin a 10-question quiz.

Answer by typing A, B, C, D or True/False.

After each question, you’ll get immediate feedback.

At the end, your total score and a custom message will be displayed.

📝 Adding Tasks:
Type: add task

You will be prompted to:

Enter the task title

Describe the task

Provide a due date

The task is saved and can be recalled or displayed (if implemented).

⏰ Setting Reminders:
Type something like:

“Remind me to submit my report in 3 days”

“Remind me to check email at 5pm”

The bot will recognize the time, schedule it, and alert you appropriately.

📜 Viewing Activity Logs:
All interactions are stored in an activity log.

This can be displayed in a log section (if UI provides a view), or saved to a file (if implemented).

🔄 Closing and Reopening:
When reopened, the chatbot can be extended to read back previous logs, reminders, or tasks if persistence is implemented.

