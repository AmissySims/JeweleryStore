﻿using JeweleryStroreLibrary.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Admin.Pages.AddPages
{
    /// <summary>
    /// Логика взаимодействия для AddEditUserPage.xaml
    /// </summary>
    public partial class AddEditUserPage : Page
    {
        User contextUser;
        DbPropertyValues oldValues;
        public AddEditUserPage(User user)
        {
            InitializeComponent();
            var role = App.db.Role.ToList();
            RoleCb.ItemsSource = role;
            contextUser = user;
            DataContext = contextUser;

            if (contextUser.Id != 0)
            {
                oldValues = App.db.Entry(contextUser).CurrentValues.Clone();
            }
            if (contextUser.Id == 0)
            {
                RoleStack.Visibility = Visibility.Visible;
            }
            else
            {
                RoleStack.Visibility = Visibility.Collapsed;
            }
        }

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Regex.IsMatch(contextUser.Phone, @"^((\+?7|8)[ -]?)?([(]?\d[- ]?[()]?){10}$") && Regex.IsMatch(contextUser.Phone, @"^(\+?7|8)?[\s(-]?[(-]?\d{3,4}[)-]?[ )-]?\d{2,7}[ -]?\d{2,4}[ -]?\d{0,2}$"))
                {
                    if (string.IsNullOrWhiteSpace(contextUser.Phone))
                    {
                        MessageBox.Show("Заполните поле телефона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(contextUser.FirstName))
                    {
                        MessageBox.Show("Заполните поле фамилии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(contextUser.LastName))
                    {
                        MessageBox.Show("Заполните поле имени", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(contextUser.MiddleName))
                    {
                        MessageBox.Show("Заполните поле отчества", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(contextUser.Login))
                    {
                        MessageBox.Show("Заполните поле логина", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(contextUser.Password))
                    {
                        MessageBox.Show("Заполните поле пароля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (contextUser.Role == null)
                    {
                        MessageBox.Show("Выберите роль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        if (contextUser.Id == 0)
                        {
                            App.db.User.Add(contextUser);
                        }
                        App.db.SaveChanges();
                        MessageBox.Show("Сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.GoBack();

                    }
                }
                else
                {
                    MessageBox.Show($" Проверьте  телефон {contextUser.Phone} на корректность", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddImageBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    contextUser.Photo = File.ReadAllBytes(dialog.FileName);
                    DataContext = null;
                    DataContext = contextUser;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (oldValues != null)
                {
                    App.db.Entry(contextUser).CurrentValues.SetValues(oldValues);

                }
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


    }

}
