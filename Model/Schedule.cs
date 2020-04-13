using System;
using System.Collections.Generic;

namespace MrPiattoWAPI.Model
{
    public partial class Schedule
    {
        public int Idschedule { get; set; }
        public int Idrestaurant { get; set; }
        public TimeSpan Otmonday { get; set; }
        public TimeSpan Ctmonday { get; set; }
        public TimeSpan Ottuesday { get; set; }
        public TimeSpan Cttuestday { get; set; }
        public TimeSpan Otwednesday { get; set; }
        public TimeSpan Ctwednesday { get; set; }
        public TimeSpan Otthursday { get; set; }
        public TimeSpan Ctthursday { get; set; }
        public TimeSpan Otfriday { get; set; }
        public TimeSpan Ctfriday { get; set; }
        public TimeSpan Otsaturday { get; set; }
        public TimeSpan Ctsaturday { get; set; }
        public TimeSpan Otsunday { get; set; }
        public TimeSpan Ctsunday { get; set; }

        public virtual Restaurant IdrestaurantNavigation { get; set; }

        // MAURICIO FARFAN
        // Method used to convert to string the schedule. Called in SchedulesController.
        public override string ToString()
        {
            return $"L: {Otmonday.ToString()} - {Ctmonday.ToString()}\n" +
                $"M: {Ottuesday.ToString()} - {Cttuestday.ToString()}\n" +
                $"M: {Otwednesday.ToString()} - {Ctwednesday.ToString()}\n" +
                $"J: {Otthursday.ToString()} - {Ctthursday.ToString()}\n" +
                $"V: {Otfriday.ToString()} - {Ctfriday.ToString()}\n" +
                $"S: {Otsaturday.ToString()} - {Ctsaturday.ToString()}\n" +
                $"D: {Otsunday.ToString()} - {Ctsunday.ToString()}\n";
        }
    }
}
