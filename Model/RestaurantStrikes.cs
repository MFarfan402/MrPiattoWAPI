using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class RestaurantStrikes
    {
        public int IdrestaurantStrikes { get; set; }
        public int Idrestaurant { get; set; }
        public DateTime Date { get; set; }
        public int Severity { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
    }
}
