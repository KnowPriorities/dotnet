using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public class RemoveSilentStakeholders : IPrioritizingHandler
    {
        public void Process(PrioritizingRequest request)
        {
            foreach (var group in request.Subject.Groups)
            {
                var stakeholders = group.Stakeholders;

                stakeholders.RemoveAll(q => q.Volume < 1);

                stakeholders.RemoveAll(
                    q => q.Priorities.Count == 0 && q.Behaviors.Count == 0);
            }
        }
    }
}
