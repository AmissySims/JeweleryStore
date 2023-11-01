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
    /// Логика взаимодействия для EditShipmentWin.xaml
    /// </summary>
    public partial class EditShipmentWin : Window
    {
        Shipment contextShip;
        public EditShipmentWin(Shipment shipment)
        {
            InitializeComponent();
            StatusCb.ItemsSource = App.db.ShipmentStatus.ToList();
            contextShip = shipment;
            DataContext = contextShip;
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = true;
        }

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (contextShip.ShipmentStatus == null)
                {
                    MessageBox.Show("Выберите статус", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    App.db.SaveChanges();
                    if (contextShip.ShipmentStatusId == 2)
                    {
                        contextShip.DateToCome = DateTime.Now;
                        var prod = App.db.Product.Where(x => x.Id == contextShip.ProductId).FirstOrDefault();
                        prod.Count += contextShip.Count;


                        App.db.SaveChanges();
                    }

                    MessageBox.Show("Сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
