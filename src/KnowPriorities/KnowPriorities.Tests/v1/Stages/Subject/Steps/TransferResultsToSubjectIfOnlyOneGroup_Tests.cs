using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Engine.Pipeline.Stages.Subject.Steps;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Subject.Steps
{
    public class TransferResultsToSubjectIfOnlyOneGroup_Tests
    {
        private readonly TransferResultsToSubjectIfOnlyOneGroup Step = new TransferResultsToSubjectIfOnlyOneGroup();
        private readonly PrioritizingRequest Request;
        private readonly ItemResult Item1 = new ItemResult() { Id = "a", Value = 3, Distance = 0.3m };
        private readonly ItemResult Item2 = new ItemResult() { Id = "b", Value = 1, Distance = 0 };

        public TransferResultsToSubjectIfOnlyOneGroup_Tests()
        {
            var subject = new KnowPriorities.v1.Models.Subject();
            Request = new PrioritizingRequest(subject);
            
            var group = new Group();
            subject.Groups.Add(group);

            var groupResult = new GroupResult();
            Request.Result.Groups.Add(groupResult);

            groupResult.Items.Add(Item1);
            groupResult.Items.Add(Item2);
        }

        [Fact]
        public void Does_Nothing_If_Multi_Group()
        {
            Assert.Equal(0, Request.Result.Items.Count);

            Request.Subject.Groups.Add(new Group());

            Step.Process(Request);

            Assert.Equal(0, Request.Result.Items.Count);
        }

        [Fact]
        public void Transfers_Items_To_Subject_Level()
        {
            Step.Process(Request);

            Assert.Equal(2, Request.Result.Items.Count);
        }

        [Fact]
        public void Maintains_Order()
        {
            Step.Process(Request);

            Assert.Equal(Item1.Id, Request.Result.Items[0].Id);
            Assert.Equal(Item2.Id, Request.Result.Items[1].Id);
        }

        [Fact]
        public void Maintains_Precalculated_Value_And_Distance()
        {
            Step.Process(Request);

            Assert.Equal(Item1.Value, Request.Result.Items[0].Value);
            Assert.Equal(Item2.Value, Request.Result.Items[1].Value);

            Assert.Equal(Item1.Distance, Request.Result.Items[0].Distance);
            Assert.Equal(Item2.Distance, Request.Result.Items[1].Distance);
        }

    }
}
