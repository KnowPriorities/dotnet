using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Engine.Pipeline.Stages;
using KnowPriorities.v1.Engine.Pipeline.Stages.Groups.Steps;
using KnowPriorities.v1.Engine.Pipeline.Stages.Preparation.Steps;
using KnowPriorities.v1.Engine.Pipeline.Stages.QualityControl.Steps;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.QualityControl
{
    public class QualityControlStage_Tests
    {

        [Fact]
        public void Has_All_Tests_In_Order()
        {
            var stage = new QualityControlStage();

            Assert.Equal(5, stage.Steps.Count);

            Assert.IsType<EnsureSubjectExists>(stage.Steps[0]);
            Assert.IsType<AssumeUpdateAtEqualsSubjectAsOfIfGreater>(stage.Steps[1]);
            Assert.IsType<SetEmptyGroupIds>(stage.Steps[2]);
            Assert.IsType<AssumeGroupPercentageRebalanceIfAllGroupsAre100Percent>(stage.Steps[3]);
            Assert.IsType<CheckObjectValidations>(stage.Steps[4]);
        }

    }
}
