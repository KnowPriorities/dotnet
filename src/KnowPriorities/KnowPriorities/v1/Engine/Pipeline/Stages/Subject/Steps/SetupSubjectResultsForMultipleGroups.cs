using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public class SetupSubjectResultsForMultipleGroups : IPrioritizingHandler
    {

        public void Process(PrioritizingRequest request)
        {
            if (request.Subject.HasMultipleGroups)
                AddItems(request.Result.Groups, request.Result.Items);
        }

        public void AddItems(List<GroupResult> groups, List<ItemResult> itemResults)
        {

            var uniqueIds = GetUniqueItemIds(groups);

            foreach (var itemId in uniqueIds)
            {
                var itemResult = new ItemResult() {Id = itemId};

                itemResult.Groups.AddRange(GetGroupsWithItemId(groups, itemId));

                itemResults.Add(itemResult);
            }
        }

        public IEnumerable<GroupResult> GetGroupsWithItemId(List<GroupResult> groups, string itemId)
        {
            return groups.Where(q => q.Items.FirstOrDefault(i => i.Id == itemId) != null);
        }

        public List<string> GetUniqueItemIds(List<GroupResult> groups)
        {
            var itemIds = new List<string>();

            foreach (var group in groups)
            {
                foreach (var item in group.Items)
                {
                    itemIds.Add(item.Id);
                }
            }

            var uniqueIds = itemIds.Distinct().ToList();

            return uniqueIds;
        }

    }
}
