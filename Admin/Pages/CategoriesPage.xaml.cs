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
using Admin.Windows;

namespace Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        public CategoriesPage()
        {
            InitializeComponent(); SortCb.Items.Add("Все");
            SortCb.Items.Add("от А до я");
            SortCb.Items.Add("от Я до А");

        }

        public void Refresh()
        {
            var search = FoundTb.Text.ToLower();
            var cat = App.db.CategoryProduct.ToList();
            if (!string.IsNullOrEmpty(search))
            {
                cat = cat.Where(x => x.Title.ToLower().Contains(search)).ToList();
            }
            if (SortCb.SelectedIndex == 1)
            {
                cat = cat.OrderBy(x => x.Title).ToList();
            }
            if (SortCb.SelectedIndex == 2)
            {
                cat = cat.OrderByDescending(x => x.Title).ToList();
            }
            CatList.ItemsSource = cat;
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
            AddEditCategoryWin editCategory = new AddEditCategoryWin(new CategoryProduct());
            editCategory.ShowDialog();
            Refresh();
        }

        private void DeleteBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selCat = (sender as Button).DataContext as CategoryProduct;
                var selProd = App.db.Product.Where(x => x.CategoryProductId == selCat.Id);
                App.db.Product.RemoveRange(selProd);
                App.db.CategoryProduct.Remove(selCat);
                App.db.SaveChanges();
                MessageBox.Show("Удалено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void EditBt_Click(object sender, RoutedEventArgs e)
        {
            AddEditCategoryWin editCategory = new AddEditCategoryWin((sender as Button).DataContext as CategoryProduct);
            editCategory.ShowDialog();
            Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
