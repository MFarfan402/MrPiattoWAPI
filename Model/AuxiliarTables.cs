using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class AuxiliarTables
    {
        public AuxiliarTables()
        {
            AuxiliarReservation = new HashSet<AuxiliarReservation>();
        }

        public int IdauxiliarTable { get; set; }
        public int Idrestaurant { get; set; }
        public string FloorName { get; set; }
        public double? CoordenateX { get; set; }
        public double? CoordenateY { get; set; }
        public double? AvarageUse { get; set; }
        public string StringIdtables { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
        public virtual ICollection<AuxiliarReservation> AuxiliarReservation { get; set; }
    }
}
