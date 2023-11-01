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
    /// Логика взаимодействия для DeliveryPointsPage.xaml
    /// </summary>
    public partial class DeliveryPointsPage : Page
    {
        public DeliveryPointsPage()
        {
            InitializeComponent();
        }
        private void Refresh()
        {
            PointsList.ItemsSource = App.db.DeliveryPoint.ToList();

        }
        private void AddPointBt_Click(object sender, RoutedEventArgs e)
        {
            AddEditDeliveryPointWin addEditDelivery = new AddEditDeliveryPointWin(new DeliveryPoint());
            addEditDelivery.ShowDialog();
            Refresh();
        }

        private void EditPoint_Click(object sender, RoutedEventArgs e)
        {
            AddEditDeliveryPointWin addEditDelivery = new AddEditDeliveryPointWin((sender as Button).DataContext as DeliveryPoint);
            addEditDelivery.ShowDialog();
            Refresh();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();

        }
    }
}
