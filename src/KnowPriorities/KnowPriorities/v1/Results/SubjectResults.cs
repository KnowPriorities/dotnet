using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KnowPriorities.v1.Results
{
    public class SubjectResults
    {
        public SubjectResults()
        {
            Items = new List<ItemResult>();
            Groups = new List<GroupResult>();
            Errors = new List<string>();
        }

        [JsonProperty("items")]
        public List<ItemResult> Items { get; set; }

        [JsonProperty("groups")]
        public List<GroupResult> Groups { get; set; }

        [JsonIgnore]
        public List<string> Errors { get; set; }


    }
}
