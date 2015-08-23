using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.v1.Engine.Pipeline.Stages.Preparation.Steps
{
    public class AssumeGroupPercentageRebalanceIfAllGroupsAre100Percent : IPrioritizingHandler
    {
        public void Process(PrioritizingRequest request)
        {
            var groups = request.Subject.Groups;

            var totalPercentage = groups.Sum(group => group.Percentage);

            if (totalPercentage == 1m || groups.Count != totalPercentage)
                return;

            var avgPercentage = 1m / groups.Count;

            // If a remainder is created, it should be negligible enough to assume assignment
            var remainder = 1m - (avgPercentage * groups.Count);

            groups.ForEach(group=> group.Percentage = avgPercentage);

            groups[0].Percentage += remainder;
        }
    }
}
