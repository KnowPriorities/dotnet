using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Engine.Pipeline.Stages;
using KnowPriorities.v1.Engine.Pipeline.Stages.Groups.Steps;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Groups
{
    public class GroupStage_Tests
    {

        [Fact]
        public void Has_All_Tests_In_Order()
        {
            var stage = new GroupStage();

            Assert.Equal(4, stage.Steps.Count);

            Assert.IsType<SetupGroupResults>(stage.Steps[0]);
            Assert.IsType<CalculateGroupValues>(stage.Steps[1]);
            Assert.IsType<AdjustGroupValuesRelativeToItemScope>(stage.Steps[2]);
            Assert.IsType<OrderGroupValues>(stage.Steps[3]);
        }

    }
}
