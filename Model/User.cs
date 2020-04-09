using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class User
    {
        public User()
        {
            Reservation = new HashSet<Reservation>();
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

        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual ICollection<UserPhotos> UserPhotos { get; set; }
        public virtual ICollection<UserRestaurant> UserRestaurant { get; set; }
        public virtual ICollection<UserStrikes> UserStrikes { get; set; }
    }
}
