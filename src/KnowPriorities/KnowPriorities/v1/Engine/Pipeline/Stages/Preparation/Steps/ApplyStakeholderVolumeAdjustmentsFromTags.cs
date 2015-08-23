using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;

namespace KnowPriorities.v1.Engine.Pipeline.Stages.Preparation.Steps
{
    public class ApplyStakeholderVolumeAdjustmentsFromTags : IPrioritizingHandler
    {
        public void Process(PrioritizingRequest request)
        {
            request.Subject.Groups.ForEach(g => ApplyTags(g, request.Subject.Tags));
        }

        public void ApplyTags(Group group, List<Tag> subjectTags)
        {
            group.Stakeholders.ForEach(s => ApplyTags(s, group.Tags, subjectTags));
        }

        public void ApplyTags(Stakeholder stakeholder, List<Tag> groupTags, List<Tag> subjectTags)
        {
            foreach (var id in stakeholder.TagIds)
            {
                bool absoluteTagApplied;
                bool adjustTagApplied;

                // 1. Apply group-level tags first as they override subject level
                var groupTag = groupTags.FirstOrDefault(q => q.Id == id);

                ApplyTag(stakeholder, groupTag, out absoluteTagApplied, out adjustTagApplied);

                if (absoluteTagApplied)
                    break;

                if (adjustTagApplied)
                    continue;

                // 2. Apply subject-level tags
                var subjectTag = subjectTags.FirstOrDefault(q => q.Id == id);

                ApplyTag(stakeholder, subjectTag, out absoluteTagApplied, out adjustTagApplied);

                if (absoluteTagApplied)
                    break;

                //if (adjustTagApplied)
                //    continue;
            }

            NormalizeVolume(stakeholder);
        }

        public void NormalizeVolume(Stakeholder stakeholder)
        {
            if (stakeholder.Volume < 0)
                stakeholder.Volume = 0;

            if (stakeholder.Volume > 10)
                stakeholder.Volume = 10;
        }

        public void ApplyTag(Stakeholder stakeholder, Tag tag, out bool absoluteTagApplied, out bool adjustTagApplied)
        {
            absoluteTagApplied = false;
            adjustTagApplied = false;

            if (tag != null)
            {
                if (tag.AbsoluteVolume.HasValue)
                {
                    stakeholder.Volume = tag.AbsoluteVolume.Value;
                    absoluteTagApplied = true;
                }

                if (tag.AdjustVolume.HasValue)
                {
                    stakeholder.Volume += tag.AdjustVolume.Value;
                    adjustTagApplied = true;
                }
            }
        }
    }
}
