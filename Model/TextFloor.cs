using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class TextFloor
    {
        public int IdtextFloor { get; set; }
        public int Idrestaurant { get; set; }
        public string FloorName { get; set; }
        public int? CoordenateX { get; set; }
        public int? CoordenateY { get; set; }
        public string Text { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
    }
}
