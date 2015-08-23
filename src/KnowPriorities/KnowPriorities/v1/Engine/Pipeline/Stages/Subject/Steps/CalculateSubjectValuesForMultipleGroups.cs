using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public class CalculateSubjectValuesForMultipleGroups : IPrioritizingHandler
    {
        public void Process(PrioritizingRequest request)
        {
            if(request.Subject.HasMultipleGroups)
                request.Result.Items.ForEach(Process);
        }

        public void Process(ItemResult item)
        {
            item.Groups.ForEach(group => UpdateItemValue(item, group));
        }

        public void UpdateItemValue(ItemResult item, GroupResult group)
        {
            var priorityNumber = GetPriorityNumber(group.Items, item.Id);

            var increase = GetIncrease(priorityNumber, group.Percentage);

            item.Value += increase;
        }

        public int GetPriorityNumber(List<ItemResult> items, string itemId)
        {
            for (var x = 0; x < items.Count; x++)
            {
                if (items[x].Id == itemId)
                    return x;
            }

            return int.MaxValue;
        }


        public long GetIncrease(int priorityNumber, decimal percentage)
        {
            if (priorityNumber >= Increases.Length)
                return 0;

            var result = (long)(Increases[priorityNumber] * percentage);

            return result;
        }

        public static long[] Increases =
        {
            63245986, 39088169, 24157817, 14930352, 9227465,
            5702887, 3524578, 2178309, 1346269, 832040,

            514229, 317811, 196418, 121393, 75025,
            46368, 28657, 17711, 10946, 6765
        };

    }
}
