using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JeweleryStroreLibrary.Models
{
    public partial class Order
    {
        public Visibility AdressVisible
        {
            get
            {
                if (DeliveryTypeId == 1)
                {
                    return Visibility.Collapsed;

                }
                else
                { return Visibility.Visible; }

            }
        }
        public Visibility AdressVisible1
        {
            get
            {
                if (DeliveryTypeId == 2)
                {
                    return Visibility.Collapsed;

                }
                else
                { return Visibility.Visible; }

            }
        }
        public Visibility DateVisible
        {
            get
            {
                if (DateToCome == null)
                {
                    return Visibility.Collapsed;

                }
                else
                { return Visibility.Visible; }

            }
        }
        public Visibility EditVisible
        {
            get
            {
                if (OrdStatusId == 6 || OrdStatusId == 7)
                {
                    return Visibility.Collapsed;

                }
                else
                { return Visibility.Visible; }

            }
        }

        public Visibility PointVisible
        {
            get
            {
                if (DeliveryTypeId == 1 && OrdStatusId == 5)
                {
                    return Visibility.Visible;

                }
                else
                { return Visibility.Collapsed; }

            }
        }
        public int? Count
        {
            get
            {
                return this.OrderProduct.Sum(x => x.Count);
            }
        }
        public decimal? TotalPrice
        {
            get
            {
                return this.OrderProduct.Sum(x => x.Count * x.Product.Price);
            }
        }
    }
}
