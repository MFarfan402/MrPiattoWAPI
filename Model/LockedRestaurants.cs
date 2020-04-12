using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class LockedRestaurants
    {
        public int IdlockedRestaurants { get; set; }
        public int Idrestaurants { get; set; }
        public int Iduser { get; set; }
        public DateTime? UnlockedDate { get; set; }

        public virtual Restaurant IdrestaurantsNavigation { get; set; }
        public virtual User IduserNavigation { get; set; }
    }
}
