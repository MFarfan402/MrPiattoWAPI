using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class User
    {
        public User()
        {
            AuxiliarReservation = new HashSet<AuxiliarReservation>();
            Comments = new HashSet<Comments>();
            Complaints = new HashSet<Complaints>();
            LockedRestaurants = new HashSet<LockedRestaurants>();
            Reservation = new HashSet<Reservation>();
            Surveys = new HashSet<Surveys>();
            UserPhotos = new HashSet<UserPhotos>();
            UserRestaurant = new HashSet<UserRestaurant>();
            UserStrikes = new HashSet<UserStrikes>();
        }

        public int Iduser { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string UserType { get; set; }
        public DateTime? UnlockedDay { get; set; }

        public virtual ICollection<AuxiliarReservation> AuxiliarReservation { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Complaints> Complaints { get; set; }
        public virtual ICollection<LockedRestaurants> LockedRestaurants { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual ICollection<Surveys> Surveys { get; set; }
        public virtual ICollection<UserPhotos> UserPhotos { get; set; }
        public virtual ICollection<UserRestaurant> UserRestaurant { get; set; }
        public virtual ICollection<UserStrikes> UserStrikes { get; set; }
    }
}
