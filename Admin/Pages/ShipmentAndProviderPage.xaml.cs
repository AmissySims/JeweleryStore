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
    /// Логика взаимодействия для ShipmentAndProviderPage.xaml
    /// </summary>
    public partial class ShipmentAndProviderPage : Page
    {
        public ShipmentAndProviderPage()
        {
            InitializeComponent(); 
            ProvFrame.NavigationService.Navigate(new ShipmentsPage());
        }

        private void ShipmentsBt_Click(object sender, RoutedEventArgs e)
        {
            ProvFrame.NavigationService.Navigate(new ShipmentsPage());
        }

        private void ProvidersBt_Click(object sender, RoutedEventArgs e)
        {
            ProvFrame.NavigationService.Navigate(new ProvidersPage());
        }
    }
}
