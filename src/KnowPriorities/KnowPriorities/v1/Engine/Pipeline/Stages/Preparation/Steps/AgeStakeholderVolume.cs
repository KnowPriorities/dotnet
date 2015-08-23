using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline
{
    public class AgeStakeholderVolume : IPrioritizingHandler
    {

        public void Process(PrioritizingRequest request)
        {
            var stakeholders = request.Subject.Stakeholders;

            foreach (var stakeholder in stakeholders)
            {
                Process(stakeholder, request);
            }
        }

        public void Process(Stakeholder stakeholder, PrioritizingRequest request)
        {
            var offset = GetVolumeOffset(stakeholder, request);

            if (offset < 1) return;

            stakeholder.Volume = stakeholder.Volume - offset;

            if (stakeholder.Volume < 0)
                stakeholder.Volume = 0;
        }

        public int GetVolumeOffset(Stakeholder stakeholder, PrioritizingRequest request)
        {
            var offset = (int)(request.Subject.AsOf.Subtract(stakeholder.UpdatedAt).TotalDays / request.Subject.DaysToAge);

            return offset;
        }


    }
}
