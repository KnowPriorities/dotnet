using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KnowPriorities.Constants;

namespace KnowPriorities.Calculation
{
    public static class Satisfaction
    {

        public static Scale Scale = Fibonacci.GetPrecisionScale(TenBillion);


        /// <summary>
        /// Compares the segment's perspective on priorities against the actual priorities
        /// </summary>
        public static int Calculate(Calculator calc, int segment)
        {
            var perspectives = LimitToScale(GetSegmentPriorities(calc, segment));

            return Calculate(calc, perspectives);
        }


        public static IEnumerable<int> Calculate(Calculator calc, IEnumerable<int[]> perspectives)
        {
            foreach (var perspective in perspectives)
                yield return Calculate(calc, perspective);
        }


        /// <summary>
        /// Compares the provided perspective on priorities against the actual priorities
        /// </summary>
        public static int Calculate(Calculator calc, int[] perspectives)
        {
            var actuals = LimitToScale(GetActualPriorities(calc));

            return Calculate(actuals, perspectives);
        }


        public static int Calculate(int[] actuals, int[] perspectives)
        {
            long possible = GetPossibleValue(perspectives.Length);
            long actual = GetActualValue(actuals, perspectives);

            return actual.ToIntPercentage(possible);
        }

        public static int ToIntPercentage(this long over, long under)
        {
            if (under == 0)
                return 0;//This occurs if no one in a segment voted

            var percentage = (decimal)over / under * 100;

            int result = Convert.ToInt32(percentage);

            if (result > 100)
                throw new Exception($"Satisfaction percentage was {result}%");

            return result;
        }

        public static int[] GetActualPriorities(Calculator calc) => calc.GetPriorities().Select(q => q.Key).ToArray();

        public static int[] GetSegmentPriorities(Calculator calc, int segment) => calc.GetPriorities(segment).Select(q => q.Key).ToArray();

        private static long GetActualValue(int[] actuals, int[] perspectives)
        {
            long result = 0;

            for (var x = 0; x < actuals.Length; x++)
            {
                var index = Array.IndexOf(perspectives, actuals[x]);

                if (index == -1)
                    continue;

                result += Value(x + 1, index + 1);
            }

            return result;
        }

        public static long GetPossibleValue(int length)
            => Scale.Values.Take(length + 1).Sum();

        public static long Value(int actual, int perspective)
            => ValueIndex(actual, perspective).Value();


        /// <summary>
        /// The actual value should not be more than the expected/possible
        /// </summary>
        public static int ValueIndex(int actual, int perspective)
                => actual < perspective ? perspective : actual;

        public static long Value(this int index)
            => (index >= Scale.Values.Length || index < 0) ? 0 : Scale.Value(index);

        public static int[] LimitToScale(int[] values)
        {
            if (values.Length > Scale.Values.Length - 1)
                return values.Take(Scale.Values.Length - 1).ToArray();

            return values;
        }

    }
}
