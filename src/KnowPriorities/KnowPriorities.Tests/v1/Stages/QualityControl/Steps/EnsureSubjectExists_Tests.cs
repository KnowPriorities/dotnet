using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Engine.Pipeline.Stages.QualityControl.Steps;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.QualityControl.Steps
{
    public class EnsureSubjectExists_Tests
    {
        private readonly EnsureSubjectExists Step = new EnsureSubjectExists();


        [Fact]
        public void Halts_If_Subject_Is_Null()
        {
            var request = new PrioritizingRequest(null);

            Step.Process(request);

            Assert.True(request.HaltProcessing);
        }

        [Fact]
        public void Does_Not_Halt_If_Subject_Is_Not_Null()
        {
            var request = new PrioritizingRequest(new KnowPriorities.v1.Models.Subject());

            Step.Process(request);

            Assert.False(request.HaltProcessing);
        }





    }
}
