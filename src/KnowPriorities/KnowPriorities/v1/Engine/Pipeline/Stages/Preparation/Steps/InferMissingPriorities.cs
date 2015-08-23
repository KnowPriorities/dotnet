using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Inferred;
using KnowPriorities.v1.Models;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public class InferMissingPriorities : IPrioritizingHandler
    {
        public void Process(PrioritizingRequest request)
        {
            var subject = request.Subject;

            var stakeholders =
                subject.Stakeholders.Where(s => s.Priorities.Count < 10 && s.Behaviors.Count > 0);

            foreach (var stakeholder in stakeholders)
            {
                InferPriorities(stakeholder, subject);
            }

        }


        // Note: a system of relationship/friend "could" be used to infer someones priorities...BUT never ever should.
        //      if a person never participates, but 3 like-minded friends do...a person shouldn't simply be a parrot.
        //      if a person never participates, they should always be considered mute.

        // Note: Dislikes are not used because that is a system for the user to filter out what they don't want to spend time on prioritizing.
        // In a system of only having "likes", everything is subjective.


        public void InferPriorities(Stakeholder stakeholder, Subject subject)
        {
            var priorities = stakeholder.Priorities;
            var indirectPriorities = stakeholder.Behaviors;

            if (indirectPriorities.Count < 1)
                return;

            indirectPriorities.Sort(subject.BehavioralComparer);

            var results = indirectPriorities.Select(i => i.Id);

            AppendPriorities(priorities, results);
        }

        public void AppendPriorities(List<string> priorities, IEnumerable<string> itemIds)
        {
            foreach (var itemId in itemIds)
            {
                if (priorities.Count == 10)
                    return;

                if (priorities.Contains(itemId))
                    continue;

                priorities.Add(itemId);
            }
        }
    
    }
}
