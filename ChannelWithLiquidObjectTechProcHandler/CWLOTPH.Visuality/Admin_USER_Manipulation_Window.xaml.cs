using System;
using System.Collections.Generic;
using System.Globalization;
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
using CWLOTPH.DB;

namespace CWLOTPH.Visuality
{
    /// <summary>
    /// Логика взаимодействия для Admin_USER_Manipulation_Window.xaml
    /// </summary>
    public partial class Admin_USER_Manipulation_Window : Window
    {
        public Admin_USER_Manipulation_Window()
        {
            InitializeComponent();
            RefreshLoginComboBox();                       // Обновляем комбо бокс логина
        }

        private void RefreshLoginComboBox()
        {
            var _userList = DbInteraсtion.getAllUserRecords();  // Получаем всех пользователей
            var _userNamesList = _userList.Select(user => user.Login).ToList();          // Формируем список логинов
            loginComboBox.ItemsSource = _userNamesList;                // Выполняем привязку данных
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            string _userLogin = loginComboBox.SelectedItem.ToString();          // Присваиваем выбранный логин
            if (_userLogin != "")
            {
                Admin_USER_Update_Window _adm_UR_upd_win = new Admin_USER_Update_Window(_userLogin); // Форма изменения
                _adm_UR_upd_win.ShowInTaskbar = false; // Не показывать в панели задач
                _adm_UR_upd_win.ShowDialog();          // Вызываем
                clearTextBoxes();
            }
        }

        private void clearTextBoxes()
        {
            idTextBox.Text = "";
            loginComboBox.SelectedIndex = -1;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            User _user = DbInteraсtion.getUser(loginComboBox.SelectedItem.ToString()); // Получаем пользователя

            MessageWindow.Show(string.Format("Вы действительно хотите удалить пользователя {0}", // Запрашиваем подтверждение удаления
            _user.Login) + "Нажмите ОК, для подтверждения или крестик в правом верхнем углу экрана для отмены");
            if (MessageWindow.Result == MessageWindow.MessageResult.Ok)                      // Если ОК
            { 
                DbInteraсtion.deleteUser(_user);                                          // Удалить пользователя
                MessageWindow.Show(string.Format("Пользователь {0} успешно удален из базы данных", // Уведомить
                _user.Login));
                clearTextBoxes();                                                              // Очистить текстовые поля
                RefreshLoginComboBox();
            }
        }

        private void loginComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loginComboBox.SelectedIndex >= 0)                                         // Если индекс не пустой
            {
                User _user = DbInteraсtion.getUser(loginComboBox.SelectedItem.ToString()); // Получить пользователя
                idTextBox.Text = _user.ID.ToString(CultureInfo.InvariantCulture);                                      // Заполнить поле
            }
        }
    }
}
