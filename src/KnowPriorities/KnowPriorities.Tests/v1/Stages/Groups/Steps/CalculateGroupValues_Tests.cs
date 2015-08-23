using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Groups.Steps
{
    public class CalculateGroupValues_Tests : PrioritizingHandlerTests
    {
        private readonly CalculateGroupValues Step = new CalculateGroupValues();


        [Fact]
        public void Process_Request_Processes_All_Groups()
        {
            var groupResult1 = new GroupResult();
            var groupResult2 = new GroupResult();

            var item1 = new ItemResult() { Id = "a" };
            var item2 = new ItemResult() { Id = "b" };

            _stakeholder1.Priorities.Clear();
            _stakeholder1.Priorities.Add("a");
            _stakeholder2.Priorities.Clear();
            _stakeholder2.Priorities.Add("b");

            item1.Stakeholders.Add(_stakeholder1);
            item2.Stakeholders.Add(_stakeholder1);

            groupResult1.Items.Add(item1);
            groupResult2.Items.Add(item2);

            _request.Result.Groups.Clear();
            _request.Result.Groups.Add(groupResult1);
            _request.Result.Groups.Add(groupResult2);

            Step.Process(_request);

            Assert.True(item1.Value > 0);
            Assert.True(item2.Value > 0);
        }


        [Fact]
        public void Process_GroupResult_Processes_All_Items()
        {
            var groupResult = new GroupResult();

            var item1 = new ItemResult() { Id = "a" };
            var item2 = new ItemResult() { Id = "b" };

            _stakeholder1.Priorities.Clear();
            _stakeholder1.Priorities.Add("a");
            _stakeholder1.Priorities.Add("b");

            item1.Stakeholders.Add(_stakeholder1);
            item2.Stakeholders.Add(_stakeholder1);

            groupResult.Items.Add(item1);
            groupResult.Items.Add(item2);

            Step.Process(groupResult);

            Assert.True(item1.Value > 0);
            Assert.True(item2.Value > 0);

        }

        [Fact]
        public void Process_ItemResult_Adds_For_All_Stakeholders()
        {
            var itemResult = new ItemResult() {Id = "a"};

            _stakeholder1.Priorities.Clear();
            _stakeholder1.Priorities.Add(itemResult.Id);

            _stakeholder2.Priorities.Clear();
            _stakeholder2.Priorities.Add(itemResult.Id);

            itemResult.Stakeholders.Add(_stakeholder1);
            itemResult.Stakeholders.Add(_stakeholder2);

            var value1 = Step.GetIncrease(_stakeholder1, itemResult.Id);
            var value2 = Step.GetIncrease(_stakeholder2, itemResult.Id);

            Assert.Equal(0, itemResult.Value);

            Step.Process(itemResult);

            var expected = value1 + value2;

            Assert.Equal(expected, itemResult.Value);
        }


        [Fact]
        public void Process_ItemResult_Increments_Value()
        {
            var itemResult = new ItemResult();

            var itemIds = _stakeholder1.Priorities;

            itemIds.Clear();
            itemIds.Add("a");

            itemResult.Stakeholders.Add(_stakeholder1);

            Assert.Equal(0, itemResult.Value);

            Step.Process(itemResult);

            Assert.True(itemResult.Value > 0);
        }


        [Fact]
        public void Differences_In_Volume_Adhere_To_The_Golden_Ratio()
        {
            var itemIds = _stakeholder1.Priorities;

            itemIds.Clear();
            itemIds.Add("a");

            var results = new long[10];

            for (var volume = 1; volume < 11; volume++)
            {
                _stakeholder1.Volume = volume;

                results[volume - 1] = Step.GetIncrease(_stakeholder1, "a");
            }

            Assert.Equal(CalculateGroupValues.GoldenRatio, Math.Round(((decimal)results[1] / (decimal)results[0]), 5));
            Assert.Equal(CalculateGroupValues.GoldenRatio, Math.Round(((decimal)results[2] / (decimal)results[1]), 5));
            Assert.Equal(CalculateGroupValues.GoldenRatio, Math.Round(((decimal)results[3] / (decimal)results[2]), 5));
            Assert.Equal(CalculateGroupValues.GoldenRatio, Math.Round(((decimal)results[4] / (decimal)results[3]), 5));
            Assert.Equal(CalculateGroupValues.GoldenRatio, Math.Round(((decimal)results[5] / (decimal)results[4]), 5));
            Assert.Equal(CalculateGroupValues.GoldenRatio, Math.Round(((decimal)results[6] / (decimal)results[5]), 5));
            Assert.Equal(CalculateGroupValues.GoldenRatio, Math.Round(((decimal)results[7] / (decimal)results[6]), 5));
            Assert.Equal(CalculateGroupValues.GoldenRatio, Math.Round(((decimal)results[8] / (decimal)results[7]), 5));
            Assert.Equal(CalculateGroupValues.GoldenRatio, Math.Round(((decimal)results[9] / (decimal)results[8]), 5));
        }


        [Fact]
        public void GetIncrease_Provides_Higher_Values_For_Higher_Volume()
        {
            var itemIds = _stakeholder1.Priorities;

            itemIds.Clear();

            itemIds.Add("a");

            _stakeholder1.Volume = 1;
            var result1 = Step.GetIncrease(_stakeholder1, "a");
            
            _stakeholder1.Volume = 2;
            var result2 = Step.GetIncrease(_stakeholder1, "a");

            Assert.True(result1 < result2);
        }

        [Fact]
        public void GetIncrease_Provides_Values_Greater_In_Value_If_Higher_On_Priority_List()
        {
            var itemIds = _stakeholder1.Priorities;

            itemIds.Clear();

            itemIds.Add("a");
            itemIds.Add("b");

            var aResult = Step.GetIncrease(_stakeholder1, "a");
            var bResult = Step.GetIncrease(_stakeholder1, "b");

            Assert.True(aResult > bResult);
        }

        [Fact]
        public void GetIncrease_Gets_The_Right_Value_For_Each_Priority_And_Volume()
        {
            const string id = "a";

            var stakeholder = new Stakeholder();

            var priorities = new List<List<string>>
            {
                new List<string> {id, "", "", "", "", "", "", "", "", ""},
                new List<string> {"", id, "", "", "", "", "", "", "", ""},
                new List<string> {"", "", id, "", "", "", "", "", "", ""},
                new List<string> {"", "", "", id, "", "", "", "", "", ""},
                new List<string> {"", "", "", "", id, "", "", "", "", ""},
                new List<string> {"", "", "", "", "", id, "", "", "", ""},
                new List<string> {"", "", "", "", "", "", id, "", "", ""},
                new List<string> {"", "", "", "", "", "", "", id, "", ""},
                new List<string> {"", "", "", "", "", "", "", "", id, ""},
                new List<string> {"", "", "", "", "", "", "", "", "", id}
            };

            for (var p = 0; p < 10; p++)
            {
                // Counting down volume to iterate down max to min values
                for (var v = 9; v > -1; v--)
                {
                    var index = p + (9 - v);

                    stakeholder.Priorities.Clear();
                    stakeholder.Priorities.AddRange(priorities[p]);

                    stakeholder.Volume = v + 1;
                    var result = Step.GetIncrease(stakeholder, id);

                    Assert.Equal(CalculateGroupValues.Increases[index], result);
                }
            }

        }

    }
}
