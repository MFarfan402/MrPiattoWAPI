using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Complaints
    {
        public int Idcomplaints { get; set; }
        public int Idrestaurant { get; set; }
        public int Iduser { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
        public virtual User IduserNavigation { get; set; }
    }
}
