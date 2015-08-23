using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;
using KnowPriorities.v1.Engine.Pipeline;

namespace KnowPriorities.Tests.v1.WholeProcess
{
    public class TestScenario
    {
        protected readonly Prioritizer Prioritizer = new Prioritizer();
        protected SubjectResults Result;
        protected Subject Subject;

        protected void Prioritize()
        {
            Result = Prioritizer.Prioritize(Subject);
        }
    }
}
