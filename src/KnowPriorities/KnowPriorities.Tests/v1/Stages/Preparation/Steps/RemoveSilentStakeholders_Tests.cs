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
    public class RemoveSilentStakeholders_Tests : PrioritizingHandlerTests
    {
        private readonly RemoveSilentStakeholders Step = new RemoveSilentStakeholders();

        [Fact]
        public void Volume_Less_Than_1_Is_Removed()
        {
            _stakeholder1.Volume = 0;

            Step.Process(_request);

            Assert.False(_subject.Groups[0].Stakeholders.Contains(_stakeholder1));
        }


        [Fact]
        public void Volume_Greater_Than_0_Is_Not_Removed()
        {
            _stakeholder1.Volume = 1;

            Step.Process(_request);

            Assert.True(_subject.Groups[0].Stakeholders.Contains(_stakeholder1));
        }

        [Fact]
        public void Removed_If_No_Priorities_Interactions_Or_Weights()
        {
            _stakeholder1.Volume = 1;

            _stakeholder1.Priorities.Clear();
            _stakeholder1.Behaviors.Clear();

            Step.Process(_request);

            Assert.False(_subject.Groups[0].Stakeholders.Contains(_stakeholder1));
        }

        [Fact]
        public void Not_Removed_If_Has_Priorities()
        {
            _stakeholder1.Volume = 1;

            _stakeholder1.Priorities.Add("z");
            _stakeholder1.Behaviors.Clear();

            Step.Process(_request);

            Assert.True(_subject.Groups[0].Stakeholders.Contains(_stakeholder1));
        }
    }
}
