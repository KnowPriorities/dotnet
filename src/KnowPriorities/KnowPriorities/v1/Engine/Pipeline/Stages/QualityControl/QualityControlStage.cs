using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline.Stages.Groups.Steps;
using KnowPriorities.v1.Engine.Pipeline.Stages.Preparation.Steps;
using KnowPriorities.v1.Engine.Pipeline.Stages.QualityControl.Steps;

namespace KnowPriorities.v1.Engine.Pipeline.Stages
{
    public class QualityControlStage : Stage
    {
        public override IEnumerable<IPrioritizingHandler> AcquireSteps()
        {
            yield return new EnsureSubjectExists();
            yield return new AssumeUpdateAtEqualsSubjectAsOfIfGreater();
            yield return new SetEmptyGroupIds();
            yield return new AssumeGroupPercentageRebalanceIfAllGroupsAre100Percent();
            yield return new CheckObjectValidations();
        }
    }
}
