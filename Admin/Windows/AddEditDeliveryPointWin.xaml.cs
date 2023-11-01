using JeweleryStroreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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

namespace Admin.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditDeliveryPointWin.xaml
    /// </summary>
    public partial class AddEditDeliveryPointWin : Window
    {
        DeliveryPoint contextPoint;
        DbPropertyValues oldValues;
        public AddEditDeliveryPointWin(DeliveryPoint point)
        {
            InitializeComponent();
            var users = App.db.User.Where(x => x.RoleId == 4).ToList();
            RoleCb.ItemsSource = users;
            contextPoint = point;
            DataContext = contextPoint;

            if (contextPoint.Id != 0)
            {
                oldValues = App.db.Entry(contextPoint).CurrentValues.Clone();
            }
        }

        private void WindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = true;
        }
        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {

            if (oldValues != null)
            {
                App.db.Entry(contextPoint).CurrentValues.SetValues(oldValues);

            }
            DialogResult = true;
        }

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(contextPoint.Adress))
                {
                    MessageBox.Show("Заполните поле адреса", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (contextPoint.User == null)
                {
                    MessageBox.Show("Выберите работника", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (contextPoint.Id == 0)
                    {
                        App.db.DeliveryPoint.Add(contextPoint);
                    }
                    App.db.SaveChanges();
                    MessageBox.Show("Сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
