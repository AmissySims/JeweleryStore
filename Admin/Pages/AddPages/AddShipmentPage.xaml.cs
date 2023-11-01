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

namespace Admin.Pages.AddPages
{
    /// <summary>
    /// Логика взаимодействия для AddShipmentPage.xaml
    /// </summary>
    public partial class AddShipmentPage : Page
    {
        public AddShipmentPage()
        {
            InitializeComponent();
            var prov = App.db.Provider.ToList();
            Provcb.ItemsSource = prov;
            var prod = App.db.Product.ToList();
            ProdCb.ItemsSource = prod;





        }

        private void AddShipBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (Provcb.SelectedIndex != -1 && CountTb.Text != "" && ProdCb.SelectedIndex != -1)
                {
                    Shipment ship = new Shipment();
                    {
                        ship.Product = (ProdCb.SelectedItem as Product);
                        ship.Provider = (Provcb.SelectedItem as Provider);
                        ship.Date = DateTime.Now;
                        ship.Count = Convert.ToInt32(CountTb.Text);
                        ship.ShipmentStatusId = 1;
                        ship.DateToCome = null;
                    }
                    App.db.Shipment.Add(ship);
                    App.db.SaveChanges();
                    MessageBox.Show("Оформлено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new ShipmentsPage());
                }
                else
                    MessageBox.Show("Заполните поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ShipmentsPage());
        }
    }
}
