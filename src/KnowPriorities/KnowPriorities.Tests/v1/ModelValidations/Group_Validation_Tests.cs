using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.ModelValidations
{
    public class Group_Validation_Tests: ValidationTest<Group>
    {

        public Group_Validation_Tests()
        {
            context.Id = "a";
            context.Percentage = 1;

            var stakeholder = new Stakeholder() {Volume = 1};
            stakeholder.Priorities.Add("a");

            context.Stakeholders.Add(stakeholder);
        }


        [Fact]
        public void Id_Required()
        {
            context.Id = null;
            ValidateFail();
        }

        [Fact]
        public void Percentage_Minimum()
        {
            const decimal badValue = (decimal) (Group.Default_Percentage_Minimum - Group.Default_Percentage_Minimum);

            context.Percentage = badValue;
            ValidateFail();
        }

        [Fact]
        public void Percentage_Maximum()
        {
            const decimal badValue = (decimal) (1 + Group.Default_Percentage_Minimum);

            context.Percentage = badValue;
            ValidateFail();
        }

        [Fact]
        public void Validates_Stakeholders()
        {
            var stakeholder = new Stakeholder() {Volume = -1};
            stakeholder.Priorities.Add("a");
            context.Stakeholders.Add(stakeholder);

            ValidateFail();
        }

        [Fact]
        public void Validates_Tags()
        {
            var tag = new Tag() { Id = null };

            context.Tags.Add(tag);

            ValidateFail();
        }

    }
}
