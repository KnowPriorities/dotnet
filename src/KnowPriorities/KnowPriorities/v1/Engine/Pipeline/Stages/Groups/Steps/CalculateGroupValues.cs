using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public class CalculateGroupValues : IPrioritizingHandler
    {

        public void Process(PrioritizingRequest request)
        {
            request.Result.Groups.ForEach(Process);
        }

        public void Process(GroupResult result)
        {
            result.Items.ForEach(Process);
        }

        public void Process(ItemResult item)
        {
            foreach (var stakeholder in item.Stakeholders)
            {
                var increase = GetIncrease(stakeholder, item.Id);

                item.Value += increase;
            }
        }

        public long GetIncrease(Stakeholder stakeholder, string itemId)
        {
            var priorityNumber = stakeholder.Priorities.IndexOf(itemId);
            var index = priorityNumber + (10 - stakeholder.Volume);

            return Increases[index];
        }

        public static long[] Increases =
        {
            63245986, 39088169, 24157817, 14930352, 9227465,
            5702887, 3524578, 2178309, 1346269, 832040,

            514229, 317811, 196418, 121393, 75025,
            46368, 28657, 17711, 10946, 6765
        };

        public static decimal GoldenRatio = 1.61803m;
        public static decimal GoldenRatioDeep = 1.618003398875m;

    }
}
