using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline.Stages.Groups.Steps
{
    public class AdjustGroupValuesRelativeToItemScope : IPrioritizingHandler
    {

        public void Process(PrioritizingRequest request)
        {
            request.Result.Groups.ForEach(g => AdjustItems(g, request.Subject.DefaultScope));
        }

        public void AdjustItems(GroupResult group, long defaultScope)
        {
            group.Items.ForEach(i => AdjustItem(i, defaultScope));
        }

        public void AdjustItem(ItemResult itemResult, long defaultScope)
        {
            var scope = itemResult.Item == null ? defaultScope : itemResult.Item.Scope;

            itemResult.Value /= scope;
        }

    }
}
