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
    public class RangeComparer_Tests
    {


        #region CompareAsRange

        [Fact]
        public void CompareAsRange_HighValue_Sorts_By_Higher_Values()
        {
            var scale = new WeightScale();
            var priorities = new List<Behavior>
            {
                new Behavior() {Id = "a", RangeValue = 1},
                new Behavior() {Id = "b", RangeValue = 2}
            };

            priorities.Sort(BehavioralComparer.GetComparer(scale));

            Assert.Equal("b", priorities[0].Id);
            Assert.Equal("a", priorities[1].Id);
        }

        [Fact]
        public void CompareAsRange_Depreciates_Values()
        {
            var scale = new WeightScale() { Depreciation = 1m };
            var priorities = new List<Behavior>
            {
                new Behavior() {Id = "a", RangeValue = 1, UpdatedAt = DateTime.UtcNow.AddDays(-30)},
                new Behavior() {Id = "b", RangeValue = 1, UpdatedAt = DateTime.UtcNow}
            };

            priorities.Sort(BehavioralComparer.GetComparer(scale));

            Assert.Equal("b", priorities[0].Id);
            Assert.Equal("a", priorities[1].Id);
        }

        [Fact]
        public void CompareAsRange_HighValue_Sorts_UnParseable_Values_At_Bottom()
        {
            var scale = new WeightScale();
            var priorities = new List<Behavior>
            {
                new Behavior() {Id = "a", RangeValue = null},
                new Behavior() {Id = "b", RangeValue = 2}
            };

            priorities.Sort(BehavioralComparer.GetComparer(scale));

            Assert.Equal("b", priorities[0].Id);
            Assert.Equal("a", priorities[1].Id);
        }

        [Fact]
        public void CompareAsRange_LowValue_Sorts_By_Lower_Values()
        {
            var scale = new WeightScale(highValue: false);
            var priorities = new List<Behavior>
            {
                new Behavior() {Id = "a", RangeValue = 2},
                new Behavior() {Id = "b", RangeValue = 1}
            };

            priorities.Sort(BehavioralComparer.GetComparer(scale));

            Assert.Equal("b", priorities[0].Id);
            Assert.Equal("a", priorities[1].Id);
        }

        [Fact]
        public void CompareAsRange_LowValue_Sorts_UnParseable_Values_At_Bottom()
        {
            var scale = new WeightScale(highValue: false);
            var priorities = new List<Behavior>
            {
                new Behavior() {Id = "a", RangeValue = null},
                new Behavior() {Id = "b", RangeValue = 1}
            };

            priorities.Sort(BehavioralComparer.GetComparer(scale));

            Assert.Equal("b", priorities[0].Id);
            Assert.Equal("a", priorities[1].Id);
        }

        #endregion

        [Fact]
        public void CompareAsHeat_Is_Used_If_Depreciated_Values_Are_Same()
        {
            var scale = new WeightScale();
            var priorities = new List<Behavior>
            {
                new Behavior() {Id = "a", RangeValue = 1, Heat = 1},
                new Behavior() {Id = "b", RangeValue = 1, Heat = 2}
            };

            priorities.Sort(BehavioralComparer.GetComparer(scale));

            Assert.Equal("b", priorities[0].Id);
            Assert.Equal("a", priorities[1].Id);
        }
    }
}
