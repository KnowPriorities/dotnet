using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace KnowPriorities.Calculation
{
    /// <summary>
    /// The grid is used to allow an x & y coordinate calculation process to occur while also calculating row/column totals and keeping counts
    /// </summary>
    public class Grid
    {
        public Grid(int width, int height)
        {
            values = new long[width + 1, height + 1];
            counts = new long[width + 1, height + 1];

            Width = width;
            Height = height;
        }

        /// <summary>
        /// Position reference to acquire the summary information given the context it is used for
        /// </summary>
        public const int Summary = 0;

        /// <summary>
        /// Width (x) of the grid provided when creating the class
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Height (y) of the grid provided when creating the class
        /// </summary>
        public int Height { get; }

        private readonly long[,] values;
        private readonly long[,] counts;

        /// <summary>
        /// Acquires the calculated value of any point on the grid. Use Grid.Summary for aggregated results
        /// </summary>
        public long Value(int x, int y) => values[x, y];

        /// <summary>
        /// Acquires the incremented counts of any point on the grid. Use Grid.Summary for aggregated results
        /// </summary>
        public long Count(int x, int y) => counts[x, y];

        /// <summary>
        /// Adds the value to the x & y coordinates, updates summary information, and increments counters
        /// </summary>
        public long Add(int x, int y, long value)
        {
            if (x < 1 || y < 1)
                throw new Exception($"x={x}, y={y}, values must be 1+");

            var result = Adjust(x, y, value);

            Adjust(Summary, y, value);
            Adjust(x, Summary, value);
            Adjust(Summary, Summary, 0);

            return result;
        }

        private long Adjust(int x, int y, long value)
        {
            Interlocked.Increment(ref counts[x, y]);
            var result = Interlocked.Add(ref values[x, y], value);

            if (result < 0)
                throw new Exception("Values provided exceeded Long.MaxValue");

            return result;
        }




    }
}
