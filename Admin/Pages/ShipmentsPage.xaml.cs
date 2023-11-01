using Admin.Pages.AddPages;
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
    /// Логика взаимодействия для ShipmentsPage.xaml
    /// </summary>
    public partial class ShipmentsPage : Page
    {
        public ShipmentsPage()
        {
            InitializeComponent(); var providers = App.db.Provider.ToList();
            providers.Insert(0, new Provider() { Title = "Все" });
            ProvCb.ItemsSource = providers;

            var stat = App.db.ShipmentStatus.ToList();
            stat.Insert(0, new ShipmentStatus() { Title = "Все" });
            StatusCb.ItemsSource = stat;



        }
        private void Refresh()
        {
            var prov = ProvCb.SelectedItem as Provider;
            var status = StatusCb.SelectedItem as ShipmentStatus;
            var shipment = App.db.Shipment.ToList();

            if (prov != null && prov.Id != 0)
            {
                shipment = shipment.Where(x => x.ProviderId == prov.Id).ToList();
            }

            if (status != null && status.Id != 0)
            {
                shipment = shipment.Where(x => x.ShipmentStatusId == status.Id).ToList();
            }

            ShipmentList.ItemsSource = shipment;

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void StatusCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void ProvCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddShipmentPage());
        }

        private void EditShipBt_Click(object sender, RoutedEventArgs e)
        {
            EditShipmentWin editShipment = new EditShipmentWin((sender as Button).DataContext as Shipment);
            editShipment.ShowDialog();
            Refresh();
        }
    }
}
