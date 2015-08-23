using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.v1.Engine.Pipeline.Stages.Preparation.Steps
{
    public class EstablishDefaultScope : IPrioritizingHandler
    {

        public void Process(PrioritizingRequest request)
        {
            var subject = request.Subject;

            subject.DefaultScope = subject.Items.Count == 0 ? 1 : subject.Items.Max(q => q.Scope);
        }
    }
}
