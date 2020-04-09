using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class PaymentOptions
    {
        public int IdpaymentOptions { get; set; }
        public int Idrestaurant { get; set; }
        public bool? Cash { get; set; }
        public bool? Visa { get; set; }
        public bool? MasterCard { get; set; }
        public bool? Amex { get; set; }
        public bool? Checks { get; set; }
        public bool? Transfers { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
    }
}
