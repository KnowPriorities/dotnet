using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Subject.Steps
{
    public class SetupSubjectResults_Test : PrioritizingHandlerTests
    {
        public SetupSubjectResults_Test() 
        {
            Groups.Add(Group1);
            Groups.Add(Group2);
            Group1.Items.Add(Item1);
            Group1.Items.Add(Item2);
            Group2.Items.Add(Item1);
        }


        private readonly SetupSubjectResultsForMultipleGroups Step = new SetupSubjectResultsForMultipleGroups();

        private readonly List<GroupResult> Groups = new List<GroupResult>();
        private readonly GroupResult Group1 = new GroupResult(){Id = "1"};
        private readonly GroupResult Group2 = new GroupResult(){Id = "2"};
        private readonly ItemResult Item1 = new ItemResult() {Id = "a"};
        private readonly ItemResult Item2 = new ItemResult() {Id = "b"};

        [Fact]
        public void Process_Transfers_Results_To_Subject_Level_ItemResults()
        {
            // required to trigger multi-group
            _request.Subject.Groups.Add(new Group());
            _request.Subject.Groups.Add(new Group());


            _request.Result.Groups.AddRange(Groups);

            Assert.Equal(0, _request.Result.Items.Count);

            Step.Process(_request);

            Assert.Equal(2, _request.Result.Items.Count);
        }

        [Fact]
        public void AddItems_Attaches_The_Group_To_The_Item()
        {
            var results = new List<ItemResult>();

            Step.AddItems(Groups, results);

            Assert.Contains(Group1, results[0].Groups);
            Assert.Contains(Group1, results[1].Groups);
            Assert.Contains(Group2, results[0].Groups);
            Assert.DoesNotContain(Group2, results[1].Groups);
        }

        [Fact]
        public void AddItems_Works()
        {
            var results = new List<ItemResult>();

            Step.AddItems(Groups, results);

            Assert.Equal(2, results.Count);
        }

        [Fact]
        public void GetGroupsWithItemId_Works()
        {
            var results = Step.GetGroupsWithItemId(Groups, "a").ToList();

            Assert.Equal(2, results.Count);
            Assert.Contains(Group1, results);
            Assert.Contains(Group2, results);


            results = Step.GetGroupsWithItemId(Groups, "b").ToList();

            Assert.Equal(1, results.Count);
            Assert.Contains( Group1, results);
            Assert.DoesNotContain(Group2, results);
        }

        [Fact]
        public void GetUniqueItemIds_Works()
        {
            var results = Step.GetUniqueItemIds(Groups);

            Assert.Equal(2, results.Count);
            Assert.Equal("a", results[0]);
            Assert.Equal("b", results[1]);
        }

    }
}
