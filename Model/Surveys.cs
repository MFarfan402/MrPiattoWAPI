using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Surveys
    {
        public int Idsurvey { get; set; }
        public int Idrestaurant { get; set; }
        public int Iduser { get; set; }
        public int Idwaiter { get; set; }
        public double FoodRating { get; set; }
        public double ComfortRating { get; set; }
        public double ServiceRating { get; set; }
        public double? GeneralScore { get; set; }
        public int? Idcomment { get; set; }

        public virtual Comments IdcommentNavigation { get; set; }
        public virtual Restaurant IdrestaurantNavigation { get; set; }
        public virtual User IduserNavigation { get; set; }
        public virtual Waiters IdwaiterNavigation { get; set; }
    }
}
