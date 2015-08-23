using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public class RemoveGroupsIfOnlyOne : IPrioritizingHandler
    {
        public void Process(PrioritizingRequest request)
        {

            if (request.Result.Groups.Count > 1)
                return;

            request.Result.Groups.Clear();
        }
    }
}
