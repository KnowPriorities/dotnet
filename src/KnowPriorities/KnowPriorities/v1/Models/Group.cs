using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KnowPriorities.v1.Models
{
    public class Group : ISimpleValidation
    {

        public Group()
        {
            Percentage = 1m;
            Stakeholders = new List<Stakeholder>();
            Tags = new List<Tag>();
        }

        public const int Default_Percentage_DecimalPlaces = 4;
        public const double Default_Percentage_Minimum = 0.0001;


        [Required]
        [JsonProperty("id")]
        public string Id { get; set; }

        [Range(Default_Percentage_Minimum, 1)]
        [JsonProperty("percentage")]
        public decimal Percentage { get; set; }

        [JsonProperty("stakeholders")]
        public List<Stakeholder> Stakeholders { get; private set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; private set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // fyi, having 0 stakeholders is okay.  It allows group % to exist even before stakeholders are grouped.

            yield break;
        }

        public void Validate(List<ValidationResult> results)
        {
            this.ValidateObject(results);

            Tags.ForEach(tag => tag.Validate(results));
            Stakeholders.ForEach(stakeholder => stakeholder.Validate(results));
        }
    }
}
