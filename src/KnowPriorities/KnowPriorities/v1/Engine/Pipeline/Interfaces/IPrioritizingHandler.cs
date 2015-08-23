using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public interface IPrioritizingHandler
    {

        //void Process(Subject subject, SubjectResults result);
        void Process(PrioritizingRequest request);

    }
}
