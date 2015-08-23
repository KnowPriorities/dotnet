using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.ModelValidations
{
    public class Stakeholder_Validation_Tests : ValidationTest<Stakeholder>
    {

        public Stakeholder_Validation_Tests()
        {
            context.Priorities.Add("a");
        }


        [Fact]
        public void Volume_Is_At_Least_0()
        {
            context.Volume = -1;
            ValidateFail();
        }

        [Fact]
        public void Volume_Is_No_More_Than_10()
        {
            context.Volume = 11;
            ValidateFail();
        }

    }
}
