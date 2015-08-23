using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline.Stages.Groups.Steps;
using KnowPriorities.v1.Engine.Pipeline.Stages.Preparation.Steps;


namespace KnowPriorities.v1.Engine.Pipeline.Stages
{
    public class PreparationStage : Stage
    {

        public override IEnumerable<IPrioritizingHandler> AcquireSteps()
        {
            yield return new ApplyStakeholderVolumeAdjustmentsFromTags();
            yield return new AgeStakeholderVolume();
            yield return new RemoveSilentStakeholders();
            yield return new InferMissingPriorities();
            yield return new ClearExcessPriorities();
            yield return new ApplyItemScopeAdjustmentsFromTags();
            yield return new EstablishDefaultScope();
        }

    }
}
