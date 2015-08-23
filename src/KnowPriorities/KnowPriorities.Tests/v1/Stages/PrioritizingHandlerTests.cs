using System;
using System.Collections.Generic;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Engine.Pipeline.Stages.Groups.Steps;
using KnowPriorities.v1.Models;

namespace KnowPriorities.Tests.v1.Stages
{
    public class PrioritizingHandlerTests
    {
        protected readonly AgeStakeholderVolume _step = new AgeStakeholderVolume();
        protected readonly KnowPriorities.v1.Models.Subject _subject;
        protected readonly Group _group1;
        protected readonly PrioritizingRequest _request;

        protected readonly Stakeholder _stakeholder1;
        protected readonly Stakeholder _stakeholder2;

        private const string DefaultGroupName = "default";

        public PrioritizingHandlerTests()
        {

            _subject = new KnowPriorities.v1.Models.Subject() { DaysToAge = 30 };

            _group1 = new Group() { Id = DefaultGroupName, Percentage = 1 };

            _subject.Groups.Add(_group1);

            _stakeholder1 = new Stakeholder();

            _stakeholder1.UpdatedAt = _subject.AsOf.AddDays(-1);
            _stakeholder1.Priorities.AddRange(new List<string>() { "a", "b", "c" });

            _stakeholder2 = new Stakeholder();

            _stakeholder2.UpdatedAt = _subject.AsOf.AddDays(-90);
            _stakeholder2.Priorities.AddRange(new List<string>() {"c", "a", "b"});



            _subject.Groups[0].Stakeholders.Add(_stakeholder1);
            _subject.Groups[0].Stakeholders.Add(_stakeholder2);
            
            _request = new PrioritizingRequest(_subject);
        }

    }
}
