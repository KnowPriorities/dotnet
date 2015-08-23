using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Subject.Steps
{
    public class CalculateSubjectValues_Tests : PrioritizingHandlerTests
    {
        private readonly CalculateSubjectValuesForMultipleGroups Step = new CalculateSubjectValuesForMultipleGroups();


        [Fact]
        public void Process_Request_Should_Update_All_Items()
        {
            _request.Result.Items.Clear();

            // required to trigger "multi group"
            _request.Subject.Groups.Clear();
            _request.Subject.Groups.Add(new Group());
            _request.Subject.Groups.Add(new Group());

            for (var x = 0; x < 4; x++)
            {
                var subjectItem = new ItemResult() {Id = x.ToString(), Value = 0};

                var group = new GroupResult() {Percentage = 0.25m};

                group.Items.Add(new ItemResult() {Id = x.ToString()});

                subjectItem.Groups.Add(group);

                _request.Result.Items.Add(subjectItem);
            }

            Step.Process(_request);
            
            foreach(var item in _request.Result.Items)
            {
                Assert.True(item.Value > 0);
            }
        }


        [Fact]
        public void Process_ItemResult_Should_Update_For_All_Groups()
        {
            var group1 = new GroupResult() { Percentage = 0.5m };
            var group2 = new GroupResult() { Percentage = 0.5m };

            group1.Items.Add(new ItemResult() { Id = "a" });
            group2.Items.Add(new ItemResult() { Id = "a" });

            var item = new ItemResult() { Id = "a", Value = 0 };

            item.Groups.Add(group1);
            item.Groups.Add(group2);

            Step.Process(item);

            var expected = Step.GetIncrease(0, 0.5m) * 2;

            Assert.Equal(expected, item.Value);
        }

        [Fact]
        public void UpdateItemValue_Does_Not_Increase_Items_Value_If_Not_Found()
        {
            var group = new GroupResult() { Percentage = 1 };
            group.Items.Add(new ItemResult() { Id = "b" });

            var item = new ItemResult() { Id = "a", Value = 0 };
            item.Groups.Add(group);

            Step.UpdateItemValue(item, group);

            Assert.Equal(0, item.Value);
        }

        [Fact]
        public void UpdateItemValue_Increases_Items_Value_If_Found()
        {
            var group = new GroupResult() {Percentage = 1};
            group.Items.Add(new ItemResult() { Id = "a" });

            var item = new ItemResult() { Id = "a", Value = 0 };
            item.Groups.Add(group);

            Step.UpdateItemValue(item, group);

            Assert.True(item.Value > 0);
        }

        [Fact]
        public void GetPriorityNumber_Returns_Correct_0_Based_Index()
        {
            var items = new List<ItemResult>
            {
                new ItemResult {Id = "a"},
                new ItemResult {Id = "b"}, // index = 1
                new ItemResult {Id = "c"}
            };

            var result = Step.GetPriorityNumber(items, "b");

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetPriorityNumber_Returns_Int_Max_If_Not_Found()
        {
            var items = new List<ItemResult>();

            var result = Step.GetPriorityNumber(items, "notfound");

            Assert.Equal(int.MaxValue, result);
        }

        [Fact]
        public void GetIncrease_Applies_Percentage()
        {
            var baseValue = CalculateSubjectValuesForMultipleGroups.Increases[0];

            var expected1 = (long) (baseValue * 0.9m);
            var result1 = Step.GetIncrease(0, 0.9m);

            var expected2 = (long) (baseValue * 0.1m);
            var result2 = Step.GetIncrease(0, 0.1m);

            Assert.Equal(expected1, result1);
            Assert.Equal(expected2, result2);
        }

        [Fact]
        public void GetIncrease_Returns_0_Value_If_Outside_Of_Increases_Array()
        {
            var result = Step.GetIncrease(CalculateSubjectValuesForMultipleGroups.Increases.Length, 1);

            Assert.Equal(0, result);
        }

    }
}
