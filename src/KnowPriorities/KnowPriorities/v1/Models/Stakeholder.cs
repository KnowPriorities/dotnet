using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KnowPriorities.v1.Models
{
    public class Stakeholder : ISimpleValidation
    {
        /// <summary>
        /// Default of 3 is so it can be reduced if needed
        /// </summary>
        public const int Default_Volume = 3;

        public Stakeholder()
        {
            Volume = Default_Volume;
            Priorities = new List<string>();
            Behaviors = new List<Behavior>();
            TagIds = new List<string>();
            UpdatedAt = DateTime.UtcNow.Date; // .Date used to prevent ms differences in object details
        }


        // *** Note: No Stakeholder.Id is allowed to prevent user tracing

        [Range(0, 10)]
        [JsonProperty("volume")]
        public int Volume { get; set; }

        [JsonProperty("priorities")]
        public List<string> Priorities { get; private set; }

        [JsonProperty("behaviors")]
        public List<Behavior> Behaviors { get; private set; }


        //[JsonProperty("weights")]
        //public List<Weight> Weights { get; private set; }

        //[JsonProperty("interactions")]
        //public List<Interaction> Interactions { get; private set; }

        [JsonProperty("tags")]
        public List<string> TagIds { get; private set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // it's okay if there are silent stakeholders. They will be removed automatically.

            yield break;
        }

        public void Validate(List<ValidationResult> results)
        {
            this.ValidateObject(results);

            Behaviors.ForEach(priority=> priority.Validate(results));
        }
    }
}
