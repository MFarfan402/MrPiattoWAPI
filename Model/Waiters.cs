using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Waiters
    {
        public int Idwaiters { get; set; }
        public int Idrestaurant { get; set; }
        public string WaiterFirstName { get; set; }
        public string WaiterLastName { get; set; }
        public double? WaiterRating { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
    }
}
