using JeweleryStroreLibrary.Models.Partials;
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
    /// Логика взаимодействия для LookPage.xaml
    /// </summary>
    public partial class LookPage : Page
    {
        Product contextProduct;
        public LookPage(Product prod)
        {
            InitializeComponent();
            contextProduct = prod;
            DataContext = contextProduct;
            Refresh();

        }

        public void Refresh()
        {
            ImageList.ItemsSource = contextProduct.ProductPhoto.ToList();
        }
        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BusketBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedProduct = (sender as Button).DataContext as Product;

                Busket bucket = new Busket
                {
                    Count = 1,
                    UserId = AccountUser.AuthUser.Id,
                    ProductId = selectedProduct.Id
                };

                var prodInBucket = App.db.Busket.Where(b => b.ProductId == bucket.ProductId).FirstOrDefault();
                if (prodInBucket != null) { MessageBox.Show("Данный товар уже присутствует в корзине", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information); return; };

                App.db.Busket.Add(bucket);
                App.db.SaveChanges();
                MessageBoxResult result = MessageBox.Show("Товар добавлен в корзину. Хотите перейти в корзину сейчас?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);


                if (result == MessageBoxResult.Yes)
                {
                    NavigationService.Navigate(new BusketPage());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении в корзину: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
