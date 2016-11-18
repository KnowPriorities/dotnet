using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static KnowPriorities.Constants;

namespace KnowPriorities.Calculation.Tests
{
    public class FibonacciTests
    {

        //private  IReadOnlyList<long> scale = Fibonacci.Scale;


        [Fact]
        public void ScaleHas62Numbers() => Assert.Equal(62, Fibonacci.Scale.Count);

        [Fact]
        public void ScaleIsSortedHighestToLowestValue() => Assert.True(Fibonacci.Scale.First() > Fibonacci.Scale.Last());

        [Fact]
        public void FibonacciScaleMatchesGeneratedOneWith12PointsOfPrecision()
        {
            var values = Fibonacci.Generate(12).Reverse().ToList();

            for (var x = 0; x < Fibonacci.Scale.Count; x++)
            {
                Assert.Equal(Fibonacci.Scale[x], values[x]);
            }
        }


        [Fact]
        public void CanGenerate()
        {
            var sequence = Fibonacci.Generate().ToList();

            Assert.Equal(91, sequence.Count);
        }

        [Fact]
        public void CanGenerateWithGoldenRatioPrecision()
        {
            var sequence = Fibonacci.Generate(12).ToList();

            Assert.Equal(62, sequence.Count);

        }

        [Fact]
        public void GenerationFollowsFibonacciProgression()
        {
            var sequence = Fibonacci.Generate().ToList();

            for (var x = 0; x < sequence.Count - 2; x++)
                Assert.Equal(sequence[x + 2], sequence[x] + sequence[x + 1]);
        }


        [Fact]
        public void PrecisionScaleThrowIfRequirementsExceedScale() 
            => Assert.ThrowsAny<Exception>(() => Fibonacci.GetPrecisionScale(TenBillion, 20));



        #region refactor and move to array helpers tests

        [Fact]
        public void StartingIndex_ReturnsSameIndex_WhenSameValue()
            => Fibonacci.Scale.ForEach((index, value) => Assert.Equal(index, Fibonacci.Scale.FloorIndex(value))); 

        [Fact]
        public void StartingIndex_ValuesOver_ReturnCurrent()
            => 0.To(Fibonacci.Scale.Count - 1, (x) => Assert.Equal(x, Fibonacci.Scale.FloorIndex(Fibonacci.Scale[x] + 1))); 

        [Fact]
        public void StartingIndex_ValuesBetween_ReturnIndexOfTheNextLowestValue()
            => 0.To(Fibonacci.Scale.Count - 2, (x) => Assert.Equal(x + 1, Fibonacci.Scale.FloorIndex(Fibonacci.Scale[x] - 1))); 

        [Fact]
        public void StartingIndex_ValuesUnderLowest_ThrowsException()
            => Assert.ThrowsAny<Exception>(() => Fibonacci.Scale.FloorIndex(Fibonacci.Scale.Last() - 1));

        #endregion



    }
}
