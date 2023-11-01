using JeweleryStroreLibrary.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
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
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        Product contextProd;

        DbPropertyValues oldValues;
        public AddEditProductPage(Product prod)
        {
            InitializeComponent();
            CategoryCb.ItemsSource = App.db.CategoryProduct.ToList();
            contextProd = prod;
            DataContext = contextProd;
            if (contextProd.Id != 0)
            {
                oldValues = App.db.Entry(contextProd).CurrentValues.Clone();
            }
            Refresh();
        }
        public void Refresh()
        {
            ImageList.ItemsSource = contextProd.ProductPhoto.ToList();
        }
        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(contextProd.Title))
                {
                    MessageBox.Show("Заполните поле названия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(contextProd.Description))
                {
                    MessageBox.Show("Заполните поле описания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (contextProd.Price == 0 || PriceTb.Text.Length <= 0)
                {
                    MessageBox.Show("Заполните поле стоимости", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (contextProd.Count < 0 || CountTb.Text.Length <= 0)
                {
                    MessageBox.Show("Заполните поле количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (contextProd.CategoryProduct == null)
                {
                    MessageBox.Show("Выберите категорию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (contextProd.Id == 0)
                    {
                        App.db.Product.Add(contextProd);


                    }
                    App.db.SaveChanges();
                    MessageBox.Show("Сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (oldValues != null)
                {
                    App.db.Entry(contextProd).CurrentValues.SetValues(oldValues);
                }
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog() { Multiselect = true };
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    if (dialog.FileNames.Length > 0)
                    {
                        foreach (var item in dialog.FileNames)
                        {
                            contextProd.ProductPhoto.Add(new ProductPhoto()
                            {
                                Image = File.ReadAllBytes(item),
                                Product = contextProd
                            });

                        }
                        Refresh();
                        DataContext = null;
                        DataContext = contextProd;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selImage = ImageList.SelectedItem as ProductPhoto;
                if (selImage != null)
                {

                    App.db.ProductPhoto.Remove(selImage);
                    App.db.SaveChanges();
                    Refresh();

                }
                else
                {
                    MessageBox.Show("Выберите изображение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PriceTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}

