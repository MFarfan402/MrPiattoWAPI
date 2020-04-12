using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class PaymentOptions
    {
        public PaymentOptions()
        {
            Restaurant = new HashSet<Restaurant>();
        }

        public int IdpaymentOptions { get; set; }
        public string PaymentOption { get; set; }

        public virtual ICollection<Restaurant> Restaurant { get; set; }
    }
}
