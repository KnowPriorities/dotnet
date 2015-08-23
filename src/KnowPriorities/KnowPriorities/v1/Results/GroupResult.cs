using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KnowPriorities.v1.Models;

namespace KnowPriorities.v1.Results
{
    public class GroupResult
    {
        public GroupResult()
        {
            Items = new List<ItemResult>();
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("items")]
        public List<ItemResult> Items { get; set; }

        [JsonProperty("percentage")]
        public decimal Percentage { get; set; }

        [JsonIgnore]
        public Group Group { get; set; }
    }
}
