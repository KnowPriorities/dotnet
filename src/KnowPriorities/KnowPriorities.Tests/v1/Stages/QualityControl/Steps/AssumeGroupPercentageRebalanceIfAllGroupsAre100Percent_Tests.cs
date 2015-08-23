using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline.Stages.Preparation.Steps;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Preparation.Steps
{
    public class AssumeGroupPercentageRebalanceIfAllGroupsAre100Percent_Tests : PrioritizingHandlerTests
    {
        private readonly AssumeGroupPercentageRebalanceIfAllGroupsAre100Percent Step =
            new AssumeGroupPercentageRebalanceIfAllGroupsAre100Percent();



        [Fact]
        public void Does_nothing_if_100_percent()
        {
            _request.Subject.Groups.Clear();
            _request.Subject.Groups.Add(new Group() { Percentage = 1 });
            _request.Result.Errors.Clear();

            Step.Process(_request);

            Assert.False(_request.HaltProcessing);
            Assert.Equal(0, _request.Result.Errors.Count);
        }


    }
}
