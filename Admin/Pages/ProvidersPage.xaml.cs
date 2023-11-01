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
    /// Логика взаимодействия для ProvidersPage.xaml
    /// </summary>
    public partial class ProvidersPage : Page
    {
        public ProvidersPage()
        {
            InitializeComponent(); SortCb.Items.Add("Все");
            SortCb.Items.Add("от А до Я");
            SortCb.Items.Add("от Я до А");

        }
        private void Refresh()
        {
            var found = FoundTb.Text.ToLower();
            var prov = App.db.Provider.ToList();
            if (!string.IsNullOrEmpty(found))
            {
                prov = prov.Where(x => x.Title.ToLower().Contains(found)).ToList();
            }
            if (SortCb.SelectedIndex == 1)
            {
                prov = prov.OrderBy(x => x.Title).ToList();
            }
            if (SortCb.SelectedIndex == 2)
            {
                prov = prov.OrderByDescending(x => x.Title).ToList();
            }
            ProvList.ItemsSource = prov;
        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void FoundTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();

        }

        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            AddEditProviderWin selProv = new AddEditProviderWin(new Provider());
            selProv.ShowDialog();
            Refresh();
        }

        private void EditBt_Click(object sender, RoutedEventArgs e)
        {
            AddEditProviderWin selProv = new AddEditProviderWin((sender as Button).DataContext as Provider);
            selProv.ShowDialog();
            Refresh();
        }

        private void DeleteBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selProv = (sender as Button).DataContext as Provider;
                var selShip = App.db.Shipment.Where(x => x.ProviderId == selProv.Id) as Shipment;
                var selProd = App.db.ShipmentProduct.Where(x => x.ShipmentId == selShip.Id);
                App.db.ShipmentProduct.RemoveRange(selProd);
                App.db.Shipment.Remove(selShip);
                App.db.Provider.Remove(selProv);
                App.db.SaveChanges();
                MessageBox.Show("Удалено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
