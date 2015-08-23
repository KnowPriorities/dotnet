using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.v1
{
    public static class DepreciationHelper
    {
        public const int Days_Per_Depreciation_Cycle = 30;

        public static int Depreciate(this int value, int increment, DateTime asOf, DateTime pointInTime)
        {
            if (increment == 0 || pointInTime > asOf)
                return value;

            var distance = Distance(asOf, pointInTime);

            var result = value - (distance * increment);

            return result;
        }

        public static decimal? Depreciate(this decimal value, decimal increment, DateTime asOf, DateTime pointInTime)
        {
            if (increment == 0m || pointInTime > asOf)
                return value;

            var distance = Distance(asOf, pointInTime);

            try { return value - (distance * increment); }
            catch { return null; }
        }


        public static int Distance(DateTime asOf, DateTime pointInTime)
        {
            return (int)(asOf.Date.Subtract(pointInTime.Date).TotalDays / Days_Per_Depreciation_Cycle);
        }




    }
}
