using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.Calculation
{
    public static class Fibonacci
    {

        
        /// <summary>
        /// The first 91 values of the Fibonacci sequence starting with 1, 2, 3 in A-Z order
        /// </summary>
        public static readonly IReadOnlyList<long> Sequence = Generate().ToList().AsReadOnly();


        /// <summary>
        /// The first 62 values of the Fibonacci sequence, with at least 12 points of precision to the Golden Ratio, in Z-A order
        /// </summary>
        public static readonly IReadOnlyList<long> Scale = Generate(12).Reverse().ToList().AsReadOnly();


        public static Scale GetPrecisionScale(long maxUniques, int length = 10)
        {
            long estimate = long.MaxValue / maxUniques;

            int index = Scale.FloorIndex(estimate);

            if (index + length >= Scale.Count)
                throw new Exception($"The requirements for {maxUniques} max uniques exceeds the scale's current capabilities for 12 point Golden Ratio precision");

            var values = new List<long>() { 0 };

            for (var x = 0; x < length; x++)
                values.Add(Scale[index + x]);

            return new Scale(values.ToArray(), maxUniques);
        }



        public static IEnumerable<long> Generate(int goldenRatioPrecision = 0)
        {
            long value1 = 1;
            long value2 = 2;
            long combined = 0;

            if (goldenRatioPrecision == 0)
            {
                yield return value1;
                yield return value2;
            }

            while (long.MaxValue - value1 - value2 >= 0)
            {
                combined = value1 + value2;

                if (goldenRatioPrecision == 0 || GoldenRatio.Precision((decimal)combined / value2) >= goldenRatioPrecision)
                    yield return combined;

                value1 = value2;
                value2 = combined;
            }
        }

    }
}
