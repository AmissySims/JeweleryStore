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
using System.Windows.Shapes;

namespace Admin.Windows
{
    /// <summary>
    /// Логика взаимодействия для TopUpBalanceWin.xaml
    /// </summary>
    public partial class TopUpBalanceWin : Window
    {
        Card contextCard;
        public TopUpBalanceWin(Card cards)
        {
            InitializeComponent();
            contextCard = cards;
            DataContext = contextCard;
        }


        private void AddBalanceTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }



        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(AddBalanceTb.Text))
                {
                    MessageBox.Show("Заполните поле начисления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                else
                {

                    contextCard.Balance += Convert.ToDecimal(AddBalanceTb.Text);
                    App.db.SaveChanges();
                    MessageBox.Show("Начислено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
