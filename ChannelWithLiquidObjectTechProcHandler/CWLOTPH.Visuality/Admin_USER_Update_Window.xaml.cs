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
using System.Security.Cryptography;
using CWLOTPH.DB;

namespace CWLOTPH.Visuality
{
    /// <summary>
    /// Логика взаимодействия для Admin_USER_Update_Window.xaml
    /// </summary>
    public partial class Admin_USER_Update_Window : Window
    {
        public Admin_USER_Update_Window(string userLogin)
        {
            InitializeComponent();
            loginTextBox.Text = userLogin;
            User _user = DbInteraсtion.getUser(userLogin);
            idTextBox.Text = _user.ID.ToString();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (passwordTextBox.Text != "" & passwordTextBox.Text.Length>4)         // Если введенный пароль не пустой и длиннее 4 символов
                {
                    User _user = new User();                                            // Создаем экземпляр класса пользователя
                    AdditiveData _aData = null;
                    _user.Login = loginTextBox.Text;                                    // Устанавливаем имя
                    _user.ID = Convert.ToDecimal(idTextBox.Text);                       // Устанавливаем ID
                    _aData = DbInteraсtion.getUser(_user.Login).AdditiveData;           // Получаем доп. данные
                    _aData.Salt = RandomString(10);                                     // Получаем Новую Соль
                    _user.AdditiveData = _aData;
                    foreach (byte b
                        in new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(
                        passwordTextBox.Text.ToString() + _aData.Salt)))         // Хэшируем
                    {
                        _user.Password += b.ToString("X2");                             // Устанавливаем пароль
                    }
                    MessageWindow.Show(string.Format("Вы действительно хотите изменить данные пользователя {0}", // Проверка на необходимость изменений
                    _user.Login) +
                    "Нажмите ОК, для подтверждения или крестик в правом верхнем углу экрана для отмены");
                    if (MessageWindow.Result == MessageWindow.MessageResult.Ok)                           // Если ОК
                    {
                        DbInteraсtion.updateUser(_user);                                                    // Обновляем
                        MessageWindow.Show(string.Format("Пользователь {0} успешно изменен",              // Уведомляем пользователя
                        _user.Login));
                    }
                    Close();
                }
            }
            catch (FormatException)
            {
                MessageWindow.Show("Неправильный формат ввода");                                            // Отлавливаем неправильный формат ввода
            }
        }

        public string RandomString(int size)                 // Функция генерации случайной строки
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                //Генерируем число являющееся латинским символом в юникоде
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                //Конструируем строку со случайно сгенерированными символами
                builder.Append(ch);
            }
            return builder.ToString();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
