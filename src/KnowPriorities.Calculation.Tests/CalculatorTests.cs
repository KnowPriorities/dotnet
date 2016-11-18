using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static KnowPriorities.Constants;

namespace KnowPriorities.Calculation.Tests
{
    public class CalculatorTests
    {
        private static readonly int segmentCount = 5;
        private static readonly int optionCount = 3;
        private static readonly int peopleCount = 25000;
        private static readonly decimal[] percentages = new decimal[] { 0.2m, 0.2m, 0.2m, 0.2m, 0.2m };


        private Calculator calc = new Calculator(segmentCount, optionCount, peopleCount, percentages);

        #region Variable Checking

        [Fact]
        public void CalculatorSegmentsEqualsWhatWasPassedIn() => Assert.Equal(5, calc.Segments);

        [Fact]
        public void CalculatorOptionsEqualsWhatWasPassedIn() => Assert.Equal(3, calc.Options);

        [Fact]
        public void CalculatorPeopleEqualsWhatWasPassedIn() => Assert.Equal(25000, calc.People);

        [Fact]
        public void ScaleMaxUniquesIsSetToPeopleTimesMaxPriorities() => Assert.Equal(calc.People * Calculator.MaxPriorities, calc.Scale.MaxUniques);

        [Fact]
        public void GoldenRatioPrecisionIsAtLeast12()
        {
            calc = new Calculator(1, 20, TenBillion, new[] { 1m });

            Assert.True(calc.Scale.GoldenRatioPrecision >= 12);
        }

        #endregion

        #region Validation

        [Fact]
        public void ValidateThrowsIfLessThan1Segment()
            => Assert.ThrowsAny<Exception>(() => Calculator.Validate(0, 2, 1, new[] { 1m }));

        [Fact]
        public void ValidateThrowsIfLessThan2Options()
            => Assert.ThrowsAny<Exception>(() => Calculator.Validate(1, 1, 1, new[] { 1m }));

        [Fact]
        public void ValidateThrowsIfLessThan1Person()
            => Assert.ThrowsAny<Exception>(() => Calculator.Validate(1, 2, 0, new[] { 1m }));

        [Fact]
        public void ValidateThrowsIfMoreSegmentsThanPercentages()
            => Assert.ThrowsAny<Exception>(() => Calculator.Validate(2, 2, 1, new[] { 1m }));

        [Fact]
        public void ValidateThrowsIfMorePercentagesThanSegments()
            => Assert.ThrowsAny<Exception>(() => Calculator.Validate(1, 2, 1, new[] { 0.5m, 0.5m }));

        [Fact]
        public void ValidateThrowsIfPercentagesDontEqual100Percent()
            => Assert.ThrowsAny<Exception>(() => Calculator.Validate(1, 2, 1, new[] { 0.5m }));


        [Theory,
            InlineData(0, 2, 1, new double[] { 1 }),
            InlineData(1, 1, 1, new double[] { 1 }),
            InlineData(1, 2, 0, new double[] { 1 }),
            InlineData(2, 2, 1, new double[] { 1 }),
            InlineData(1, 2, 1, new double[] { 0.5, 0.5 }),
            InlineData(1, 2, 1, new double[] { 0.5 }),
        ]
        public void CalculatorThrowsIfValidationErrorOccurs(int segments, int options, int people, double[] percentages)
            => Assert.ThrowsAny<Exception>(() => new Calculator(segments, options, people, percentages.Select(n => Convert.ToDecimal(n)).ToArray()));


        #endregion

        #region Adding Votes


        [Fact]
        public void AddingVotesThrowsExceptionIfOverLimits()
        {
            Assert.ThrowsAny<Exception>(() => calc.AddVote(calc.Segments + 1, 1, 1));
            Assert.ThrowsAny<Exception>(() => calc.AddVote(1, calc.Options + 1, 1));
        }

        [Fact]
        public void AddingVotesThrowsExceptionIfUnder1()
        {
            Assert.ThrowsAny<Exception>(() => calc.AddVote(0, 1, 1));
            Assert.ThrowsAny<Exception>(() => calc.AddVote(1, 0, 1));
            Assert.ThrowsAny<Exception>(() => calc.AddVote(1, 1, 0));
        }


        [Fact]
        public void VotesAddsValue()
        {
            Assert.Equal(0, calc.GetValue(1, 1));

            Assert.True(calc.AddVote(1, 1, 1) > 0);
        }

        [Fact]
        public void VotesAddValueEvenly()
        {
            var expected = (long)(calc.Scale.Value(1) * calc.Percentages[1]);

            Assert.Equal(expected, calc.AddVote(1, 1, 1));
            Assert.Equal(expected * 2, calc.AddVote(1, 1, 1));
        }

        [Fact]
        public void Priority1IsGreaterThanPriority2()
        {
            var priority1 = calc.AddVote(1, 1, 1);
            var priority2 = calc.AddVote(1, 2, 2);

            Assert.True(priority1 > priority2, $"{priority1} > {priority2}");
        }

        [Fact]
        public void AddsNoValueIfPriorityIsGreaterThanScaleTracks()
            => Assert.Equal(0, calc.AddVote(1, 1, calc.Scale.Values.Length + 1));


        [Fact]
        public void CanAddAPersonsEntirePriorityListInOneCommand()
        {
            calc.AddVote(1, new[] { 1, 2 });

            var expected = (long)((calc.Scale.Value(1) + calc.Scale.Value(2)) * calc.Percentages[1]);

            var result = calc.GetValue(1, 1) + calc.GetValue(1, 2);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetAdjustmentIsSegmentPercentageTimesScaleValuePriority()
            => Assert.Equal((long)(calc.Percentages[1] * calc.Scale.Value(3)), calc.GetAdjustment(1, 3));

        #endregion



        #region Retrieving Values


        [Fact]
        public void CanGetValueOfSegmentAndOption()
        {
            var expected = calc.AddVote(1, 2, 3);
            var result = calc.GetValue(1, 2);

            Assert.Equal(expected, result);
        }

        [Theory,
            InlineData(10, 1), InlineData(1, 10), InlineData(10, 10),
            InlineData(-1, 1), InlineData(1, -1), InlineData(-1, -1),]
        public void GetValueThrowsExceptionIfOutOfBounds(int segment, int option)
            => Assert.ThrowsAny<Exception>(() => calc.GetValue(segment, option));


        [Fact]
        public void CanGetVoterCountForSegment()
        {
            calc.AddVote(1, 1, 1);
            Assert.Equal(1, calc.GetVoterCountForSegment(1));
        }

        [Theory, InlineData(-1), InlineData(10)]
        public void GetVoterCountForSegmentThrowsIfOutOfBounds(int segment)
            => Assert.ThrowsAny<Exception>(() => calc.GetVoterCountForSegment(segment));



        [Fact]
        public void GetPrioritiesReturnsEmptyListIfNoVotes()
            => Assert.Equal(0, calc.GetPriorities(1).Count);


        [Fact]
        public void GetPrioritiesPlacesResultsIntoOptionValuePairs()
        {
            calc.AddVote(1, 2, 3);
            calc.AddVote(1, 3, 4);

            var results = calc.GetPriorities(1);

            Assert.Equal(1, results.Count(q => q.Key == 2));
            Assert.Equal(1, results.Count(q => q.Key == 3));

            Assert.Equal(calc.GetValue(1, 2), results.First(q => q.Key == 2).Value);
            Assert.Equal(calc.GetValue(1, 3), results.First(q => q.Key == 3).Value);
        }

        [Fact]
        public void GetPrioritiesDoesNotIncludeOptionsWithoutVotes()
        {
            calc.AddVote(1, 1, 1);

            var results = calc.GetPriorities(2);

            Assert.Equal(0, results.Count(q => q.Key == 1));
        }


        [Fact]
        public void GetPrioritiesSortsResultsByHighestToLowestValue()
        {
            calc.AddVote(1, 2, 3);
            calc.AddVote(1, 3, 4);

            var results = calc.GetPriorities(1);

            Assert.True(results.First().Value > results.Last().Value);
        }




        [Fact]
        public void CanGetPriorities()
        {
            calc = new Calculator(1, 3, 2, new decimal[] { 1m });

            // Person A
            calc.AddVote(1, 3, 1);
            calc.AddVote(1, 2, 2);
            calc.AddVote(1, 1, 3);

            // Person B
            calc.AddVote(1, 2, 1);
            calc.AddVote(1, 1, 2);
            calc.AddVote(1, 3, 3);

            var result = calc.GetPriorities(1);

            Assert.Equal(2, result[0].Key);
            Assert.Equal(3, result[1].Key);
            Assert.Equal(1, result[2].Key);

        }

        [Fact]
        public void GetPrioritesReturnsTheSameAsUsingGridSummary()
        {
            calc.AddVote(1, 2, 3);
            calc.AddVote(1, 3, 4);

            var summary = calc.GetPriorities();
            var details = calc.GetPriorities(Grid.Summary);

            for (var x = 0; x < summary.Count; x++)
            {
                Assert.Equal(details[x].Key, summary[x].Key);
                Assert.Equal(details[x].Value, summary[x].Value);
            }
        }

        #endregion


        [Fact]
        public void CanSupport10BillionPeople()
        {
            calc = new Calculator(1, 2, TenBillion, new[] { 1m });

            var result = calc.AddVote(1, 1, 1) * TenBillion;

            Assert.True(result > 0);
        }

        //[Fact]
        //public void CanProcess25kPeopleUnder100ms()
        //{
        //    var watch = new System.Diagnostics.Stopwatch();

        //    List<Tuple<int, int, int>> values = new List<Tuple<int, int, int>>();
        //    var random = new Random();

        //    for (int x = 1; x < 25000; x++)
        //        values.Add(new Tuple<int, int, int>(random.Next(1, 5), random.Next(1, 3), random.Next(1, 10)));


        //    watch.Start();

        //    Parallel.ForEach(values, value => calc.AddVote(value.Item1, value.Item2, value.Item3));

        //    watch.Stop();

        //    Assert.True(watch.ElapsedMilliseconds < 100, $"Actual was {watch.ElapsedMilliseconds}ms");
        //}


    }
}
