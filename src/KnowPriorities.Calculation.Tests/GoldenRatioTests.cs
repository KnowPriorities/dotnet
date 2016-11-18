using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KnowPriorities.Calculation.Tests
{
    public class GoldenRatioTests
    {

        [Fact]
        public void GoldenRatioValueIsMaximumDecimal() => Assert.Equal(1.618033988749894848204586834m, GoldenRatio.Value);

        [Fact]
        public void LongValueIsCorrect()
            => Assert.Equal("1.618033988749894848204586834365638117720309179805762862135448622705260462818902449707207204189391137", GoldenRatio.LongValue);


        [Fact]
        public void MaxGoldenRatioPrecisionMirrorsValueLength()
            => Assert.Equal(GoldenRatio.MaxPrecision, GoldenRatio.Value.ToString().Length - 1);

        [Fact]
        public void PrecisionPercentageDividesAgainstMax()
            => Assert.Equal(5 / GoldenRatio.MaxPrecision, GoldenRatio.Percentage(5));


        [Fact]
        public void CanCalculateGoldenRatioPrecision()
        {
            var golden = GoldenRatio.Value.ToString();

            for (var x = 1; x < golden.Length; x++)
            {
                var test = Convert.ToDecimal(golden.Substring(0, x));

                var expected = x > 1 ? x - 1 : x;
                var result = GoldenRatio.Precision(test);

                Assert.Equal(expected, result);
            }
        }

        [Fact]
        public void PrecisionCalculationIsAccurateEvenWithExtraNumbersAfterPrecision()
            => Assert.Equal(3, GoldenRatio.Precision(1.617m));


        [Theory,
            InlineData(2),
            InlineData(0),
            InlineData(-1),
            ]
        public void PrecisionCalculationReturnsZeroWhenNoMatch(decimal value)
            => Assert.Equal(0, GoldenRatio.Precision(value));

        [Fact]
        public void PrecisionCalculationStopsWhenTheFirstValueDoesntMatch()
            => Assert.Equal(0, GoldenRatio.Precision(0.618m));

    }
}
