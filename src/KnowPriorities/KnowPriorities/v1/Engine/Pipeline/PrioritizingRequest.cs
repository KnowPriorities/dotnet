using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public class PrioritizingRequest
    {
        public PrioritizingRequest(Subject subject)
        {
            this.Subject = subject;
            this.Result = new SubjectResults();
        }

        public readonly Subject Subject;
        public readonly SubjectResults Result;
        public bool HaltProcessing;
    }
}
