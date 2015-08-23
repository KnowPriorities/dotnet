using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace KnowPriorities.v1.Models
{
    public class Behavior : ISimpleValidation
    {
        public Behavior()
        {
            Heat = 1;
            UpdatedAt = DateTime.UtcNow.Date; // .Date used to prevent ms differences in object details
        }


        [Required]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("series")]
        public string SeriesValue { get; set; }

        [JsonProperty("range")]
        public decimal? RangeValue { get; set; }

        [Range(1, int.MaxValue)]
        [JsonProperty("heat")]
        public int Heat { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }


        public int GetHeatValue()
        {
            return GetHeatValue(Heat, UpdatedAt);
        }

        public static int GetHeatValue(int heat, DateTime updatedAt)
        {
            const int scale = 10;

            decimal valuePerMonth = (decimal)heat / scale;
            int monthsInactive = (int)(DateTime.Today.Subtract(updatedAt.Date).TotalDays / 30);

            int value = monthsInactive > scale ? 0 : (int)(valuePerMonth * (scale - monthsInactive));

            return value;
        }

        public void Validate(List<ValidationResult> results)
        {
            this.ValidateObject(results);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
