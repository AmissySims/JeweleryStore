using JeweleryStroreLibrary.Models.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JeweleryStroreLibrary.Models
{
    public partial class User
    {
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName} {MiddleName}";
            }
        }

        public Visibility AccVisible
        {
            get
            {
                if (Id == AccountUser.AuthUser.Id)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }

    }
}
