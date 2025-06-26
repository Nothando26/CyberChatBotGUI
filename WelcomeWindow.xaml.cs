using System.Windows;

namespace CyberChatBotPOEPart1
{
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void StartChatting_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserNameTextBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(userName))
            {
                MainWindow mainWindow = new MainWindow(userName);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter your name to continue.", "Missing Name", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
