using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline.Stages;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public class Prioritizer
    {

        public readonly PrioritizingStage Stage = new PrioritizingStage();

        public SubjectResults Prioritize(Subject subject)
        {
            var request = new PrioritizingRequest(subject);

            Stage.Process(request);
            
            return request.Result;
        }

    }
}
