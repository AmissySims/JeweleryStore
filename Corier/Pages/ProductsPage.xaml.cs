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

namespace Corier.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        CategoryProduct selCateg;
        public ProductsPage()
        {
            InitializeComponent();

            selCateg = null;

            FilterCb.Items.Add("Все");
            FilterCb.Items.Add("до 500");
            FilterCb.Items.Add("от 500 до 2000");
            FilterCb.Items.Add("от 2000 до 5000");
            FilterCb.Items.Add("от 5000");

            SortCb.Items.Add("Все");
            SortCb.Items.Add("от А до Я");
            SortCb.Items.Add("от Я до А");
            SortCb.Items.Add("По цене мин.");
            SortCb.Items.Add("По цене макс.");
        }

        private void Refresh()
        {
            var found = FoundTb.Text.ToLower();
            var prod = App.db.Product.ToList();
            if (!string.IsNullOrEmpty(found))
            {
                prod = App.db.Product.Where(x => x.Title.ToLower().Contains(found)).ToList();
            }
            if (selCateg != null)
            {
                prod = prod.Where(x => x.CategoryProductId == selCateg.Id).ToList();
            }

            if (FilterCb.SelectedIndex == 1)
            {
                prod = prod.Where(x => x.Price < 500).ToList();
            }
            if (FilterCb.SelectedIndex == 2)
            {
                prod = prod.Where(x => x.Price >= 500 && x.Price < 2000).ToList();
            }
            if (FilterCb.SelectedIndex == 3)
            {
                prod = prod.Where(x => x.Price >= 2000 && x.Price < 5000).ToList();
            }
            if (FilterCb.SelectedIndex == 4)
            {
                prod = prod.Where(x => x.Price >= 5000).ToList();
            }

            if (SortCb.SelectedIndex == 1)
            {
                prod = prod.OrderBy(x => x.Title).ToList();
            }
            if (SortCb.SelectedIndex == 2)
            {
                prod = prod.OrderByDescending(x => x.Title).ToList();
            }
            if (SortCb.SelectedIndex == 3)
            {
                prod = prod.OrderBy(x => x.Price).ToList();
            }
            if (SortCb.SelectedIndex == 1)
            {
                prod = prod.OrderByDescending(x => x.Price).ToList();
            }

            ProductList.ItemsSource = prod;
        }

        private void LookBt_Click(object sender, RoutedEventArgs e)
        {
            var selProd = (sender as Button).DataContext as Product;
            NavigationService.Navigate(new LookPage(selProd));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void TextileBt_Click(object sender, RoutedEventArgs e)
        {
            selCateg = App.db.CategoryProduct.FirstOrDefault(x => x.Id == 1);
            Refresh();
        }

        private void DecorBt_Click(object sender, RoutedEventArgs e)
        {
            selCateg = App.db.CategoryProduct.FirstOrDefault(x => x.Id == 2);
            Refresh();
        }

        private void LightBt_Click(object sender, RoutedEventArgs e)
        {
            selCateg = App.db.CategoryProduct.FirstOrDefault(x => x.Id == 3);
            Refresh();
        }

        private void DishBt_Click(object sender, RoutedEventArgs e)
        {
            selCateg = App.db.CategoryProduct.FirstOrDefault(x => x.Id == 4);
            Refresh();
        }

        private void FlowBt_Click(object sender, RoutedEventArgs e)
        {
            selCateg = App.db.CategoryProduct.FirstOrDefault(x => x.Id == 5);
            Refresh();
        }

        private void FurnitureBt_Click(object sender, RoutedEventArgs e)
        {
            selCateg = App.db.CategoryProduct.FirstOrDefault(x => x.Id == 6);
            Refresh();
        }

        private void FilterCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void FoundTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }



        private void AllBt_Click(object sender, RoutedEventArgs e)
        {
            selCateg = null;
            Refresh();
        }
    }
}
