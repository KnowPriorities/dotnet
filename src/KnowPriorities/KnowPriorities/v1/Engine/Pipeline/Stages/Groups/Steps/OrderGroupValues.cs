using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public class OrderGroupValues : IPrioritizingHandler 
    {

        public void Process(PrioritizingRequest request)
        {
            request.Result.Groups.ForEach(g=> g.Items.Sort());
        }
    }
}
