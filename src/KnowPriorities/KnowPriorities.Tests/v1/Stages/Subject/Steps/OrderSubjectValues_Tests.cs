using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Results;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Subject.Steps
{
    public class OrderSubjectValues_Tests : PrioritizingHandlerTests
    {
        private readonly OrderSubjectValues Step = new OrderSubjectValues();

        [Fact]
        public void OrderSubjectValues_Orders_Items_By_Value()
        {
            var items = _request.Result.Items;

            items.Clear();

            items.Add(new ItemResult() { Id = "a", Value = 1 });
            items.Add(new ItemResult() { Id = "b", Value = 2 });
            
            Step.Process(_request);

            Assert.Equal("b", items[0].Id);
            Assert.Equal("a", items[1].Id);
        }

    }
}
