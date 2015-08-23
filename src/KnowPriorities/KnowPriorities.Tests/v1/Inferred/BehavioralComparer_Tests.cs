using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Inferred;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.Inferred
{
    public class BehavioralComparer_Tests
    {

        [Fact]
        public void GetComparer_Returns_SeriesComparer_If_ScaleSeriesCount_Greater_Than_0()
        {
            var scale = new WeightScale("a", "b");

            var result = BehavioralComparer.GetComparer(scale);

            Assert.IsType<SeriesComparer>(result);
        }

        [Fact]
        public void GetComparer_Returns_RangeComparer_If_ScaleSeriesCount_Equals_0()
        {
            var scale = new WeightScale(highValue:true);

            var result = BehavioralComparer.GetComparer(scale);

            Assert.IsType<RangeComparer>(result);
        }

        #region CompareAsHeat

        [Fact]
        public void CompareAsHeat_Sorts_By_Count_If_Same_Date()
        {
            var scale = new WeightScale();
            var priorities = new List<Behavior>
            {
                new Behavior() {Id = "a", Heat = 1},
                new Behavior() { Id = "b", Heat = 2 }
            };

            priorities.Sort(BehavioralComparer.GetComparer(scale));

            Assert.Equal("b", priorities[0].Id);
            Assert.Equal("a", priorities[1].Id);
        }

        [Fact]
        public void CompareAsHeat_Sorts_By_Date_If_Same_Count()
        {
            var scale = new WeightScale();
            var priorities = new List<Behavior>
            {
                new Behavior() {Id = "a", Heat = 3, UpdatedAt = DateTime.UtcNow.Date.AddDays(-30)},
                new Behavior() { Id = "b", Heat = 3, UpdatedAt = DateTime.UtcNow.Date }
            };

            priorities.Sort(BehavioralComparer.GetComparer(scale));

            Assert.Equal("b", priorities[0].Id);
            Assert.Equal("a", priorities[1].Id);
        }

        [Fact]
        public void CompareAsHeat_Sorts_Depreciated_Values_With_Different_Heat_Lower()
        {
            var scale = new WeightScale();
            var priorities = new List<Behavior>
            {
                new Behavior() {Id = "a", Heat = 10, UpdatedAt = DateTime.UtcNow.Date.AddDays(-60)},
                new Behavior() {Id = "b", Heat = 9, UpdatedAt = DateTime.UtcNow.Date}
            };

            priorities.Sort(BehavioralComparer.GetComparer(scale));

            Assert.Equal("b", priorities[0].Id);
            Assert.Equal("a", priorities[1].Id);
        }


        #endregion

    }
}
