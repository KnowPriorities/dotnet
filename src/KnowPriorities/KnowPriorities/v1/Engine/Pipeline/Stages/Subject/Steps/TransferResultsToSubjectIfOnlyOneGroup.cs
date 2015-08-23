using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;

namespace KnowPriorities.v1.Engine.Pipeline.Stages.Subject.Steps
{
    public class TransferResultsToSubjectIfOnlyOneGroup : IPrioritizingHandler
    {
        public void Process(PrioritizingRequest request)
        {
            if (request.Subject.HasOnlyOneGroup)
                request.Result.Items.AddRange(request.Result.Groups.First().Items);
        }

    }
}
