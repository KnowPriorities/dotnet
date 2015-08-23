using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Groups.Steps
{
    public class SetupGroupResults_Tests : PrioritizingHandlerTests
    {
        private readonly SetupGroupResults Step = new SetupGroupResults();


        [Fact]
        public void Process_Runs_Setup_On_All_Groups()
        {
            _group1.Percentage = 0.5m;
            var group2 = new Group() {Id = "zzTop", Percentage = 0.5m};

            _subject.Groups.Add(group2);

            Step.Process(_request);

            Assert.Equal(2, _request.Result.Groups.Count);
        }

        [Fact]
        public void SetupGroup_Adds_Items()
        {
            Step.SetupGroup(_group1, _request);

            var group = _request.Result.Groups.First();

            Assert.True(group.Items.Count > 0);

            Assert.Equal(1, _request.Result.Groups.Count);
        }

        [Fact]
        public void SetupGroup_Adds_Group_Result()
        {
            Step.SetupGroup(_group1, _request);

            Assert.Equal(1, _request.Result.Groups.Count);
            Assert.Equal(_group1, _request.Result.Groups.First().Group);
        }

        [Fact]
        public void AddItems_Attaches_Stakeholders()
        {
            _stakeholder1.Priorities.Clear();
            _stakeholder2.Priorities.Clear();

            _stakeholder1.Priorities.Add("a");
            _stakeholder1.Priorities.Add("b");
            _stakeholder2.Priorities.Add("a");

            var itemResults = new List<ItemResult>();

            Step.AddItems(_subject.Groups[0].Stakeholders, itemResults, new List<Item>());

            Assert.Contains(_stakeholder1, itemResults[0].Stakeholders);
            Assert.Contains(_stakeholder1, itemResults[1].Stakeholders);

            Assert.Contains(_stakeholder2, itemResults[0].Stakeholders);
            Assert.DoesNotContain(_stakeholder2, itemResults[1].Stakeholders);
        }

        [Fact]
        public void AddItems_Adds_Each_Unique_Item()
        {
            _stakeholder1.Priorities.Clear();
            _stakeholder2.Priorities.Clear();

            _stakeholder1.Priorities.Add("a");
            _stakeholder1.Priorities.Add("b");
            _stakeholder2.Priorities.Add("a");

            var itemResults = new List<ItemResult>();

            Step.AddItems(_subject.Groups[0].Stakeholders, itemResults, new List<Item>());

            Assert.Equal(2, itemResults.Count);
            Assert.Equal("a", itemResults[0].Id);
            Assert.Equal("b", itemResults[1].Id);
        }

        [Fact]
        public void GetStakeholdersWithItemId_Works()
        {
            _stakeholder1.Priorities.Clear();
            _stakeholder2.Priorities.Clear();

            _stakeholder1.Priorities.Add("a");
            _stakeholder2.Priorities.Add("b");

            var results = Step.GetStakeholdersWithItemId(_subject.Groups[0].Stakeholders, "a");

            Assert.Contains(_stakeholder1, results);
            Assert.DoesNotContain(_stakeholder2, results);
        }


        [Fact]
        public void GetUniqueItemIds_Works()
        {
            _stakeholder1.Priorities.Clear();
            _stakeholder2.Priorities.Clear();

            _stakeholder1.Priorities.Add("a");
            _stakeholder1.Priorities.Add("b");
            _stakeholder2.Priorities.Add("a");

            var results = Step.GetUniqueItemIds(_subject.Groups[0].Stakeholders);

            Assert.Equal(2, results.Count);
            Assert.Equal("a", results[0]);
            Assert.Equal("b", results[1]);
        }


    }
}
