using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Engine.Pipeline.Stages.QualityControl.Steps;
using Xunit;
using KnowPriorities.v1.Models;

namespace KnowPriorities.Tests.v1.Stages.QualityControl.Steps
{
    public class CheckObjectValidations_Tests
    {
        private readonly CheckObjectValidations Step = new CheckObjectValidations();

        private readonly PrioritizingRequest Request =
            new PrioritizingRequest(new KnowPriorities.v1.Models.Subject() { AsOf = DateTime.UtcNow });

        public CheckObjectValidations_Tests()
        {
            Assert.False(Request.HaltProcessing);
            Assert.True(Request.Result.Errors.Count == 0);
        }


        [Fact]
        public void Runs_Validation_Starting_At_Subject()
        {
            Request.Subject.DaysToAge = -1;

            Step.Process(Request);

            Assert.True(Request.HaltProcessing);
            Assert.True(Request.Result.Errors.Count > 0);
        }


    }
}
