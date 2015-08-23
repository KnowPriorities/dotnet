using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Results;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Polish.Steps
{
    public class RemoveGroupsIfOnlyOne_Tests : PrioritizingHandlerTests
    {
        private readonly RemoveGroupsIfOnlyOne Step = new RemoveGroupsIfOnlyOne();


        [Fact]
        public void Clears_Group_Results_If_Only_One()
        {
            _request.Result.Groups.Clear();
            _request.Result.Groups.Add(new GroupResult());

            Step.Process(_request);

            Assert.Equal(0, _request.Result.Groups.Count);
        }

        [Fact]
        public void Does_Not_Clear_Groups_If_More_Than_One()
        {
            _request.Result.Groups.Clear();
            _request.Result.Groups.Add(new GroupResult());
            _request.Result.Groups.Add(new GroupResult());

            Step.Process(_request);

            Assert.Equal(2, _request.Result.Groups.Count);
        }


    }
}
