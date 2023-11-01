using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JeweleryStroreLibrary.Models
{
    partial class Shipment
    {
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
        public Visibility Visible
        {
            get
            {
                if (ShipmentStatusId == 1)
                {
                    return Visibility.Visible;

                }
                else
                { return Visibility.Collapsed; }

            }
        }
    }
}
