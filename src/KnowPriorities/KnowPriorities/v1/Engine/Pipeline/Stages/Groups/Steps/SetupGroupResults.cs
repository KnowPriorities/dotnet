using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public class SetupGroupResults : IPrioritizingHandler
    {

        public void Process(PrioritizingRequest request)
        {
            request.Subject.Groups.ForEach(group => SetupGroup(group, request));
        }

        public void SetupGroup(Group group, PrioritizingRequest request)
        {
            var groupResult = new GroupResult()
            {
                Id = group.Id,
                Percentage = group.Percentage,
                Group = group
            };

            AddItems(group.Stakeholders, groupResult.Items, request.Subject.Items);

            request.Result.Groups.Add(groupResult);
        }

        public void AddItems(List<Stakeholder> stakeholders, List<ItemResult> itemResults, List<Item> subjectItems)
        {
            var uniqueIds = GetUniqueItemIds(stakeholders);

            foreach (var itemId in uniqueIds)
            {
                var itemResult = new ItemResult() {Id = itemId, Item=subjectItems.FirstOrDefault(q=> q.Id==itemId)};

                itemResult.Stakeholders.AddRange(GetStakeholdersWithItemId(stakeholders, itemId));

                itemResults.Add(itemResult);
            }
        }

        public IEnumerable<Stakeholder> GetStakeholdersWithItemId(List<Stakeholder> stakeholders, string itemId)
        {
            return stakeholders.Where(q => q.Priorities.Contains(itemId));
        }

        public List<string> GetUniqueItemIds(List<Stakeholder> stakeholders)
        {
            var itemIds = new List<string>();

            foreach (var stakeholder in stakeholders)
            {
                itemIds.AddRange(stakeholder.Priorities);
            }

            var uniqueIds = itemIds.Distinct();

            return uniqueIds.ToList();
        }

    }
}
