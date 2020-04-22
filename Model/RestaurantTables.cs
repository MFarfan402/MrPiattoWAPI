using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class RestaurantTables
    {
        public RestaurantTables()
        {
            LockedTables = new HashSet<LockedTables>();
            Reservation = new HashSet<Reservation>();
            TableDistribution = new HashSet<TableDistribution>();
        }

        public int Idtables { get; set; }
        public int Idrestaurant { get; set; }
        public string FloorName { get; set; }
        public double CoordenateX { get; set; }
        public double CoordenateY { get; set; }
        public double AvarageUse { get; set; }
        public string Seats { get; set; }
        public string Type { get; set; }
        public int? FloorIndex { get; set; }
        public string TableName { get; set; }
        public bool? IsJoin { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
        public virtual ICollection<LockedTables> LockedTables { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual ICollection<TableDistribution> TableDistribution { get; set; }
    }
}
