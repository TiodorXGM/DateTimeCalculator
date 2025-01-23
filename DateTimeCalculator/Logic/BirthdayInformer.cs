using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeCalculator.Logic
{
    public static class BirthdayInformer
    {
        public static bool CalculateEighteenYearsDifference(DateTime birthDate, DateTime currentDate, out TimeDifferenceResult result)
        {
            DateTime eighteenYearsDate = birthDate.AddYears(18);

            if (currentDate >= eighteenYearsDate)
            {
                result = DateComparer.CompareDates(eighteenYearsDate, 0, currentDate, 0);
                return true; 
            }
            else
            {
                result = DateComparer.CompareDates(currentDate, 0, eighteenYearsDate, 0);                 
                return false;
            }
        }

        public static TimeDifferenceResult CalculateLiveDuration(DateTime birthDate, DateTime currentDate)
        {
            return DateComparer.CompareDates(birthDate, 0, currentDate, 0);
         
        }

        public static TimeDifferenceResult CalculateDifferenceForNYears(DateTime birthDate, DateTime currentDate, int years)
        {
            DateTime nYearsDate = birthDate.AddYears(years);
            return DateComparer.CompareDates(currentDate, 0, nYearsDate, 0);           
        }

        public static TimeDifferenceResult CalculateNextBirthday(DateTime birthDate, DateTime currentDate)
        {           
            DateTime currentYearBirthday = new DateTime(currentDate.Year, birthDate.Month, birthDate.Day);

            if (currentDate > currentYearBirthday)
            {
                currentYearBirthday = new DateTime(currentDate.Year + 1, birthDate.Month, birthDate.Day);
            }
            return DateComparer.CompareDates(currentDate, 0, currentYearBirthday, 0);
        }
    }
}
