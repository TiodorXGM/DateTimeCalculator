using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeCalculator.Logic
{

    public class TimeDifferenceResult
    {
        public int Years { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int Milliseconds { get; set; }
    }

    public static class DateComparer
    {
        public static TimeDifferenceResult CompareDates(DateTime date1, int ms1, DateTime date2, int ms2)
        {
            date1 = date1.AddMilliseconds(-date1.Millisecond);
            date2 = date2.AddMilliseconds(-date2.Millisecond);

            int totalDays = (date2 - date1).Days;

            int years        = 0, 
                months       = 0, 
                days         = 0, 
                hours        = 0, 
                minutes      = 0, 
                seconds      = 0, 
                milliseconds = 0;
            
            while (date1.AddYears(1) <= date2)
            {
                date1 = date1.AddYears(1);
                years++;
            }

            while (date1.AddMonths(1) <= date2)
            {
                date1 = date1.AddMonths(1);
                months++;
            }

            TimeSpan remaining = date2 - date1;

            days = remaining.Days;
            hours = remaining.Hours;
            minutes = remaining.Minutes;
            seconds = remaining.Seconds;
            milliseconds = remaining.Milliseconds + (ms2 - ms1);



            return new TimeDifferenceResult
            {
                Years = years,
                Months = months,
                Days = days,
                Hours = hours,
                Minutes = minutes,
                Seconds = seconds,
                Milliseconds = milliseconds
            };
        }

        public static int GetTotalDays(DateTime date1, int ms1, DateTime date2, int ms2)
        {          
            date1 = date1.AddMilliseconds(-date1.Millisecond);
            date2 = date2.AddMilliseconds(-date2.Millisecond);

            int days = (date2 - date1).Days;

            return days;
        }
    }
}
