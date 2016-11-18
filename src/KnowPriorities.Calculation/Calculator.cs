using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace KnowPriorities.Calculation
{
    public class Calculator
    {
        public const int MaxPriorities = 5;

        public Calculator(int segmentCount, int optionCount, long peopleCount, decimal[] percentages)
        {
            Validate(segmentCount, optionCount, peopleCount, percentages);

            Percentages = percentages.Resize(1).ToList().AsReadOnly();

            People = peopleCount;

            grid = new Grid(segmentCount, optionCount);
            Scale = Fibonacci.GetPrecisionScale(peopleCount * MaxPriorities);
        }

        private readonly Grid grid;
        public readonly Scale Scale;

        public int Segments => grid.Width;
        public int Options => grid.Height;
        public long People { get; }

        public IReadOnlyList<decimal> Percentages { get; }


        #region Adding Votes

        public void AddVote(int segment, int[] priorities)
        {
            for (var x = 0; x < priorities.Length; x++)
                AddVote(segment, priorities[x], x + 1);
        }

        public long AddVote(int segment, int option, int priority) => grid.Add(segment, option, GetAdjustment(segment, priority));

        public long GetAdjustment(int segment, int priority) => (long)(Percentages[segment] * Scale.Value(priority));

        #endregion

        #region Retrieving Values

        public long GetValue(int segment, int option) => grid.Value(segment, option);

        public long GetVoterCountForSegment(int segment) => grid.Count(segment, Grid.Summary);
        

        /// <summary>
        /// For a segment, returns a list of the options and their computed values in sorted order (most valuable to least valuable)
        /// </summary>
        public List<KeyValuePair<int, long>> GetPriorities(int segment)
        {
            var results = new List<KeyValuePair<int, long>>();

            // If no one voted, then the segment cannot have a priority list
            if (GetVoterCountForSegment(segment) < 1)
                return results;


            for (var x = 1; x <= Options; x++)
            {
                if(grid.Count(segment, x)>0)
                    results.Add(new KeyValuePair<int, long>(x, grid.Value(segment, x)));
            }

            results.Sort((x, y) => y.Value.CompareTo(x.Value));

            return results;
        }

        public List<KeyValuePair<int, long>> GetPriorities() => GetPriorities(Grid.Summary);

        #endregion

        #region Validation

        public static void Validate(int segments, int options, long people, decimal[] percentages)
        {
            if (segments < 1)
                throw new Exception("Requires at least 1 segment");

            if (options < 2)
                throw new Exception("Requires at least 2 options");

            if (people < 1)
                throw new Exception("Requires at least 1 person");

            if (segments != percentages.Length)
                throw new Exception($"{segments} segments provided but {percentages.Length} percentages provided.  Values must match.");

            if (percentages.Sum() != 1)
                throw new Exception($"The total segment percentages equals {percentages.Sum() * 100}% instead of 100%.");
        }


        #endregion


    }
}
