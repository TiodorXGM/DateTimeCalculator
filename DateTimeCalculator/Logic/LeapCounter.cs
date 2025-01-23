using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeCalculator.Logic
{
    public static class LeapCounter
    {
        public static int CountLeap(DateTime startDate, DateTime endDate)
        {
            int count = 0;

            for (int year = startDate.Year; year <= endDate.Year; year++)
            {
                if (DateTime.IsLeapYear(year)) count++;
            }

            return count;

        }
    }
}
