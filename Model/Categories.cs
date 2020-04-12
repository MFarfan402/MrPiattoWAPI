using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Categories
    {
        public Categories()
        {
            Restaurant = new HashSet<Restaurant>();
        }

        public int Idcategory { get; set; }
        public string Category { get; set; }

        public virtual ICollection<Restaurant> Restaurant { get; set; }
    }
}
