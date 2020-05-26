using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrPiattoWAPI.Model
{
    public class ManualReservations
    {
        public int IDReservation { get; set; }
        public int IDTable { get; set; }
        public DateTime Date { get; set; }
        public int AmountOfPeople { get; set; }
        public bool Checked { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool? CheckedFromApp { get; set; }

        public virtual RestaurantTables IdtableNavigation { get; set; }
    }
}
