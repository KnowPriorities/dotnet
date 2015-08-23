using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline.Stages
{
    public abstract class Stage : IPrioritizingHandler
    {

        public readonly ReadOnlyCollection<IPrioritizingHandler> Steps;

        protected Stage()
        {
            Steps = AcquireSteps().ToList().AsReadOnly();
        }

        public virtual IEnumerable<IPrioritizingHandler> AcquireSteps()
        {
            return new List<IPrioritizingHandler>();
        }


        public void Process(PrioritizingRequest request)
        {
            foreach (var step in Steps)
            {
                step.Process(request);

                if (request.HaltProcessing)
                    break;
            }
        }
    }
}
