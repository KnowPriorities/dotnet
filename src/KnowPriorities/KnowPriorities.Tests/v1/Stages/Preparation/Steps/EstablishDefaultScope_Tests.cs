using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline.Stages.Preparation.Steps;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Preparation.Steps
{
    public class EstablishDefaultScope_Tests : PrioritizingHandlerTests
    {
        private readonly EstablishDefaultScope Step = new EstablishDefaultScope();

        [Fact]
        public void If_No_Items_DefaultScope_Is_1()
        {
            _subject.Items.Clear();

            Step.Process(_request);

            Assert.Equal(1, _subject.DefaultScope);
        }

        [Fact]
        public void Sets_DefaultScope_To_Max_Item_Scope_Found()
        {
            _subject.Items.Clear();
            _subject.Items.Add(new Item() { Scope = 5 });
            _subject.Items.Add(new Item() { Scope = 50 });

            Step.Process(_request);

            Assert.Equal(50, _subject.DefaultScope);

        }

    }
}
