using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static KnowPriorities.Constants;

namespace KnowPriorities.Calculation.Tests
{
    public class ConstantsTests
    {

        [Theory,
            InlineData(OneMillion, 1000000),
            InlineData(TenBillion, 10000000000),
        ]
        public void NumericConstantIsStable(long actual, long expected)
            => Assert.Equal(expected, actual);



    }
}
