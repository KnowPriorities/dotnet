using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Engine.Pipeline.Stages.Preparation.Steps;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Preparation.Steps
{
    public class ApplyStakeholderVolumeAdjustmentsFromTags_Tests
    {

        private readonly ApplyStakeholderVolumeAdjustmentsFromTags Step = new ApplyStakeholderVolumeAdjustmentsFromTags();

        private readonly Stakeholder context = new Stakeholder();
        private readonly List<Tag> GroupTags = new List<Tag>();
        private readonly List<Tag> SubjectTags = new List<Tag>();

        private bool absoluteTagApplied = false;
        private bool adjustTagApplied = false;

        public ApplyStakeholderVolumeAdjustmentsFromTags_Tests()
        {
            context.TagIds.Add("a");
        }


        [Fact]
        public void Normalize_Volume_Ensures_Minimum_Of_0()
        {
            context.Volume = -1;
            
            Step.NormalizeVolume(context);

            Assert.Equal(0, context.Volume);
        }

        [Fact]
        public void Normalize_Volume_Ensures_Maximum_Of_10()
        {
            context.Volume = 11;

            Step.NormalizeVolume(context);

            Assert.Equal(10, context.Volume);
        }

        [Fact]
        public void ApplyTag_Does_Nothing_If_No_Tag()
        {
            Step.ApplyTag(context, null, out absoluteTagApplied, out adjustTagApplied);

            Assert.Equal(Stakeholder.Default_Volume, context.Volume);
        }

        [Fact]
        public void ApplyTag_Applies_Absolute_Volume_If_Supplied()
        {
            var tag = new Tag() { Id = "a", AbsoluteVolume = 10 };

            Step.ApplyTag(context, tag, out absoluteTagApplied, out adjustTagApplied);

            Assert.Equal(10, context.Volume);
            Assert.True(absoluteTagApplied);
            Assert.False(adjustTagApplied);
        }

        [Fact]
        public void ApplyTag_Applies_Adjust_Volume_If_Supplied()
        {
            var tag = new Tag() {Id = "a", AdjustVolume = 1};

            Step.ApplyTag(context, tag, out absoluteTagApplied, out adjustTagApplied);

            Assert.Equal((Stakeholder.Default_Volume + 1), context.Volume);
            Assert.True(adjustTagApplied);
            Assert.False(absoluteTagApplied);
        }

        [Fact]
        public void ApplyTags_Applies_Group_Level_Tags()
        {
            GroupTags.Add(new Tag() {Id = "a", AbsoluteVolume = 5});

            Step.ApplyTags(context, GroupTags, SubjectTags);

            Assert.Equal(5, context.Volume);
        }

        [Fact]
        public void ApplyTags_Applies_Subject_Level_Tags()
        {
            SubjectTags.Add(new Tag() { Id = "a", AbsoluteVolume = 5 });

            Step.ApplyTags(context, GroupTags, SubjectTags);

            Assert.Equal(5, context.Volume);
        }

        [Fact]
        public void ApplyTags_Applies_Overrides_Subject_Level_With_Group_Level_Tag_When_Absolute()
        {
            GroupTags.Add(new Tag() { Id = "a", AbsoluteVolume = 7 });
            SubjectTags.Add(new Tag() { Id = "a", AbsoluteVolume = 5 });

            Step.ApplyTags(context, GroupTags, SubjectTags);

            Assert.Equal(7, context.Volume);
        }

        [Fact]
        public void ApplyTags_Applies_Overrides_Subject_Level_With_Group_Level_Tag_When_Adjustment()
        {
            GroupTags.Add(new Tag() { Id = "a", AdjustVolume = 1 });
            SubjectTags.Add(new Tag() { Id = "a", AdjustVolume = 2 });

            Step.ApplyTags(context, GroupTags, SubjectTags);

            Assert.Equal(Stakeholder.Default_Volume + 1, context.Volume);
        }

        [Fact]
        public void ApplyTags_Applies_All_Tags()
        {
            context.TagIds.Add("b");
            
            GroupTags.Add(new Tag() { Id = "a", AdjustVolume = 1 });
            SubjectTags.Add(new Tag() { Id = "b", AdjustVolume = 2 });

            Step.ApplyTags(context, GroupTags, SubjectTags);

            Assert.Equal(Stakeholder.Default_Volume + 3, context.Volume);
        }

        [Fact]
        public void ApplyTags_Normalizes_Volume_After_Applying_Tags()
        {
            GroupTags.Add(new Tag() { Id = "a", AdjustVolume = 10 });

            Step.ApplyTags(context, GroupTags, SubjectTags);

            Assert.Equal(10, context.Volume);
        }

        [Fact]
        public void ApplyTags_For_All_Stakeholders_In_A_Group()
        {
            var group = new Group();

            group.Tags.Add(new Tag() { Id = "a", AdjustVolume = 1 });
            group.Tags.Add(new Tag() { Id = "b", AdjustVolume = 2 });

            var stakeholder1 = new Stakeholder();
            var stakeholder2 = new Stakeholder();

            stakeholder1.TagIds.Add("a");
            stakeholder2.TagIds.Add("b");

            group.Stakeholders.Add(stakeholder1);
            group.Stakeholders.Add(stakeholder2);

            Step.ApplyTags(group, SubjectTags);

            Assert.Equal(Stakeholder.Default_Volume + 1, stakeholder1.Volume);
            Assert.Equal(Stakeholder.Default_Volume + 2, stakeholder2.Volume);

        }

        [Fact]
        public void ApplyTags_For_All_Groups()
        {
            var group1 = new Group();
            var group2 = new Group();

            var stakeholder1 = new Stakeholder();
            var stakeholder2 = new Stakeholder();

            stakeholder1.TagIds.Add("a");
            stakeholder2.TagIds.Add("b");

            group1.Stakeholders.Add(stakeholder1);
            group2.Stakeholders.Add(stakeholder2);

            var subject = new KnowPriorities.v1.Models.Subject();

            subject.Groups.Add(group1);
            subject.Groups.Add(group2);

            group1.Tags.Add(new Tag() { Id = "a", AdjustVolume = 1 });
            subject.Tags.Add(new Tag() { Id = "b", AdjustVolume = 2 });

            var request = new PrioritizingRequest(subject);

            Step.Process(request);

            Assert.Equal(Stakeholder.Default_Volume + 1, stakeholder1.Volume);
            Assert.Equal(Stakeholder.Default_Volume + 2, stakeholder2.Volume);
        }
    }
}
