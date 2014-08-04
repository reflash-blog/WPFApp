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
    /// Логика взаимодействия для Admin_USER_Add.xaml
    /// </summary>
    public partial class Admin_USER_Add : Window
    {
        public Admin_USER_Add()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginTextBox.Text != "" & passwordTextBox.Text != "")           // Если поле логина и пароля не пустые
            {
                if (passwordTextBox.Text.Length > 4 )                           // Если пароль более 4 символов
                {
                    User user = new User();                                     // Создаем пользователя
                    AdditiveData aData = new AdditiveData();                    // Создаем доп. данные

                    user.Login = loginTextBox.Text.ToString();                  // Устанавливаем Login

                    aData.ID = user.ID;                                         // Устанавливаем ID для доп. данных
                    aData.Salt = RandomString(10);                              // Получаем Соль
                    user.AdditiveData = aData;
                    MD5 md5 = new MD5CryptoServiceProvider();
                    //compute hash from the bytes of text
                    byte[] checkSum = ASCIIEncoding.ASCII.GetBytes(passwordTextBox.Text + user.AdditiveData.Salt);
                    md5.ComputeHash(checkSum);
                    byte[] result = md5.Hash;
                    string password = BitConverter.ToString(result).Replace("-", String.Empty);
                    user.Password = password.ToString();
                    DbInteraсtion.addUser(user);                         // Добавляем пользователя в БД
                    MessageWindow.Show(string.Format("Пользователь {0} успешно добавлен в базу данных", // Уведомляем
                    user.Login));
                    clearTextBoxes();                                           // Очищаем текстовые поля

                }
                else
                {
                    MessageWindow.Show("Длина пароля должна быть более чем 4 символа");
                }
            }
        }

        private void clearTextBoxes()
        {
            loginTextBox.Text = "";
            passwordTextBox.Text = "";
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
    }
}
