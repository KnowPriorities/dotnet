using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KnowPriorities.v1.Models
{
    public class WeightScale : ISimpleValidation
    {
        public const bool Default_HighValue = true;

        public WeightScale()
        {
            HighValue = Default_HighValue; // Default to high; aka 10 > 1
            Series = new List<string>();
        }

        public WeightScale(params string[] series)
        {
            Series = new List<string>(series);
        }

        public WeightScale(bool highValue)
        {
            HighValue = highValue;
            Series = new List<string>();
        }

        [JsonProperty("series")]
        public List<string> Series { get; private set; }

        [JsonProperty("high_value")]
        public bool HighValue { get; set; }

        [JsonProperty("depreciation")]
        public decimal? Depreciation { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }

        public void Validate(List<ValidationResult> results)
        {
            this.ValidateObject(results);
        }
    }
}
