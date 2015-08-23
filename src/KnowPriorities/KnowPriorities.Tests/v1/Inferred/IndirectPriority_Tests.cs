using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.Inferred
{
    public class IndirectPriority_Tests
    {

        [Fact]
        public void Count_Defaults_To_1()
        {
            var priority = new Behavior();

            Assert.Equal(1, priority.Heat);
        }

        [Fact]
        public void UpdatedAt_Defaults_To_UtcNow_Date()
        {
            var priority = new Behavior();

            Assert.Equal(DateTime.UtcNow.Date, priority.UpdatedAt);
        }


        #region GetInteractionValue

        [Fact]
        public void GetInteractionValue_Depreciates_Count_By_30_Days()
        {
            var priority = new Behavior {Heat = 10, UpdatedAt = DateTime.UtcNow.Date};

            var result1 = priority.GetHeatValue();

            priority.UpdatedAt = DateTime.UtcNow.Date.AddDays(-30);

            var result2 = priority.GetHeatValue();

            Assert.True(result1 > result2);
        }

        [Fact]
        public void GetInteractionValue_Depreciates_To_0_After_10_Months()
        {
            var priority = new Behavior { Heat = 10, UpdatedAt = DateTime.UtcNow.Date.AddDays(-30 * 10) };

            var result = priority.GetHeatValue();

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetInteractionValue_Returns_0_If_Count_Is_Less_Than_1()
        {
            var priority = new Behavior { Heat = 0, UpdatedAt = DateTime.UtcNow.Date };

            var result = priority.GetHeatValue();

            Assert.Equal(0, result);
            
        }


        #endregion

    }
}
