using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace JeweleryStroreLibrary.Models
{
    public partial class Product
    {
        public byte[] MainImage
        {
            get
            {
                var firstPhoto = this.ProductPhoto.FirstOrDefault();
                if (firstPhoto != null)
                {
                    return firstPhoto.Image;
                }
                return null;
            }
        }

        public Visibility Visibility
        {
            get
            {

                if (Count == 0)
                {
                    return Visibility.Collapsed;
                }
                else { return Visibility.Visible; }
            }
        }
        public string IsAvalible
        {
            get
            {


                if (Count == 0)
                {
                    return $"Нет в наличии";
                }
                else { return $"В наличии"; }
            }
        }

        public SolidColorBrush ColorAv
        {
            get
            {

                if (Count == 0)
                {
                    return Brushes.Red;
                }
                else { return Brushes.Green; }
            }
        }

        public SolidColorBrush ColorBl
        {
            get
            {

                if (Count == 0)
                {
                    return Brushes.LightGray;
                }
                else { return Brushes.Transparent; }
            }

        }


    }

    public class BuscketItem
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}

