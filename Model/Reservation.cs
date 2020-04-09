using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Reservation
    {
        public Reservation()
        {
            Tables = new HashSet<Tables>();
        }

        public int Idreservation { get; set; }
        public int Idrestaurant { get; set; }
        public int Iduser { get; set; }
        public int Idtable { get; set; }
        public DateTime? Date { get; set; }
        public int? AmountOfPeople { get; set; }

        public virtual User Idrestaurant1 { get; set; }
        public virtual Restaurant IdrestaurantNavigation { get; set; }
        public virtual ICollection<Tables> Tables { get; set; }
    }
}
