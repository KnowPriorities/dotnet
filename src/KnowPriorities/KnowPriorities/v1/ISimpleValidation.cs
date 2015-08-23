using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowPriorities.v1.Models
{
    public interface ISimpleValidation : IValidatableObject
    {

        void Validate(List<ValidationResult> results);
    }
}
