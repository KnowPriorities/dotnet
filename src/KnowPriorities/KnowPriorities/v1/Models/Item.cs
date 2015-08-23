using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace KnowPriorities.v1.Models
{
    public class Item : ISimpleValidation
    {
        public Item()
        {
            TagIds = new List<string>();
        }


        [Required]
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Can be a number, or even expressed as a calculation of Time X Cost. Don't assume scope.  Item is defined, they should know.
        /// </summary>
        [Required]
        [Range(1, 10)]//long.MaxValue)]
        [JsonProperty("scope")]
        public long Scope { get; set; }
        
        [JsonProperty("tags")]
        public List<string> TagIds { get; private set; }

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
