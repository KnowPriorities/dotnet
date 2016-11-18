using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KnowPriorities.Calculation.Tests
{
    public class ArrayHelpersTests
    {

        [Theory,
            InlineData(1, 1, new long[] { 1, 5, 10 }),
            InlineData(3, 1, new long[] { 1, 5, 10 }),
            InlineData(5, 5, new long[] { 1, 5, 10 }),
            InlineData(7, 5, new long[] { 1, 5, 10 }),
            InlineData(10, 10, new long[] { 1, 5, 10 }),
            InlineData(12, 10, new long[] { 1, 5, 10 }),
        ]
        public void Floor_Works(long find, long expected, long[] values)
            => Assert.Equal(expected, values.Floor(find));

        [Fact]
        public void Floor_Throws_Exception_If_Below_Minimum()
         => Assert.ThrowsAny<Exception>(() => (new long[] { 2, 3 }).Floor(1));

    }
}
