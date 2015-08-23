using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Engine.Pipeline.Stages;
using KnowPriorities.v1.Engine.Pipeline.Stages.Groups.Steps;
using KnowPriorities.v1.Engine.Pipeline.Stages.Preparation.Steps;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Preparation
{
    public class PreparationStage_Tests
    {

        [Fact]
        public void Has_All_Tests_In_Order()
        {
            var stage = new PreparationStage();

            Assert.Equal(7, stage.Steps.Count);

            Assert.IsType<ApplyStakeholderVolumeAdjustmentsFromTags>(stage.Steps[0]);
            Assert.IsType<AgeStakeholderVolume>(stage.Steps[1]);
            Assert.IsType<RemoveSilentStakeholders>(stage.Steps[2]);
            Assert.IsType<InferMissingPriorities>(stage.Steps[3]);
            Assert.IsType<ClearExcessPriorities>(stage.Steps[4]);
            Assert.IsType<ApplyItemScopeAdjustmentsFromTags>(stage.Steps[5]);
            Assert.IsType<EstablishDefaultScope>(stage.Steps[6]);
        }

    }
}
