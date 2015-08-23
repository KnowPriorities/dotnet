using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.ModelValidations
{
    public class Item_Validation_Tests : ValidationTest<Item>
    {
        public Item_Validation_Tests()
        {
            context.Id = "a";
            context.Scope = 1;
        }

        [Fact]
        public void Id_Required()
        {
            context.Id = null;
            ValidateFail();
        }

        [Fact]
        public void Scope_Greater_Than_0()
        {
            context.Scope = 0;
            ValidateFail();
        }

    }
}
