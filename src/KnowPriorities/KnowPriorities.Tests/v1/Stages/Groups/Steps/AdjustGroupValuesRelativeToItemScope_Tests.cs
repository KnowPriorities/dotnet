using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline.Stages.Groups.Steps;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Groups.Steps
{
    public class AdjustGroupValuesRelativeToItemScope_Tests : PrioritizingHandlerTests
    {
        private readonly AdjustGroupValuesRelativeToItemScope Step = new AdjustGroupValuesRelativeToItemScope();

        const int defaultScope = 2;

        [Fact]
        public void Adjusts_All_Groups()
        {
            var group1 = new GroupResult();
            var group2 = new GroupResult();

            var item1 = new ItemResult() { Item = new Item() { Scope = 4 }, Value = 100 };
            var item2 = new ItemResult() { Item = new Item() { Scope = 5 }, Value = 100 };

            group1.Items.Add(item1);
            group2.Items.Add(item2);

            _request.Result.Groups.Clear();
            _request.Result.Groups.Add(group1);
            _request.Result.Groups.Add(group2);

            Step.Process(_request);

            Assert.Equal(25, item1.Value);
            Assert.Equal(20, item2.Value);
        }


        [Fact]
        public void Adjusts_All_Items()
        {
            var group = new GroupResult();
            var item1 = new ItemResult() { Item = new Item() { Scope = 4 }, Value = 100 };
            var item2 = new ItemResult() { Item = new Item() { Scope = 5 }, Value = 100 };
        
            group.Items.Add(item1);
            group.Items.Add(item2);

            Step.AdjustItems(group, defaultScope);

            Assert.Equal(25, item1.Value);
            Assert.Equal(20, item2.Value);
        }

        [Fact]
        public void AdjustItem_Uses_Item_Scope_If_Item_Found()
        {
            var itemResult = new ItemResult() { Item = new Item() { Scope = 4 }, Value = 100 };

            Step.AdjustItem(itemResult, defaultScope);

            Assert.Equal(25, itemResult.Value);
        }

        [Fact]
        public void AdjustItem_Uses_Default_Scope_If_No_Item_Found()
        {
            var itemResult = new ItemResult() { Item = null, Value = 100 };

            Step.AdjustItem(itemResult, defaultScope);

            Assert.Equal(50, itemResult.Value);
        }


    }
}
