using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using KnowPriorities.v1.Models;

namespace KnowPriorities.v1.Engine.Pipeline.Stages.QualityControl.Steps
{
    public class CheckObjectValidations : IPrioritizingHandler
    {
        
        // ref: http://odetocode.com/blogs/scott/archive/2011/06/29/manual-validation-with-data-annotations.aspx

        public void Process(PrioritizingRequest request)
        {

            var results = new List<ValidationResult>();

            request.Subject.Validate(results);

            if (results.Count == 0)
                return;

            request.HaltProcessing = true;
            request.Result.Errors.AddRange(results.Select(i => i.ErrorMessage));
        }

    }
}
