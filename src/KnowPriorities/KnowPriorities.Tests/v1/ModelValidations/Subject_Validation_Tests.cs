using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.ModelValidations
{
    public class Subject_Validation_Tests : ValidationTest<Subject>
    {

        public Subject_Validation_Tests()
        {
            var group = new Group() {Id = "a", Percentage = 1};
            var stakeholder = new Stakeholder() { Volume = 1 };

            stakeholder.Priorities.Add("a");

            group.Stakeholders.Add(stakeholder);

            context.Groups.Add(group);
        }

        [Fact]
        public void DaysToAge_Minimum_Of_1()
        {
            context.DaysToAge = 0;
            ValidateFail();
        }

        [Fact]
        public void DaysToAge_Maximum_Of_90()
        {
            context.DaysToAge = 91;
            ValidateFail();
        }

        [Fact]
        public void AsOf_Cannot_Be_In_Future()
        {
            context.AsOf = DateTime.UtcNow.AddDays(1);
            ValidateFail();
        }

        [Fact]
        public void At_Least_1_Group_Is_Required()
        {
            context.Groups.Clear();
            ValidateFail();
        }

        [Fact]
        public void At_Least_1_Stakeholder_Is_Required()
        {
            context.Groups.First().Stakeholders.Clear();
            ValidateFail();
        }

        [Fact]
        public void Group_Percentages_Must_Total_To_1()
        {
            context.Groups.First().Percentage = 0.99m;
            ValidateFail();
        }

        [Fact]
        public void Validates_Items()
        {
            var item = new Item() {Id = "a", Scope = -1};

            context.Items.Add(item);

            ValidateFail();
        }

        [Fact]
        public void Validates_Groups()
        {
            context.Groups.First().Id = null;

            ValidateFail();
        }

        [Fact]
        public void Validates_Tags()
        {
            var tag = new Tag() {Id = null};

            context.Tags.Add(tag);

            ValidateFail();
        }
    }
}
