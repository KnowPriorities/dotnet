using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.Calculation.Tests
{
    public static class Helpers
    {

        public static void ForEach<T>(this IEnumerable<T> array, Action<int, T> work)
        {
            var values = array.ToList();

            for (var x = 0; x < values.Count; x++)
                work(x, values[x]);
        }


        public static void To(this long start, long stop, Action<long> work)
        {
            if (start < stop)
                IterateUp(start, stop, work);
            else
                IterateDown(start, stop, work);
        }

        public static void IterateUp(long start, long stop, Action<long> work)
        {
            for (var x = start; x <= stop; x++)
                work(x);
        }

        public static void IterateDown(long start, long stop, Action<long> work)
        {
            for (var x = stop; x >= stop; x--)
                work(x);
        }



        public static void To(this int start, int stop, Action<int> work)
        {
            if (start < stop)
                IterateUp(start, stop, work);
            else
                IterateDown(start, stop, work);
        }

        public static void IterateUp(int start, int stop, Action<int> work)
        {
            for (var x = start; x <= stop; x++)
                work(x);
        }

        public static void IterateDown(int start, int stop, Action<int> work)
        {
            for (var x = stop; x >= stop; x--)
                work(x);
        }


    }
}
