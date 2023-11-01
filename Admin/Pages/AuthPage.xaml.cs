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
using System.Windows.Navigation;
using System.Windows.Shapes;

using JeweleryStroreLibrary.Models;
using JeweleryStroreLibrary.Models.Partials;

namespace Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }
        private void EnterBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var login = LoginTb.Text.Trim();
                var password = PasswordTb.Text.Trim();
                AccountUser.AuthUser = App.db.User.ToList().Find(x => x.Login == login && x.Password == password && x.RoleId == 1);
                var user = AccountUser.AuthUser;
                if (user == null)
                {
                    MessageBox.Show("Такого пользователя не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(login))
                {
                    MessageBox.Show("Заполните поле логина", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Заполните поле пароля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    AccountUser.isAuth = true;
                    AccountUser.AuthUser = user;
                    MessageBox.Show("Вход выполнен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new MainPage());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}
