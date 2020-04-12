using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class LockedTables
    {
        public int IdlockedTables { get; set; }
        public int Idtables { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual RestaurantTables IdtablesNavigation { get; set; }
    }
}
