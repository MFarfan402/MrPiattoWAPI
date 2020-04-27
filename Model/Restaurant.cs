using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            AuxiliarTables = new HashSet<AuxiliarTables>();
            Comments = new HashSet<Comments>();
            Complaints = new HashSet<Complaints>();
            DayStatistics = new HashSet<DayStatistics>();
            HourStatistics = new HashSet<HourStatistics>();
            LockedHours = new HashSet<LockedHours>();
            LockedRestaurants = new HashSet<LockedRestaurants>();
            Policies = new HashSet<Policies>();
            RestaurantPhotos = new HashSet<RestaurantPhotos>();
            RestaurantStrikes = new HashSet<RestaurantStrikes>();
            RestaurantTables = new HashSet<RestaurantTables>();
            Schedule = new HashSet<Schedule>();
            Surveys = new HashSet<Surveys>();
            UserPhotos = new HashSet<UserPhotos>();
            UserRestaurant = new HashSet<UserRestaurant>();
            Waiters = new HashSet<Waiters>();
        }

        public int Idrestaurant { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool Confirmation { get; set; }
        public DateTime LastLogin { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Dress { get; set; }
        public double Price { get; set; }
        public double Score { get; set; }
        public int SeverityLevel { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public int Idcategories { get; set; }
        public int Idpayment { get; set; }

        public virtual Categories IdcategoriesNavigation { get; set; }
        public virtual PaymentOptions IdpaymentNavigation { get; set; }
        public virtual ICollection<AuxiliarTables> AuxiliarTables { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Complaints> Complaints { get; set; }
        public virtual ICollection<DayStatistics> DayStatistics { get; set; }
        public virtual ICollection<HourStatistics> HourStatistics { get; set; }
        public virtual ICollection<LockedHours> LockedHours { get; set; }
        public virtual ICollection<LockedRestaurants> LockedRestaurants { get; set; }
        public virtual ICollection<Policies> Policies { get; set; }
        public virtual ICollection<RestaurantPhotos> RestaurantPhotos { get; set; }
        public virtual ICollection<RestaurantStrikes> RestaurantStrikes { get; set; }
        public virtual ICollection<RestaurantTables> RestaurantTables { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual ICollection<Surveys> Surveys { get; set; }
        public virtual ICollection<TableStatistics> TableStatistics { get; set; }
        public virtual ICollection<UserPhotos> UserPhotos { get; set; }
        public virtual ICollection<UserRestaurant> UserRestaurant { get; set; }
        public virtual ICollection<Waiters> Waiters { get; set; }
    }
}
