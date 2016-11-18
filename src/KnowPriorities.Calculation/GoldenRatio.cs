using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.Calculation
{
    /// <summary>
    /// Used to provide information and functionality surrounding the Golden Ratio as adapted to support the .Net platform
    /// </summary>
    public static class GoldenRatio
    {
        /// <summary>
        /// The Golden Ratio to the maximum precision allowed in .Net via decimal (1.618033988749894848204586834m)
        /// </summary>
        public readonly static decimal Value = 1.618033988749894848204586834m;

        /// <summary>
        /// The Golden Ratio in a longer form of 100 places
        /// </summary>
        public const string LongValue = "1.618033988749894848204586834365638117720309179805762862135448622705260462818902449707207204189391137";

        /// <summary>
        /// Precision of GoldenRatio.Value (28) which is the max precision allowed in .Net via decimal
        /// </summary>
        public const int MaxPrecision = 28;

        /// <summary>
        /// Calculates the approximate golden ratio (higher/lower)
        /// </summary>
        public static decimal Calculate(long lower, long higher) => (decimal)higher / lower;

        /// <summary>
        /// Calculates the approximate % precision to the max (precision/MaxPrecision)
        /// </summary>
        public static decimal Percentage(int precision) => precision / MaxPrecision;

        /// <summary>
        /// Calculates the approximate % of precision to the max after calculating the approximate golden ratio (precision/higher/lower)
        /// </summary>
        public static int Precision(long lower, long higher) => Precision(Calculate(lower, higher));

        /// <summary>
        /// Determines the precision of the ratio provided against the golden ratio providing a value of 0-28(MaxPrecision)
        /// </summary>
        public static int Precision(decimal ratio)
        {
            var value = ratio.ToString();

            int min = 0;

            for (min = 0; min < value.Length; min++)
            {
                if (value[min] != LongValue[min])
                    break;
            }

            return min > 1 ? min - 1 : min; // take decimal point into account
        }

    }
}
