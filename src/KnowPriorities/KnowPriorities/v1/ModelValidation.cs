using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.v1
{
    public static class ModelValidation
    {

        public static void ValidateObject<T>(this T item, List<ValidationResult> results) where T : IValidatableObject
        {
            var context = new ValidationContext(item, serviceProvider: null, items: null);

            Validator.TryValidateObject(item, context, results, validateAllProperties: true);
        }

    }
}
