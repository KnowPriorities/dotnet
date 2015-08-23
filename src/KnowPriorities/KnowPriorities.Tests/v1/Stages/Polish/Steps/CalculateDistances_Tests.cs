using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Results;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Polish.Steps
{
    public class CalculateDistances_Tests : PrioritizingHandlerTests
    {
        private readonly CalculateDistances Step = new CalculateDistances();


        [Fact]
        public void Process_Updates_Subject_And_Group_Level()
        {
            _request.Result.Items.Clear();
            _request.Result.Items.Add(new ItemResult() { Value = 2 });
            _request.Result.Items.Add(new ItemResult() { Value = 1 });
            var group = new GroupResult();

            group.Items.Add(new ItemResult() {Value = 3 });
            group.Items.Add(new ItemResult() {Value = 2});
            _request.Result.Groups.Clear();
            _request.Result.Groups.Add(group);

            Step.Process(_request);

            Assert.Equal(1, _request.Result.Items[0].Distance);
            Assert.Equal(0.5m, _request.Result.Groups[0].Items[0].Distance);
        }

        [Fact]
        public void SetDistances_Updates_All_Distances()
        {
            var items = new List<ItemResult>
            {
                new ItemResult() {Value = 3},
                new ItemResult() {Value = 2},
                new ItemResult() {Value = 1}
            };

            Step.SetDistances(items);

            Assert.Equal(0.5m, items[0].Distance);
            Assert.Equal(1, items[1].Distance);
            Assert.Equal(0, items[2].Distance);
        }


        [Fact]
        public void SetDistance_Provides_Fractional_Percentages()
        {
            var current = new ItemResult { Value = 3 };
            var nextItemDown = new ItemResult() { Value = 2 };

            Step.SetDistance(current, nextItemDown);

            Assert.Equal(0.5m, current.Distance);
        }

        [Fact]
        public void SetDistance_Is_Current_Divided_By_NextItemDown()
        {
            var current = new ItemResult { Value = 8 };
            var nextItemDown = new ItemResult() { Value = 2 };

            Step.SetDistance(current, nextItemDown);

            Assert.Equal(3, current.Distance);
        }

        [Fact]
        public void SetDistance_Sets_0_If_Item_Below_Is_Null()
        {
            var current = new ItemResult { Value = 8 };

            Step.SetDistance(current, null);

            Assert.Equal(0, current.Distance);
        }


        [Fact]
        public void SetDistance_Rounds_To_2_Decimal_Places()
        {
            var current = new ItemResult { Value = 7 };
            var nextItemDown = new ItemResult() { Value = 3 };

            Step.SetDistance(current, nextItemDown);

            Assert.Equal(1.33m, current.Distance);
        }

    }
}
