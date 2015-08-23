using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using Xunit;

namespace KnowPriorities.Tests.v1
{
    public class Prioritizer_Load_Tests
    {

        private readonly Prioritizer _prioritizer = new Prioritizer();

        [Fact]
        public void Can_Prioritize_Tiny_Set_Of_25_Stakeholders()
        {
            var subject = Simulate.Tiny_Set_Of_25_Stakeholders();

            var results = _prioritizer.Prioritize(subject);

            Assert.True(results.Items.Count > 0);
        }


        [Fact]
        public void Can_Prioritize_Small_Set_Of_250_Stakeholders()
        {
            var subject = Simulate.Small_Set_Of_250_Stakeholders();

            var results = _prioritizer.Prioritize(subject);

            Assert.True(results.Items.Count > 0);
        }


        [Fact]
        public void Can_Prioritize_Medium_Set_of_2k_Stakeholders()
        {
            var subject = Simulate.Medium_Set_of_2k_Stakeholders();

            var results = _prioritizer.Prioritize(subject);

            Assert.True(results.Items.Count > 0);
        }

    }
}
