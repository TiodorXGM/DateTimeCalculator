using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeCalculator.Logic
{
    public static class WorkingDaysCounter
    {
        public static int CountWorkingDays(DateTime startDate, DateTime endDate, List<DayOfWeek> includedDays)
        {

            int count = 0;
            
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (includedDays.Contains(date.DayOfWeek))
                {
                    count++;
                }
            }
            return count;
        }
    }

}