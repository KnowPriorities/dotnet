﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline.Stages.Groups.Steps;
using KnowPriorities.v1.Models;
using KnowPriorities.v1.Results;

namespace KnowPriorities.v1.Engine.Pipeline.Stages
{
    public class GroupStage : Stage
    {

        public override IEnumerable<IPrioritizingHandler> AcquireSteps()
        {
            yield return new SetupGroupResults();
            yield return new CalculateGroupValues();
            yield return new AdjustGroupValuesRelativeToItemScope();
            yield return new OrderGroupValues();
        }

    }
}
