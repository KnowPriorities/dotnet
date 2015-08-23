using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.ModelValidations
{
    public class ValidationTest<T> where T : class, ISimpleValidation, new()
    {

        protected readonly T context = new T();

        protected void ValidateFail()
        {
            var results = new List<ValidationResult>();

            context.Validate(results);

            Assert.Equal(1, results.Count);
        }

        protected void ValidateNoFail()
        {
            var results = new List<ValidationResult>();

            context.Validate(results);

            Assert.Equal(0, results.Count);
        }


    }
}
