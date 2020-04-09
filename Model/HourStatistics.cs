using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class HourStatistics
    {
        public int IdhourStatistics { get; set; }
        public int Idrestaurant { get; set; }
        public double? Average0000 { get; set; }
        public double? Average0100 { get; set; }
        public double? Average0200 { get; set; }
        public double? Average0300 { get; set; }
        public double? Average0400 { get; set; }
        public double? Average0500 { get; set; }
        public double? Average0600 { get; set; }
        public double? Average0700 { get; set; }
        public double? Average0800 { get; set; }
        public double? Average0900 { get; set; }
        public double? Average1000 { get; set; }
        public double? Average1100 { get; set; }
        public double? Average1200 { get; set; }
        public double? Average1300 { get; set; }
        public double? Average1400 { get; set; }
        public double? Average1500 { get; set; }
        public double? Average1600 { get; set; }
        public double? Average1700 { get; set; }
        public double? Average1800 { get; set; }
        public double? Average1900 { get; set; }
        public double? Average2000 { get; set; }
        public double? Average2100 { get; set; }
        public double? Average2200 { get; set; }
        public double? Average2300 { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }
    }
}
