using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using KnowPriorities.v1.Engine.Inferred;
using KnowPriorities.v1.Models;

namespace KnowPriorities.Tests.v1.Inferred
{
    public class SeriesComparer_Tests
    {


        [Fact]
        public void CompareAsSeries_Lower_Index_Values_Are_Sorted_Higher()
        {
            var scale = new WeightScale(series: new[] { "1", "2", "3" });
            var priorities = new List<Behavior>
            {
                new Behavior() {Id = "a", SeriesValue = "3"},
                new Behavior() {Id = "b", SeriesValue = "1"}
            };

            priorities.Sort(BehavioralComparer.GetComparer(scale));

            Assert.Equal("b", priorities[0].Id);
            Assert.Equal("a", priorities[1].Id);
        }

        [Fact]
        public void CompareAsSeries_Depreciates_Values()
        {
            var scale = new WeightScale(series: new[] { "1", "2", "3" }) { Depreciation = 1m };
            var priorities = new List<Behavior>
            {
                new Behavior() {Id = "a", SeriesValue = "1", UpdatedAt = DateTime.UtcNow.AddDays(-30)},
                new Behavior() {Id = "b", SeriesValue = "1", UpdatedAt = DateTime.UtcNow}
            };

            priorities.Sort(BehavioralComparer.GetComparer(scale));

            Assert.Equal("b", priorities[0].Id);
            Assert.Equal("a", priorities[1].Id);
        }

        [Fact]
        public void CompareAsSeries_Values_Not_Found_Are_Sorted_To_Bottom()
        {
            var scale = new WeightScale(series: new[] { "1", "2", "3" });
            var priorities = new List<Behavior>
            {
                new Behavior() {Id = "a", SeriesValue = "5"},
                new Behavior() {Id = "b", SeriesValue = "1"}
            };

            priorities.Sort(BehavioralComparer.GetComparer(scale));

            Assert.Equal("b", priorities[0].Id);
            Assert.Equal("a", priorities[1].Id);
        }


        [Fact]
        public void CompareAsHeat_Is_Used_If_Depreciated_Values_Are_Same()
        {
            var scale = new WeightScale(series: new[] { "1", "2", "3" });
            var priorities = new List<Behavior>
            {
                new Behavior() {Id = "a", SeriesValue = "3", Heat = 1},
                new Behavior() {Id = "b", SeriesValue = "3", Heat = 2}
            };

            priorities.Sort(BehavioralComparer.GetComparer(scale));

            Assert.Equal("b", priorities[0].Id);
            Assert.Equal("a", priorities[1].Id);
        }

    }
}
