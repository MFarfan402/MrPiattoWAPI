using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class UserRestaurant
    {
        public int IduserRestaurant { get; set; }
        public int Idrestaurant { get; set; }
        public int Iduser { get; set; }
        public bool Visited { get; set; }
        public bool MailSubscription { get; set; }
        public bool Favorite { get; set; }
        public bool ComplaintAdded { get; set; }
        public bool SurveyAdded { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
        public virtual User IduserNavigation { get; set; }
    }
}
