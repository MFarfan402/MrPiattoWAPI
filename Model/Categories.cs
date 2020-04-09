using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Categories
    {
        public int Idcategories { get; set; }
        public int Idrestaurant { get; set; }
        public string Category { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
    }
}
