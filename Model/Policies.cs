using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Policies
    {
        public int Idpolicies { get; set; }
        public int Idrestaurant { get; set; }
        public int? MaxTimeRes { get; set; }
        public int? MinTimeRes { get; set; }
        public int? ModTime { get; set; }
        public bool? Strikes { get; set; }
        public int? StrikeType { get; set; }
        public int? MaxTimeArr { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
    }
}
