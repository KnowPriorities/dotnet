using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace KnowPriorities.Calculation.Tests
{
    public class GridTests
    {

        private readonly Grid grid = new Grid(3, 4);


        [Fact]
        public void HeightIsSameAsProvided() => Assert.Equal(4, grid.Height);

        [Fact]
        public void WidthIsSameAsProvided() => Assert.Equal(3, grid.Width);


        [Fact]
        public void AddingValueOnlyAddsWhatWasProvided()
        {
            grid.Add(1, 1, 3);

            Assert.Equal(3, grid.Value(1, 1));
        }

        [Fact]
        public void AddReturnsTheNewlyAddedValue()
        {
            grid.Add(1, 1, 2);

            Assert.Equal(5, grid.Add(1, 1, 3));
        }

        [Fact]
        public void AddingValuesAlsoAddsToColumnSummary()
        {
            grid.Add(1, 1, 3);

            Assert.Equal(3, grid.Value(Grid.Summary, 1));
        }

        [Fact]
        public void AddingValuesAlsoAddsToRowSummary()
        {
            grid.Add(1, 1, 3);

            Assert.Equal(3, grid.Value(1, Grid.Summary));
        }


        [Fact]
        public void OverallSummaryValueDoesNotChange()
        {
            grid.Add(1, 1, 3);

            Assert.Equal(0, grid.Value(Grid.Summary, Grid.Summary));
        }


        [Fact]
        public void AddingIncrementsCounterForThatPosition()
        {
            Assert.Equal(0, grid.Count(1, 1));

            grid.Add(1, 1, 3);

            Assert.Equal(1, grid.Count(1, 1));
        }

        [Fact]
        public void AddingIncrementsCounterColumnSummary()
        {
            grid.Add(1, 1, 3);

            Assert.Equal(1, grid.Count(Grid.Summary, 1));
        }

        [Fact]
        public void AddingIncrementsCounterRowSummary()
        {
            grid.Add(1, 1, 3);

            Assert.Equal(1, grid.Count(1, Grid.Summary));
        }

        [Fact]
        public void AddingIncrementsOverallCounterSummary()
        {
            Assert.Equal(0, grid.Count(Grid.Summary, Grid.Summary));

            grid.Add(1, 1, 1);
            grid.Add(1, 1, 2);
            grid.Add(1, 1, 3);

            Assert.Equal(3, grid.Count(Grid.Summary, Grid.Summary));
        }


        [Theory, InlineData(0, 0), InlineData(1, 0), InlineData(0, 1), InlineData(-1, -1)]
        public void AddingValueMustBeToPositionsMustBeAtLeast1(int x, int y)
            => Assert.ThrowsAny<Exception>(() => grid.Add(x, y, 1));

        [Fact]
        public void ThrowsExceptionWhenAddingValuesMoreThanLong()
        {
            grid.Add(1, 1, long.MaxValue);

            Assert.ThrowsAny<Exception>(() => grid.Add(1, 1, 1));
        }

        [Fact]
        public void ThrowsExceptionWhenAddingValuesMoreThanLongInDifferentColumns()
        {
            grid.Add(1, 1, long.MaxValue);

            Assert.ThrowsAny<Exception>(() => grid.Add(2, 1, 1));
        }

        [Fact]
        public void ThrowsExceptionWhenAddingValuesMoreThanLongInDifferentRows()
        {
            grid.Add(1, 1, long.MaxValue);

            Assert.ThrowsAny<Exception>(() => grid.Add(1, 2, 1));
        }

    }
}
