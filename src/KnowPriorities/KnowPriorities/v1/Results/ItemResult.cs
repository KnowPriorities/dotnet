using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KnowPriorities.v1.Models;

namespace KnowPriorities.v1.Results
{
    public class ItemResult : IComparable<ItemResult>
    {
        public ItemResult()
        {
            Stakeholders = new List<Stakeholder>();
            Groups = new List<GroupResult>();
        }


        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// % difference from the priority item lower than it (ex 1.distance = 1.value / 2.value )
        /// </summary>
        [JsonProperty("distance")]
        public decimal Distance { get; set; }
        
        ///// <summary>
        ///// # of stakeholders that were included in coming up with this result
        ///// </summary>
        //[JsonProperty("stakeholder_count")]
        //public long StakeholderCount { get; set; }

        [JsonIgnore] 
        public long Value;// { get; set; }

        //[JsonIgnore]
        //public long AdjustedValue { get; set; }


        #region Processing Values

        [JsonIgnore]
        public Group Group { get; set; }
        
        [JsonIgnore]
        public Item Item { get; set; }

        [JsonIgnore]
        public List<Stakeholder> Stakeholders { get; set; }

        [JsonIgnore]
        public List<GroupResult> Groups { get; set; }


        #endregion



        public int CompareTo(ItemResult other)
        {
            return other.Value.CompareTo(this.Value);
        }
    }
}
