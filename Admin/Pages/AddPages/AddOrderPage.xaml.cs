using Admin.Windows;
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

namespace Admin.Pages.AddPages
{
    /// <summary>
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Page
    {
        public List<BuscketItem> Busket { get; set; }
        public AddOrderPage(List<BuscketItem> bucketList)
        {

            InitializeComponent();
            Busket = bucketList;
            var points = App.db.DeliveryPoint.ToList();
            DeliveryPointCb.ItemsSource = points;

            DateTb.Text = DateTime.Now.ToString("dd.MM.yyyy");

            NameTb.Text = AccountUser.AuthUser.FullName;
            Adress.Visibility = Visibility.Hidden;
            IfPickup.Visibility = Visibility.Hidden;
            var cards = App.db.Card.Where(x => x.UserId == AccountUser.AuthUser.Id).ToList();
            CardCb.ItemsSource = cards;
            PriceTb.Text = $"{Convert.ToString(Busket.Sum(b => b.Count * b.Product.Price)) + " ₽"}";
        }

        private void Pickup_Checked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Visible;
            Adress.Visibility = Visibility.Hidden;
        }

        private void Pickup_Unchecked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Visible;
            Adress.Visibility = Visibility.Visible;
        }

        private void Courier_Checked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Hidden;
            Adress.Visibility = Visibility.Visible;
        }

        private void Courier_Unchecked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Visible;
        }

        private void CardCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddCardBt_Click(object sender, RoutedEventArgs e)
        {
            AddCardWin addCard = new AddCardWin(new Card());
            addCard.ShowDialog();
            var cards = App.db.Card.Where(x => x.UserId == AccountUser.AuthUser.Id).ToList();
            CardCb.ItemsSource = cards;

        }

        private void DeliveryPointCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OrderBt_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.db.Busket.ToList().Count <= 0)
                {
                    MessageBox.Show("Ваша корзина пуста", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var selCard = (CardCb.SelectedItem as Card);
                if (selCard.Balance < Busket.Sum(b => b.Count * b.Product.Price))
                {
                    MessageBox.Show("На карте недостаточно средств! Выберите другую карту", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Order ord = new Order();
                {
                    ord.UserId = AccountUser.AuthUser.Id;

                    ord.OrdStatusId = 1;

                    if (Courier.IsChecked == true)
                    {
                        ord.DeliveryTypeId = 2;
                        ord.DeliveryPointId = null;
                        ord.AdressToDelivery = AdressTb.Text;
                    }
                    else if (Pickup.IsChecked == true)
                    {
                        ord.DeliveryTypeId = 1;
                        ord.DeliveryPointId = (DeliveryPointCb.SelectedItem as DeliveryPoint).Id;
                        ord.AdressToDelivery = null;

                    }
                    else
                    {
                        MessageBox.Show("Выберите тип доставки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    ord.Date = DateTime.Now;
                    ord.Cost = Busket.Sum(b => b.Count * b.Product.Price);
                }
                //Добавление в Order
                App.db.Order.Add(ord);

                //Удаление корзины
                var bucketsToRemove = App.db.Busket.Where(b => b.UserId == AccountUser.AuthUser.Id).ToList();
                App.db.Busket.RemoveRange(bucketsToRemove);

                //Добавление в OrderProduct
                foreach (var b in Busket)
                {
                    var orderProduct = new OrderProduct
                    {
                        ProductId = b.Product.Id,
                        Count = b.Count,
                        OrderId = ord.Id
                    };

                    //Минус товар на складе
                    BuscketItem selectedProd = App.db.Product
                            .Where(p => p.Id == orderProduct.ProductId)
                            .Select(p => new BuscketItem
                            {
                                Product = p,
                                Count = (int)p.Count
                            })
                            .FirstOrDefault();
                    selectedProd.Product.Count -= b.Count;
                    App.db.OrderProduct.Add(orderProduct);

                }

                //Сохранение

                selCard.Balance -= Busket.Sum(b => b.Count * b.Product.Price);
                App.db.SaveChanges();
                MessageBox.Show("Заказ успешно добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ProductsPage());



            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при оформлении заказа: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
