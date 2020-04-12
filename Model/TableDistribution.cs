using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class TableDistribution
    {
        public int IdtableDistribution { get; set; }
        public int Idtables { get; set; }
        public int CoordenateX { get; set; }
        public int CoordenateY { get; set; }
        public DateTime Date { get; set; }

        public virtual RestaurantTables IdtablesNavigation { get; set; }
    }
}
