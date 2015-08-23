using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1
{
    public class Prioritizer_Tests
    {
        private readonly Prioritizer _prioritizer = new Prioritizer();
        private readonly Subject _subject = new Subject();
        private readonly Group _group1 = new Group() {Id = "everyone"};
        private readonly Stakeholder _stakeholder1;
        private readonly Stakeholder _stakeholder2;


        public Prioritizer_Tests()
        {
            _stakeholder1 = new Stakeholder();
            _stakeholder2 = new Stakeholder();

            _stakeholder1.Priorities.Add("a");
            _stakeholder1.Priorities.Add("b");
            _stakeholder1.Priorities.Add("c");

            _stakeholder2.Priorities.Add("c");
            _stakeholder2.Priorities.Add("a");
            _stakeholder2.Priorities.Add("b");

            _group1.Stakeholders.Add(_stakeholder1);
            _group1.Stakeholders.Add(_stakeholder2);

            _subject.Groups.Add(_group1);
        }


        [Fact]
        public void Simple_Set_Without_Item_Group_Or_Volume_Differences()
        {
            var result = _prioritizer.Prioritize(_subject);

            Assert.Equal(3, result.Items.Count);
            Assert.Equal("a", result.Items[0].Id);
            Assert.Equal("c", result.Items[1].Id);
            Assert.Equal("b", result.Items[2].Id);
        }


        [Fact]
        public void Simple_Set_With_Volume_Differences()
        {
            _stakeholder1.Volume = 3; //2 is not different enough to override stk2's #1
            _stakeholder2.Volume = 1;
            
            var result = _prioritizer.Prioritize(_subject);

            Assert.Equal(3, result.Items.Count);
            Assert.Equal("a", result.Items[0].Id);
            Assert.Equal("b", result.Items[1].Id);
            Assert.Equal("c", result.Items[2].Id);
        }


        [Fact]
        public void Simple_Set_With_Group_Percentage_Differences()
        {
            var group2 = new Group() { Id = "b", Percentage = 0.49m };
            _subject.Groups.Add(group2);
            
            _group1.Stakeholders.Remove(_stakeholder2);
            _group1.Percentage = 0.51m;
            
            _stakeholder1.Volume = 1;
            _stakeholder2.Volume = 1;

            var result = _prioritizer.Prioritize(_subject);

            Assert.Equal(3, result.Items.Count);
            Assert.Equal("a", result.Items[0].Id);
            Assert.Equal("b", result.Items[1].Id);
            Assert.Equal("c", result.Items[2].Id);
        }

        [Fact]
        public void Stakeholder_Volume_Is_Isolated_To_Each_Groups()
        {
            _subject.Groups.Clear();

            var group1 = new Group() { Id = "a", Percentage = 0.50m };
            _subject.Groups.Add(group1);
            group1.Stakeholders.Add(_stakeholder1);
            _stakeholder1.Volume = 1;

            var group2 = new Group() { Id = "b", Percentage = 0.50m };
            _subject.Groups.Add(group2);
            group2.Stakeholders.Add(_stakeholder2);
            _stakeholder2.Volume = 10;

            var result = _prioritizer.Prioritize(_subject);

            Assert.Equal(3, result.Items.Count);
            Assert.Equal("a", result.Items[0].Id);
            Assert.Equal("c", result.Items[1].Id);
            Assert.Equal("b", result.Items[2].Id);
       }

        [Fact]
        public void Stakeholder_Groups_Are_Prioritized_Using_Their_Percentages()
        {
            // the tipping point for 2 people is 73% vs 27% to get person A's choices
            
            _subject.Groups.Clear();

            var group1 = new Group() { Id = "a", Percentage = 0.73m };
            _subject.Groups.Add(group1);
            group1.Stakeholders.Add(_stakeholder1);

            var group2 = new Group() { Id = "b", Percentage = 0.27m };
            _subject.Groups.Add(group2);
            group2.Stakeholders.Add(_stakeholder2);

            var result = _prioritizer.Prioritize(_subject);

            Assert.Equal(3, result.Items.Count);
            Assert.Equal("a", result.Items[0].Id);
            Assert.Equal("b", result.Items[1].Id);
            Assert.Equal("c", result.Items[2].Id);
        }


        [Fact]
        public void Stakeholder_Items_Are_Prioritized_Using_Their_LastUpdated()
        {
            _stakeholder1.UpdatedAt = _subject.AsOf.AddDays(_subject.DaysToAge * -1);

            var result = _prioritizer.Prioritize(_subject);

            Assert.Equal(3, result.Items.Count);
            Assert.Equal("a", result.Items[0].Id);
            Assert.Equal("c", result.Items[1].Id);
            Assert.Equal("b", result.Items[2].Id);
        }

        [Fact]
        public void Can_Prioritize_When_Different_Stakeholders_Have_Different_Systems_Of_Value()
        {
            var stakeholder3 = new Stakeholder() { Volume = 1 };
            var stakeholder4 = new Stakeholder() { Volume = 1 };

            _group1.Stakeholders.Add(stakeholder3);
            _group1.Stakeholders.Add(stakeholder4);

            _subject.Scale.Series.AddRange(new List<string> { "a", "b", "c" });

            stakeholder3.Behaviors.Add(new Behavior() { Id = "b", SeriesValue = "c" });
            stakeholder4.Behaviors.Add(new Behavior() { Id = "c", Heat = 1 });

            var result = _prioritizer.Prioritize(_subject);

            Assert.Equal(3, result.Items.Count);
        }

    }
}
