using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Results;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Groups.Steps
{
    public class OrderGroupValues_Tests : PrioritizingHandlerTests
    {
        private readonly OrderGroupValues Step = new OrderGroupValues();


        [Fact]
        public void Orders_Items_In_Each_Group()
        {
            var group1 = new GroupResult();
            var group2 = new GroupResult();
            var item1 = new ItemResult() { Value = 1 };
            var item2 = new ItemResult() { Value = 2 };

            group1.Items.Add(item1);
            group1.Items.Add(item2);
            group2.Items.Add(item1);
            group2.Items.Add(item2);

            _request.Result.Groups.Add(group1);
            _request.Result.Groups.Add(group2);

            Step.Process(_request);

            Assert.Equal(item2, group1.Items[0]);
            Assert.Equal(item1, group1.Items[1]);

            Assert.Equal(item2, group2.Items[0]);
            Assert.Equal(item1, group2.Items[1]);
        }

        [Fact]
        public void Orders_Items_By_Adjusted_Value()
        {
            var group = new GroupResult();
            var item1 = new ItemResult() { Value = 1 };
            var item2 = new ItemResult() { Value = 2 };

            group.Items.Add(item1);
            group.Items.Add(item2);

            _request.Result.Groups.Add(group);

            Step.Process(_request);

            Assert.Equal(item2, group.Items[0]);
            Assert.Equal(item1, group.Items[1]);

        }

    }
}
