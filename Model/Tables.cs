using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Tables
    {
        public int Idtables { get; set; }
        public int Idrestaurant { get; set; }
        public string FloorName { get; set; }
        public int? CoordenateX { get; set; }
        public int? CoordenateY { get; set; }
        public string Status { get; set; }
        public double? AverageUse { get; set; }

        public virtual Restaurant Idrestaurant1 { get; set; }
        public virtual Reservation IdrestaurantNavigation { get; set; }
    }
}
