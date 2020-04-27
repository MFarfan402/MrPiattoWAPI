using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class TableStatistics
    {
        public int IDTableStatistics { get; set; }
        public int IDRestaurant { get; set; }
        public int IDTable { get; set; }
        public double AvarageUse { get; set; }
        public DateTime DateStatistics { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
        public virtual RestaurantTables IdrestaurantTablesNavigation { get; set; }
    }
}
