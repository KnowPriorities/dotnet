using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Engine.Pipeline.Stages;
using KnowPriorities.v1.Engine.Pipeline.Stages.Groups.Steps;
using KnowPriorities.v1.Engine.Pipeline.Stages.Subject.Steps;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Subject
{
    public class SubjectStage_Tests
    {

        [Fact]
        public void Has_All_Tests_In_Order()
        {
            var stage = new SubjectStage();

            Assert.Equal(4, stage.Steps.Count);

            Assert.IsType<TransferResultsToSubjectIfOnlyOneGroup>(stage.Steps[0]);
            Assert.IsType<SetupSubjectResultsForMultipleGroups>(stage.Steps[1]);
            Assert.IsType<CalculateSubjectValuesForMultipleGroups>(stage.Steps[2]);
            Assert.IsType<OrderSubjectValues>(stage.Steps[3]);
        }

    }
}
