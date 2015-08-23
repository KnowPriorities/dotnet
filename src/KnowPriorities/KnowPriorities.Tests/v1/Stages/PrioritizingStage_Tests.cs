using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline.Stages;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages
{
    public class PrioritizingStage_Tests
    {

        [Fact]
        public void Has_All_Tests_In_Order()
        {
            var stage = new PrioritizingStage();

            Assert.Equal(5, stage.Steps.Count);

            Assert.IsType<QualityControlStage>(stage.Steps[0]);
            Assert.IsType<PreparationStage>(stage.Steps[1]);
            Assert.IsType<GroupStage>(stage.Steps[2]);
            Assert.IsType<SubjectStage>(stage.Steps[3]);
            Assert.IsType<PolishStage>(stage.Steps[4]);
        }

    }
}
