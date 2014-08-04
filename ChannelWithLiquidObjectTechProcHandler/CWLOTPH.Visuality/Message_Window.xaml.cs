using System.Windows;

namespace CWLOTPH.Visuality
{
    /// <summary>
    /// Логика взаимодействия для Message_Window.xaml
    /// </summary>
    public partial class MessageWindow
    {
        public static MessageResult Result = MessageResult.None;
        private static MessageWindow _messageWindow;

        public enum MessageResult{
            None,Ok,Cancel
        }
        public MessageWindow()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            if (_messageWindow == null) return;
            Result = MessageResult.Cancel;
            _messageWindow.Close();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (_messageWindow == null) return;
            Result = MessageResult.Ok;
            _messageWindow.Close();
        }

        public static void Show(string message)
        {
            _messageWindow = new MessageWindow();
            var realMessage = FormatMessage(message);
            _messageWindow.textLabel.Content = realMessage;
            _messageWindow.ShowDialog();
        }

        private static string FormatMessage(string message)
        {
            var realMessage = "";
            if (message.Length > 36)
            {
                for (var i = 0; i < message.Length; i++)
                {
                    realMessage += message[i];
                    if (i % 36 == 0&i!=0)
                        realMessage += "\n";
                }
            }
            else
            {
                realMessage = message;
            }
            return realMessage;
        }
    }
}
