using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.v1.Engine.Pipeline.Stages.QualityControl.Steps
{
    public class SetEmptyGroupIds : IPrioritizingHandler
    {

        public void Process(PrioritizingRequest request)
        {

            var groups = request.Subject.Groups.Where(q => string.IsNullOrEmpty(q.Id)).ToList();

            if (groups.Count == 0)
                return;

            var x = 1;

            foreach (var group in groups)
            {
                group.Id = x.ToString();
                x++;
            }

        }

    }
}
