using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Waiters
    {
        public Waiters()
        {
            Surveys = new HashSet<Surveys>();
        }

        public int Idwaiter { get; set; }
        public int Idrestaurant { get; set; }
        public string WaiterFirstName { get; set; }
        public string WaiterLasName { get; set; }
        public double WaiterRating { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
        public virtual ICollection<Surveys> Surveys { get; set; }
    }
}
