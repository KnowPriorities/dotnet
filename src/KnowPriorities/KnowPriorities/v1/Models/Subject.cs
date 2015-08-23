using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KnowPriorities.v1.Engine.Inferred;
using KnowPriorities.v1.Engine.Pipeline.Stages.QualityControl.Steps;

namespace KnowPriorities.v1.Models
{
    public class Subject : ISimpleValidation
    {
        public const int Default_DaysToAge = 30;

        public static long[] Default_SystemOfValues =
        {
            63245986, 39088169, 24157817, 14930352, 9227465,
            5702887, 3524578, 2178309, 1346269, 832040,

            514229, 317811, 196418, 121393, 75025,
            46368, 28657, 17711, 10946, 6765
        };


        public Subject()
        {
            Tags = new List<Tag>();
            Items = new List<Item>();
            Groups = new List<Group>();
            Scale = new WeightScale();

            AsOf = DateTime.UtcNow.Date; // .Date used to prevent ms differences in object details
            DaysToAge = Default_DaysToAge;

            //SystemOfValues = Default_SystemOfValues;
        }

        [JsonProperty("asof")]
        public DateTime AsOf { get; set; }


        //[JsonProperty("system_of_values")]
        //public long[] SystemOfValues { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; private set; }
            
        [JsonProperty("items")]
        public List<Item> Items { get; private set; }

        [JsonProperty("groups")]
        public List<Group> Groups { get; private set; }

        [JsonProperty("scale")]
        public WeightScale Scale { get; private set; }

        [Range(1, 90)]
        [JsonProperty("days_to_age")]
        public int DaysToAge { get; set; }

        [JsonIgnore]
        public long DefaultScope { get; set; }

        [JsonIgnore]
        public bool HasMultipleGroups
        {
            get { return Groups.Count > 1; }
        }

        [JsonIgnore]
        public bool HasOnlyOneGroup
        {
            get { return Groups.Count == 1; }
        }

        private ReadOnlyCollection<Stakeholder> _Stakeholders;

        [JsonIgnore]
        public ReadOnlyCollection<Stakeholder> Stakeholders
        {
            get
            {
                if (_Stakeholders != null)
                    return _Stakeholders;

                var stakeholders = new List<Stakeholder>();

                Groups.ForEach(g=> stakeholders.AddRange(g.Stakeholders));

                _Stakeholders = stakeholders.AsReadOnly();

                return _Stakeholders;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Rule: AsOf can't be in the future...won't make any sense
            if (AsOf > DateTime.UtcNow)
                yield return new ValidationResult("AsOf cannot be in the future (validated against UTC)");
            
            if (Groups.Count == 0)
            {// Rule: Groups are required
                yield return new ValidationResult("No groups of stakeholders were provided");
            }
            else if (Groups.Sum(q => q.Percentage) != 1)
            {
                // Rule: Group percentages must equal 100%
                yield return
                    new ValidationResult(
                        "Group percentages must total to 1 (aka 100%).");
            }

            // Rule: Stakeholders required.  Checked at this level because individual groups can be empty.
            if (Groups.Count > 0)
            {
                var stakeholderCount = 0;

                Groups.ForEach(group => stakeholderCount += group.Stakeholders.Count);

                if (stakeholderCount == 0)
                    yield return new ValidationResult("Need at least 1 stakeholder to prioritize something");
            }
        }

        public void Validate(List<ValidationResult> results)
        {
            this.ValidateObject(results);

            Scale.Validate(results);

            Tags.ForEach(tag => tag.Validate(results));
            Items.ForEach(item => item.Validate(results));
            Groups.ForEach(group => group.Validate(results));
        }

        private BehavioralComparer behavioralComparer;
        public BehavioralComparer BehavioralComparer {
            get { return behavioralComparer ?? (behavioralComparer = BehavioralComparer.GetComparer(Scale)); }
        }
    }
}
