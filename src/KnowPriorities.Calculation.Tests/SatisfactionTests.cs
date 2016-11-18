using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static KnowPriorities.Constants;

namespace KnowPriorities.Calculation.Tests
{
    public class SatisfactionTests
    {

        [Fact]
        public void ScaleIsBasedOn10Billion()
            => Assert.Equal(TenBillion, Satisfaction.Scale.MaxUniques);


        [Fact]
        public void ValueGetsValueAtScalesIndex()
            => Assert.Equal(Satisfaction.Scale.Value(1), Satisfaction.Value(1));

        [Theory, InlineData(-1), InlineData(11)]
        public void ValueReturnsZeroIfIndexIsOutOfBounds(int index)
            => Assert.Equal(0, Satisfaction.Value(index));

        [Fact]
        public void ValueIndexReturnsPerspectiveIfGreaterThanActual()
            => Assert.Equal(3, Satisfaction.ValueIndex(2, 3));

        [Fact]
        public void ValueIndexReturnsActualIfGreaterThanPerspective()
            => Assert.Equal(4, Satisfaction.ValueIndex(4, 2));


        [Theory, 
            InlineData(2, 1, 2), // Expected 2nd place, got 1st, value = 2nd place (capped expectation)
            InlineData(2, 2, 1), // Expected 1st place, got 2nd, value = 2nd place (lowered expectation)
            InlineData(4, 4, 4)] // Expected 4th & got 4th, value = 4th (perspective = reality)
        public void ValueShouldNotBeMoreThanExpected(int expected, int actual, int perspective)
        {
            var result = Satisfaction.Value(actual, perspective);

            Assert.Equal(Satisfaction.Value(expected), result);
        }


        [Fact]
        public void GetPossibleValueTotalsValues()
        {
            for (var x = 1; x < Satisfaction.Scale.Values.Length; x++)
            {
                long expected = Satisfaction.Scale.Values.Take(x + 1).Sum();

                Assert.Equal(expected, Satisfaction.GetPossibleValue(x));
            }
        }




        [Fact]
        public void CanCalculateSatisfaction()
        {
            var reality = new int[] { 1, 2, 3 };
            var goal = new int[] { 3, 1, 2 };

            var result = Satisfaction.Calculate(reality, goal);

            var possible = Satisfaction.Scale.Value(1) + Satisfaction.Scale.Value(2) + Satisfaction.Scale.Value(3);

            var actual = Satisfaction.Scale.Value(3) // 3: Goal was 1st, but reality was 3rd
                + Satisfaction.Scale.Value(2) // 1: Goal was 2nd, reality was 1st, so expectation was 2nd
                + Satisfaction.Scale.Value(3); // 2: Goal was 3rd, reality was 1st, so expectation was 3rd 

            var expected = (int)((decimal)actual / possible * 100);

            Assert.Equal(expected, result);//69%
        }


        [Fact]
        public void ToIntPercentageThrowsIfGreaterThan100()
        {
            Assert.ThrowsAny<Exception>(() => Satisfaction.ToIntPercentage(2, 1));
        }

        [Fact]
        public void ToIntPercentageWorks()
        {
            Assert.Equal(25, Satisfaction.ToIntPercentage(1, 4));
        }

    }
}
