using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Surveys
    {
        public int Idsurveys { get; set; }
        public int Idrestaurant { get; set; }
        public int Idclient { get; set; }
        public int Idwaiter { get; set; }
        public double? FoodRating { get; set; }
        public double? ComfortRating { get; set; }
        public double? ServiceRating { get; set; }
        public string ServiceType { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
    }
}
