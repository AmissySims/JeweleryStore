﻿using System;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
   
        public MainPage()
        {
            InitializeComponent();
            Main1Frame.NavigationService.Navigate(new MainMainPage());
        }

        private void ExitBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void AccountBt_Click(object sender, RoutedEventArgs e)
        {
            Main1Frame.NavigationService.Navigate(new AccountPage());
        }

        private void MainBt_Click(object sender, RoutedEventArgs e)
        {
            Main1Frame.NavigationService.Navigate(new MainMainPage());
        }

        private void ProductBt_Click(object sender, RoutedEventArgs e)
        {
            Main1Frame.NavigationService.Navigate(new ProductsPage());
        }

        private void UsersBt_Click(object sender, RoutedEventArgs e)
        {
            Main1Frame.NavigationService.Navigate(new UsersPage());
        }

        private void ShipmentsBt_Click(object sender, RoutedEventArgs e)
        {
            Main1Frame.NavigationService.Navigate(new ShipmentAndProviderPage());
        }

        private void OrdersBt_Click(object sender, RoutedEventArgs e)
        {
            Main1Frame.NavigationService.Navigate(new OrdersPage());
        }

       
        private void CategoryBt_Click(object sender, RoutedEventArgs e)
        {
            Main1Frame.NavigationService.Navigate(new CategoriesPage());
        }

        private void DeliveryPountsBt_Click(object sender, RoutedEventArgs e)
        {
            Main1Frame.NavigationService.Navigate(new DeliveryPointsPage());
        }

        private void BusketBt_Click(object sender, RoutedEventArgs e)
        {
            Main1Frame.NavigationService.Navigate(new BusketPage());
        }
    }
}
