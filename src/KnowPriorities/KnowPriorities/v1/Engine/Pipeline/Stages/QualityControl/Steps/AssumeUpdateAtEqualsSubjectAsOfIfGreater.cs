using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.v1.Engine.Pipeline.Stages.QualityControl.Steps
{
    public class AssumeUpdateAtEqualsSubjectAsOfIfGreater : IPrioritizingHandler
    {
        public void Process(PrioritizingRequest request)
        {
            var asOf = request.Subject.AsOf;

            foreach (var group in request.Subject.Groups)
            {
                foreach (var stakeholder in group.Stakeholders)
                {
                    foreach (var priority in stakeholder.Behaviors)
                    {
                        if (priority.UpdatedAt > asOf)
                            priority.UpdatedAt = asOf;
                    }

                    if (stakeholder.UpdatedAt > asOf)
                        stakeholder.UpdatedAt = asOf;
                }
            }

        }
    }
}
