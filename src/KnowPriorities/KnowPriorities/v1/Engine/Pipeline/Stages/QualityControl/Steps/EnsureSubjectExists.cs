using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.v1.Engine.Pipeline.Stages.QualityControl.Steps
{
    public class EnsureSubjectExists : IPrioritizingHandler
    {
        public void Process(PrioritizingRequest request)
        {
            if (request.Subject != null)
                return;

            request.HaltProcessing = true;
            request.Result.Errors.Add("We were unable to process your request as no subject was provided.");
        }
    }
}
