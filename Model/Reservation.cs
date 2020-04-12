using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Reservation
    {
        public int Idreservation { get; set; }
        public int Iduser { get; set; }
        public int Idtable { get; set; }
        public DateTime Date { get; set; }
        public int AmountOfPeople { get; set; }

        public virtual RestaurantTables IdtableNavigation { get; set; }
        public virtual User IduserNavigation { get; set; }
    }
}
