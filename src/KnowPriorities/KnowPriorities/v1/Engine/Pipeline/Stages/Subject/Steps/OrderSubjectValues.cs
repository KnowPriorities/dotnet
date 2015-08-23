using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public class OrderSubjectValues : IPrioritizingHandler 
    {
        public void Process(PrioritizingRequest request)
        {
            request.Result.Items.Sort();
        }
    }
}
