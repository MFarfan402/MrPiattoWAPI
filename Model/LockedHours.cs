using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class LockedHours
    {
        public int IdlockedHours { get; set; }
        public int Idrestaurant { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
    }
}
