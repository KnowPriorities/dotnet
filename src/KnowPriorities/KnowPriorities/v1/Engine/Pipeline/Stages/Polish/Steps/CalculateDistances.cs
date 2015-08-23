using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public class CalculateDistances : IPrioritizingHandler

    {
        public void Process(PrioritizingRequest request)
        {
            SetDistances(request.Result.Items);

            request.Result.Groups.ForEach(g => SetDistances(g.Items));
        }

        public void SetDistances(List<ItemResult> items)
        {

            for (var x = 0; x < items.Count; x++)
            {
                var current = items[x];
                var nextItemDown = (x + 1 == items.Count) ? null : items[x + 1];

                SetDistance(current, nextItemDown);
            }
        }

        public void SetDistance(ItemResult current, ItemResult nextItemDown)
        {
            if (nextItemDown == null || nextItemDown.Value == 0)
            {
                current.Distance = 0;
                return;
            }

            current.Distance = ((decimal) current.Value / nextItemDown.Value) - 1;

            current.Distance = Math.Round(current.Distance, 2);
        }

    }
}
