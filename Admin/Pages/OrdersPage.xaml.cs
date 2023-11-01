using Admin.Windows;
using JeweleryStroreLibrary.Models;
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

namespace Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent(); var stat = App.db.OrdStatus.ToList();
            stat.Insert(0, new OrdStatus() { Title = "Все" });
            StatusCb.ItemsSource = stat;

            var use = App.db.User.Where(x => x.RoleId == 1 || x.RoleId == 2).ToList();
            use.Insert(0, new User() { FirstName = "Все" });
            UserCb.ItemsSource = use;
        }

        private void Refresh()
        {
            var users = UserCb.SelectedItem as User;
            var status = StatusCb.SelectedItem as OrdStatus;
            var ord = App.db.Order.Where(x => x.User.RoleId == 1 || x.User.RoleId == 2).ToList();

            if (users != null && users.Id != 0)
            {
                ord = ord.Where(x => x.UserId == users.Id).ToList();
            }

            if (status != null && status.Id != 0)
            {
                ord = ord.Where(x => x.OrdStatusId == status.Id).ToList();
            }

            OrdersList.ItemsSource = ord;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void StatusCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void UserCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void EditShipBt_Click(object sender, RoutedEventArgs e)
        {
            EditOrderWin editOrder = new EditOrderWin((sender as Button).DataContext as Order);
            editOrder.ShowDialog();
            Refresh();
        }
    }
}
