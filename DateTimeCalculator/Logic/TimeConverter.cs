using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DateTimeCalculator.Form1;

namespace DateTimeCalculator.Logic
{
    public enum TimeType
    {
        Years,
        Months,
        Days,
        Hours,
        Minutes,
        Seconds
    }
    public  static class TimeConverter
    {
        public static int GetTimeFactor(TimeType type)
        {
            int timeFactor = 1;
            if (type == TimeType.Years) timeFactor = 60 * 60 * 24 * 365;
            if (type == TimeType.Months) timeFactor = 60 * 60 * 24 * 30;
            if (type == TimeType.Days) timeFactor = 60 * 60 * 24;
            if (type == TimeType.Hours) timeFactor = 60 * 60;
            if (type == TimeType.Minutes) timeFactor = 60;
            if (type == TimeType.Seconds) timeFactor = 1;
            return timeFactor;
        }

        public static decimal ConvertTime(decimal value, TimeType fromType, TimeType toType)
        {
            if (fromType == TimeType.Years && toType == TimeType.Months)
            {
                return value * 12; 
            }

            if (fromType == TimeType.Months && toType == TimeType.Years)
            {
                return value / 12; 
            }

            int fromFactor = GetTimeFactor(fromType);
            int toFactor = GetTimeFactor(toType);

            return value * fromFactor / toFactor;
        }

    }
}
