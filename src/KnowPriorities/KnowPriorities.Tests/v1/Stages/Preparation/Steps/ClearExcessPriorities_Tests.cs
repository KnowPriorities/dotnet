using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Preparation.Steps
{
    public class ClearExcessPriorities_Tests : PrioritizingHandlerTests
    {
        private readonly ClearExcessPriorities Step = new ClearExcessPriorities();

        [Fact]
        public void Processes_All_Stakeholders()
        {
            _stakeholder1.Priorities.Clear();
            _stakeholder2.Priorities.Clear();

            var itemIds = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
            
            _stakeholder1.Priorities.AddRange(itemIds);
            _stakeholder2.Priorities.AddRange(itemIds);

            Step.Process(_request);

            Assert.Equal(10, _stakeholder1.Priorities.Count);
            Assert.Equal(10, _stakeholder2.Priorities.Count);
        }

        [Fact]
        public void Removes_Priorities_Greater_Than_10_From_Stakeholder()
        {
            _stakeholder1.Priorities.Clear();
            _stakeholder1.Behaviors.Clear();

            var itemIds = new List<string> {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"};
            _stakeholder1.Priorities.AddRange(itemIds);

            Step.Cleanse(_stakeholder1);

            Assert.Equal(10, _stakeholder1.Priorities.Count);
        }


        [Fact]
        public void Removes_Priorities_Preserves_Order()
        {
            _stakeholder1.Priorities.Clear();
            _stakeholder1.Behaviors.Clear();

            var itemIds = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
            _stakeholder1.Priorities.AddRange(itemIds);

            Step.Cleanse(_stakeholder1);

            for(var x=0; x<10;x++)
                Assert.Equal(itemIds[x], _stakeholder1.Priorities[x]);
        }
        


    }
}
