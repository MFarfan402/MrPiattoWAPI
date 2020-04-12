using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Comments
    {
        public int Idcomment { get; set; }
        public int Idrestaurant { get; set; }
        public int Iduser { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
        public virtual User IduserNavigation { get; set; }
    }
}
