using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Preparation.Steps
{
    public class InferMissingPriorities_Tests : PrioritizingHandlerTests
    {
        private readonly InferMissingPriorities Step = new InferMissingPriorities();


        [Fact]
        public void Process_Infers_For_All_Attached_Stakeholders()
        {
            var weight = new Behavior() { Id = "a", RangeValue = 1, UpdatedAt = DateTime.UtcNow.AddDays(-1) };
            var interaction = new Behavior() { Id = "b", Heat = 1, UpdatedAt = DateTime.UtcNow };

            _stakeholder1.Priorities.Clear();
            _stakeholder2.Priorities.Clear();
            _stakeholder1.Behaviors.Clear();
            _stakeholder2.Behaviors.Clear();

            _stakeholder1.Behaviors.Add(weight);
            _stakeholder1.Behaviors.Add(interaction);
            _stakeholder2.Behaviors.Add(weight);
            _stakeholder2.Behaviors.Add(interaction);

            Step.Process(_request);

            Assert.Equal(2, _stakeholder1.Priorities.Count);
            Assert.Equal(2, _stakeholder2.Priorities.Count);
        }


        [Fact]
        public void Process_Only_Applies_If_Less_Than_10_Priorities()
        {
            _stakeholder1.Priorities.Clear();
            _stakeholder1.Behaviors.Clear();

            _stakeholder1.Behaviors.Add(new Behavior() { Id = "a", RangeValue = 1, UpdatedAt = DateTime.UtcNow.AddDays(-1) });
            _stakeholder1.Behaviors.Add(new Behavior() { Id = "b", Heat = 1, UpdatedAt = DateTime.UtcNow });

            Step.Process(_request);

            Assert.Equal(2, _stakeholder1.Priorities.Count);
        }


        #region InferPriorities for Stakeholder

        [Fact]
        public void InferPriorities_Applies_Weights_Before_Interactions()
        {
            _stakeholder1.Priorities.Clear();
            _stakeholder1.Behaviors.Clear();

            _stakeholder1.Behaviors.Add(new Behavior() { Id = "a", RangeValue = 1, UpdatedAt = DateTime.UtcNow.AddDays(-1) });
            _stakeholder1.Behaviors.Add(new Behavior() { Id = "b", Heat = 1, UpdatedAt = DateTime.UtcNow });

            Step.InferPriorities(_stakeholder1, _subject);

            Assert.Equal("a", _stakeholder1.Priorities[0]);
            Assert.Equal("b", _stakeholder1.Priorities[1]);
        }

        [Fact]
        public void InferPriorities_Applies_Interactions_And_Weights()
        {
            _stakeholder1.Priorities.Clear();
            _stakeholder1.Behaviors.Clear();

            _stakeholder1.Behaviors.Add(new Behavior() { Id = "a", RangeValue = 1, UpdatedAt = DateTime.UtcNow });
            _stakeholder1.Behaviors.Add(new Behavior() { Id = "b", Heat = 1, UpdatedAt = DateTime.UtcNow });

            Step.InferPriorities(_stakeholder1, _subject);

            Assert.Equal(2, _stakeholder1.Priorities.Count);
        }

        #endregion

        #region Append Priorities

        [Fact]
        public void AppendPriorities_Does_Nothing_If_10_Priorities_Are_Already_Identified()
        {
            var priorities = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            var itemIds = new List<string> {"z"};

            Step.AppendPriorities(priorities, itemIds);

            Assert.DoesNotContain("z", priorities);
        }

        [Fact]
        public void AppendPriorities_Does_Nothing_Does_Not_Add_Duplicates()
        {
            var priorities = new List<string> { "1", "2", "3" };
            var itemIds = new List<string> { "2" };

            Step.AppendPriorities(priorities, itemIds);

            Assert.Equal(3, priorities.Count);
        }

        [Fact]
        public void AppendPriorities_Add_ItemId_If_Missing()
        {
            var priorities = new List<string> { "1", "2", "3" };
            var itemIds = new List<string> { "7" };

            Step.AppendPriorities(priorities, itemIds);

            Assert.Contains("7", priorities);
        }

        [Fact]
        public void AppendPriorities_Adds_All_Missing_ItemIds()
        {
            var priorities = new List<string> { "1", "2", "3" };
            var itemIds = new List<string> { "4", "5", "1" };

            Step.AppendPriorities(priorities, itemIds);

            Assert.Equal(5, priorities.Count);
        }

        #endregion

    }
}
