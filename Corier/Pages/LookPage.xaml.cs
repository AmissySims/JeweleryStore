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
    }
}
