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

namespace Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для CardsPage.xaml
    /// </summary>
    public partial class CardsPage : Page
    {
        public CardsPage()
        {
            InitializeComponent();
        }
        public void Refresh()
        {
            var selCards = App.db.Card.Where(x => x.UserId == AccountUser.AuthUser.Id).ToList();
            CardsList.ItemsSource = selCards;
        }
        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            AddCardWin addCard = new AddCardWin(new Card());
            addCard.ShowDialog();
            Refresh();
        }

        private void DeleteBt_Click(object sender, RoutedEventArgs e)
        {
            var selCard = (sender as Button).DataContext as Card;
            App.db.Card.Remove(selCard);
            App.db.SaveChanges();
            Refresh();
        }

        private void TopUpBalanceBt_Click(object sender, RoutedEventArgs e)
        {
            var selCard = (sender as Button).DataContext as Card;
            TopUpBalanceWin topUpBalance = new TopUpBalanceWin(selCard);
            topUpBalance.ShowDialog();
            Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
