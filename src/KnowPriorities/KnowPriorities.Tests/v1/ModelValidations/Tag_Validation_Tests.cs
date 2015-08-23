using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Schema;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.ModelValidations
{
    public class Tag_Validation_Tests : ValidationTest<Tag>
    {
        public Tag_Validation_Tests()
        {
            context.Id = "a";
        }

        [Fact]
        public void Id_Required()
        {
            context.AbsoluteScope = 1;
            ValidateNoFail();

            context.Id = null;
            ValidateFail();
        }

        [Fact]
        public void AbsoluteVolume_Minimum_Is_0()
        {
            context.AbsoluteVolume = 0;
            ValidateNoFail();

            context.AbsoluteVolume = -1;
            ValidateFail();
        }

        [Fact]
        public void AbsoluteVolume_Maximum_Is_10()
        {
            context.AbsoluteVolume = 10;
            ValidateNoFail();

            context.AbsoluteVolume = 11;
            ValidateFail();
        }

        [Fact]
        public void AdjustVolume_Minimum_Is_Negative_10()
        {
            context.AdjustVolume = -10;
            ValidateNoFail();

            context.AdjustVolume = -11;
            ValidateFail();
        }

        [Fact]
        public void AdjustVolume_Maximum_Is_10()
        {
            context.AdjustVolume = 10;
            ValidateNoFail();

            context.AdjustVolume = 11;
            ValidateFail();
        }

        [Fact]
        public void AbsoluteScope_Minimum_Is_1()
        {
            context.AbsoluteScope = 1;
            ValidateNoFail();

            context.AbsoluteScope = 0;
            ValidateFail();
        }

        [Fact]
        public void AdjustScope_Minimum_Is_Negative_10()
        {
            context.AdjustScope = -10;
            ValidateNoFail();

            context.AdjustScope = -11;
            ValidateFail();
        }

        [Fact]
        public void AdjustScope_Maximum_Is_10()
        {
            context.AdjustScope = 10;
            ValidateNoFail();

            context.AdjustScope = 11;
            ValidateFail();
        }

        [Fact]
        public void At_Least_1_Absolute_Or_Adjust_Value_Must_Be_Set()
        {
            ValidateFail();
        }

        [Fact]
        public void Adjustment_Values_Cannot_Be_Zero()
        {
            context.AdjustVolume = 0;
            ValidateFail();

            context.AdjustVolume = null;
            context.AdjustScope = 0;
            ValidateFail();
        }

        [Fact]
        public void Absolute_And_Adjust_Volume_Cannot_Both_Be_Set()
        {
            context.AbsoluteVolume = 1;
            context.AdjustVolume = 1;

            ValidateFail();
        }

        [Fact]
        public void Absolute_And_Adjust_Scope_Cannot_Both_Be_Set()
        {
            context.AbsoluteScope = 1;
            context.AdjustScope = 1;

            ValidateFail();
        }

    }
}
