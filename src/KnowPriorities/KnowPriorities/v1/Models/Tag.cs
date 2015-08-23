using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KnowPriorities.v1.Models
{
    public class Tag : ISimpleValidation
    {
        [Required]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("multiply")]
        public bool Multiply { get; set; }

        [Range(0, 10)]
        [JsonProperty("absolute_volume")]
        public int? AbsoluteVolume { get; set; }

        [Range(-10, 10)]
        [JsonProperty("adjust_volume")]
        public int? AdjustVolume { get; set; }

        //[Range(1, long.MaxValue)]
        [Range(1, 10)]
        [JsonProperty("absolute_scope")]
        public long? AbsoluteScope { get; set; }

        [Range(-10, 10)]
        [JsonProperty("adjust_scope")]
        public long? AdjustScope { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!AbsoluteVolume.HasValue && !AdjustVolume.HasValue && !AbsoluteScope.HasValue && !AdjustScope.HasValue)
                yield return new ValidationResult("Tags need to have at least one of these set: absolute_volume, adjust_volume, absolute_scope, or adjust_scope.");

            if (AdjustVolume.HasValue && AdjustVolume == 0)
                yield return new ValidationResult("Tags that adjust volume need a value 1-10 (0 provided).");

            if (AdjustScope.HasValue && AdjustScope == 0)
                yield return new ValidationResult("Tags that adjust volume need a value 1-10 (0 provided).");

            if (AbsoluteVolume.HasValue && AdjustVolume.HasValue)
                yield return new ValidationResult("Both adjust volume and absolute volume can not be set for the same tag.");

            if (AbsoluteScope.HasValue && AdjustScope.HasValue)
                yield return new ValidationResult("Both adjust scope and absolute scope can not be set for the same tag.");
        }

        public void Validate(List<ValidationResult> results)
        {
            this.ValidateObject(results);
        }


    }
}
