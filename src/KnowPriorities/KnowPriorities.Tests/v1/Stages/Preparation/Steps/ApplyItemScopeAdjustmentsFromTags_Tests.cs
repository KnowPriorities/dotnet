using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Engine.Pipeline.Stages.Preparation.Steps;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Preparation.Steps
{
    public class ApplyItemScopeAdjustmentsFromTags_Tests
    {

        private readonly ApplyItemScopeAdjustmentsFromTags Step = new ApplyItemScopeAdjustmentsFromTags();

        private readonly Item context = new Item() {Id = "a", Scope = 1};
        private readonly List<Tag> Tags = new List<Tag>();

        public ApplyItemScopeAdjustmentsFromTags_Tests()
        {
            context.TagIds.Add("a");
        }

        [Fact]
        public void Adjusts_Scope_From_Tag()
        {
            Tags.Add(new Tag() {Id = "a", AdjustScope = 1});

            Step.ApplyTags(context, Tags);

            Assert.Equal(2, context.Scope);
        }

        [Fact]
        public void Absolute_Scope_From_Tag_Overrides_Scope()
        {
            Tags.Add(new Tag() { Id = "a", AbsoluteScope = 7 });

            Step.ApplyTags(context, Tags);

            Assert.Equal(7, context.Scope);
        }

        [Fact]
        public void Absolute_Scope_Halts_Further_Processing_On_Item()
        {
            context.TagIds.Add("b");
            context.TagIds.Add("c");

            Tags.Add(new Tag() { Id = "a", AbsoluteScope = 5 });
            Tags.Add(new Tag() { Id = "b", AdjustScope = -3 });
            Tags.Add(new Tag() { Id = "c", AbsoluteScope = 2 });

            Step.ApplyTags(context, Tags);

            Assert.Equal(5, context.Scope);
        }

        [Fact]
        public void Continues_Applying_Tags_Even_If_Tag_Id_Not_Found()
        {
            context.TagIds.Add("notfound");
            context.TagIds.Add("c");

            Tags.Add(new Tag() { Id = "a", AdjustScope = 5 });
            Tags.Add(new Tag() { Id = "c", AdjustScope = -2 });

            Step.ApplyTags(context, Tags);

            Assert.Equal(4, context.Scope);
        }

        [Fact]
        public void AdjustScope_Continues_Applying_Tags()
        {
            context.TagIds.Add("b");

            Tags.Add(new Tag() { Id = "a", AdjustScope = 1 });
            Tags.Add(new Tag() { Id = "b", AdjustScope = 1 });

            Step.ApplyTags(context, Tags);

            Assert.Equal(3, context.Scope);
        }


        [Fact]
        public void Ensures_Scope_Minimum_Of_1()
        {
            Tags.Add(new Tag() {Id = "a", AdjustScope = -10});

            Step.ApplyTags(context, Tags);

            Assert.Equal(1, context.Scope);
        }


        [Fact]
        public void Processes_All_Items()
        {
            var subject = new KnowPriorities.v1.Models.Subject();

            var item1 = new Item() {Id = "1", Scope = 1};
            var item2 = new Item() { Id = "2", Scope = 1 };

            item1.TagIds.Add("a");
            item2.TagIds.Add("b");

            subject.Tags.Add(new Tag() { Id = "a", AdjustScope = 1 });
            subject.Tags.Add(new Tag() { Id = "b", AdjustScope = 2 });

            subject.Items.Add(item1);
            subject.Items.Add(item2);

            var request = new PrioritizingRequest(subject);

            Step.Process(request);

            Assert.Equal(2, item1.Scope);
            Assert.Equal(3, item2.Scope);

        }

    }
}
