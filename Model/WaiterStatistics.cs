using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class WaiterStatistics
    {
        public int IdwaiterStatistics { get; set; }
        public int Idwaiter { get; set; }
        public double Rating { get; set; }
        public DateTime DateStatistics { get; set; }
        public virtual Waiters IdwaitersNavigation { get; set; }
    }
}
