using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.v1.Engine.Pipeline.Stages
{
    public class PrioritizingStage : Stage
    {

        public override IEnumerable<IPrioritizingHandler> AcquireSteps()
        {
            yield return new QualityControlStage();
            yield return new PreparationStage();
            yield return new GroupStage();
            yield return new SubjectStage();
            yield return new PolishStage();
        }
    }
}
