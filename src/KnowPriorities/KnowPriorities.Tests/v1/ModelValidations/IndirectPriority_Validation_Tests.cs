using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.ModelValidations
{
    public class IndirectPriority_Validation_Tests : ValidationTest<Behavior>
    {

        public IndirectPriority_Validation_Tests()
        {
            context.Id = "a";
        }

        [Fact]
        public void Id_Required()
        {
            context.Id = null;
            ValidateFail();
        }

        [Fact]
        public void Count_Minimum()
        {
            context.Heat = 0;
            ValidateFail();
        }


    }
}
