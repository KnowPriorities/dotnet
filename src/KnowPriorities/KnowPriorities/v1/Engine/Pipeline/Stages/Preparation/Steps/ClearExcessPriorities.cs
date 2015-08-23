using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public class ClearExcessPriorities : IPrioritizingHandler
    {

        public void Process(PrioritizingRequest request)
        {
            foreach(var stakeholder in request.Subject.Stakeholders)
                Cleanse(stakeholder);
        }


        public void Cleanse(Stakeholder stakeholder)
        {
            // Remove extra priorities
            var itemIds = stakeholder.Priorities;

            if (itemIds.Count > 10)
                itemIds.RemoveRange(10, itemIds.Count - 10);
        }
    }
}
