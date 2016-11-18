using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.Calculation
{
    public class Scale
    {
        public Scale(long[] values, long maxUniques)
        {
            Values = values;
            MaxUniques = maxUniques;

            GoldenRatioPrecision = GoldenRatio.Precision(values[values.Length - 1], values[values.Length - 2]);
        }

        public long[] Values { get; }

        public long MaxUniques { get; }

        public int GoldenRatioPrecision { get; }

        public long Value(int location)
        {
            if (location < 1)
                throw new Exception($"Location ({location}) on the scale must be at least 1");

            if (location > Values.Length)
                return 0;

            return Values[location];
        }

    }
}
