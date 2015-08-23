using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;

namespace KnowPriorities.v1.Engine.Pipeline.Stages.Preparation.Steps
{
    public class ApplyItemScopeAdjustmentsFromTags : IPrioritizingHandler
    {
        public void Process(PrioritizingRequest request)
        {
            request.Subject.Items.ForEach(i => ApplyTags(i, request.Subject.Tags));
        }

        public void ApplyTags(Item item, List<Tag> tags)
        {
            foreach (var id in item.TagIds)
            {
                var subjectTag = tags.FirstOrDefault(q => q.Id == id);

                if (subjectTag == null)
                    continue;

                if (subjectTag.AbsoluteScope.HasValue)
                {
                    item.Scope = subjectTag.AbsoluteScope.Value;
                    break;
                }
                
                if (subjectTag.AdjustScope.HasValue)
                {
                    item.Scope += subjectTag.AdjustScope.Value;
                }
            }

            NormalizeScope(item);
        }

        public void NormalizeScope(Item item)
        {
            if (item.Scope < 1)
                item.Scope = 1;

            if (item.Scope > 10)
                item.Scope = 10;
        }

    }
}
