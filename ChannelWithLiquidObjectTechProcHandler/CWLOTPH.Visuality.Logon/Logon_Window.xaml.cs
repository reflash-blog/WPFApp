using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using CWLOTPH.DB;
using CWLOTPH.Process;

namespace CWLOTPH.Visuality.Logon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LogonWindow
    {
        
        public LogonWindow()
        {
            InitializeComponent();
            PreviewKeyDown += Window_KeyDown;   // Проверка на нажатие клавиши
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.Key == Key.Enter)
           {
               okButton_Click(null, null);           // Нажатие Enter эквивалентно нажатию кнопки OK
           }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {

            if (loginTextBox.Text == "admin" && passwordTextBox.Password == "admin")       // Стандартный пароль администратора
            {
                LoadWindowProcessing();
                var admWindow = new AdminWindow();
                admWindow.Show();
                Close();
            }
            else
            {
                if (loginTextBox.Text == "user" && passwordTextBox.Password == "user")    // Стандартный пароль пользователя
                {
                    LoadWindowProcessing();
                    var pController = new ProcessController();
                    pController.initProcessController();
                    Close();
                }
                else
                {
                    var user = DbInteraсtion.getUser(loginTextBox.Text);
                    MD5 md5 = new MD5CryptoServiceProvider();
                    //compute hash from the bytes of text
                    var checkSum = Encoding.ASCII.GetBytes(passwordTextBox.Password + user.AdditiveData.Salt.Replace(" ", string.Empty));
                    md5.ComputeHash(checkSum);
                    var result = md5.Hash;
                    var password = BitConverter.ToString(result).Replace("-", String.Empty);
                    if (password == user.Password)
                    {
                        LoadWindowProcessing();
                        var pController = new ProcessController();
                        pController.initProcessController();
                        Close();
                    }
                    else
                    {
                        MessageWindow.Show("Неверный логин или пароль");
                    }

                }
            }
        }

        private void LoadWindowProcessing()
        {
            Visibility = Visibility.Hidden;
            var loadWindow = Load_Window.Initialize();
            loadWindow.ShowLoadGUI();
            while (loadWindow.IsActive)
            {
                Thread.Sleep(1);
            }
        }
    }
}
