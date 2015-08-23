using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline.Stages.Subject.Steps;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline.Stages
{
    public class SubjectStage : Stage
    {

        public override IEnumerable<IPrioritizingHandler> AcquireSteps()
        {
            yield return new TransferResultsToSubjectIfOnlyOneGroup();
            yield return new SetupSubjectResultsForMultipleGroups();
            yield return new CalculateSubjectValuesForMultipleGroups();
            yield return new OrderSubjectValues();
        }
    }
}
