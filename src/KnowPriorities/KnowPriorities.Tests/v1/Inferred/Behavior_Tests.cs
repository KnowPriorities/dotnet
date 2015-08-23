using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.Inferred
{
    public class Behavior_Tests
    {

        [Fact]
        public void GetHeatValue_Adapter_Uses_Correct_Values()
        {
            var behavior = new Behavior() {Heat = 10, UpdatedAt = DateTime.UtcNow.AddDays(-30)};

            var result = behavior.GetHeatValue();

            Assert.Equal(9, result);
        }


        [Fact]
        public void GetHeatValue_Returns_0_If_Count_Is_0()
        {
            var result = Behavior.GetHeatValue(0, DateTime.UtcNow);

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetHeatValue_Reduces_By_1_Tenth_If_30_Days_Old()
        {
            var result = Behavior.GetHeatValue(10, DateTime.UtcNow.AddDays(-30));

            Assert.Equal(9, result);
        }

        [Fact]
        public void GetHeatValue_Does_Not_Reduce_By_1_Tenth_If_29_Days_Old()
        {
            var result = Behavior.GetHeatValue(10, DateTime.UtcNow.AddDays(-29));

            Assert.Equal(10, result);
        }

        [Fact]
        public void GetHeatValue_Zeros_Out_Value_After_10_30Day_Increments()
        {
            var result = Behavior.GetHeatValue(100, DateTime.UtcNow.AddDays(-30 * 10));

            Assert.Equal(0, result);
        }

    }
}
