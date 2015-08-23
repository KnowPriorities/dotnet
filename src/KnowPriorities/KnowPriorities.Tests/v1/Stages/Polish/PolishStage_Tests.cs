using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Engine.Pipeline.Stages;
using KnowPriorities.v1.Engine.Pipeline.Stages.Groups.Steps;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Polish
{
    public class PolishStage_Tests
    {

        [Fact]
        public void Has_All_Tests_In_Order()
        {
            var stage = new PolishStage();

            Assert.Equal(2, stage.Steps.Count);

            Assert.IsType<RemoveGroupsIfOnlyOne>(stage.Steps[0]);
            Assert.IsType<CalculateDistances>(stage.Steps[1]);
        }

    }
}
