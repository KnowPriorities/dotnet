using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.Calculation
{
    public static class ArrayHelpers
    {


        /// <summary>
        /// Finds the numerical value that equals or is the next lowest value
        /// </summary>
        public static long Floor(this IEnumerable<long> array, long find)
        {
            var values = array.ToList();

            values.Sort();

            if (values.Contains(find))
                return find;

            if (values.Max() <= find)
                return values.Max();


            var ceiling = values.FirstOrDefault(value => value > find);

            var floor = values[values.IndexOf(ceiling) - 1];

            return floor;
        }


        public static int FloorIndex(this IEnumerable<long> array, long find) => array.ToList().IndexOf(array.Floor(find));


        public static IEnumerable<T> Resize<T>(this IEnumerable<T> values, int count)
        {
            for (var x = 0; x < count; x++)
                yield return default(T);

            foreach (var value in values)
                yield return value;
        }

    }
}
