using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Categories = new HashSet<Categories>();
            Comments = new HashSet<Comments>();
            DayStatistics = new HashSet<DayStatistics>();
            HourStatistics = new HashSet<HourStatistics>();
            LockedHours = new HashSet<LockedHours>();
            PaymentOptions = new HashSet<PaymentOptions>();
            Policies = new HashSet<Policies>();
            Reservation = new HashSet<Reservation>();
            RestaurantPhotos = new HashSet<RestaurantPhotos>();
            RestaurantStrikes = new HashSet<RestaurantStrikes>();
            Surveys = new HashSet<Surveys>();
            Tables = new HashSet<Tables>();
            TextFloor = new HashSet<TextFloor>();
            UserPhotos = new HashSet<UserPhotos>();
            UserRestaurant = new HashSet<UserRestaurant>();
            Waiters = new HashSet<Waiters>();
        }

        public int Idrestaurant { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool Confirmation { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Name { get; set; }
        public TimeSpan? OpeningTime { get; set; }
        public TimeSpan? ClosingTime { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Dress { get; set; }
        public double? Price { get; set; }
        public double? Score { get; set; }
        public int? SeverityLevel { get; set; }

        public virtual ICollection<Categories> Categories { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<DayStatistics> DayStatistics { get; set; }
        public virtual ICollection<HourStatistics> HourStatistics { get; set; }
        public virtual ICollection<LockedHours> LockedHours { get; set; }
        public virtual ICollection<PaymentOptions> PaymentOptions { get; set; }
        public virtual ICollection<Policies> Policies { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual ICollection<RestaurantPhotos> RestaurantPhotos { get; set; }
        public virtual ICollection<RestaurantStrikes> RestaurantStrikes { get; set; }
        public virtual ICollection<Surveys> Surveys { get; set; }
        public virtual ICollection<Tables> Tables { get; set; }
        public virtual ICollection<TextFloor> TextFloor { get; set; }
        public virtual ICollection<UserPhotos> UserPhotos { get; set; }
        public virtual ICollection<UserRestaurant> UserRestaurant { get; set; }
        public virtual ICollection<Waiters> Waiters { get; set; }
    }
}
