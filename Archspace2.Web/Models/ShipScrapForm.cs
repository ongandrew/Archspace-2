using Newtonsoft.Json;
using System.Collections.Generic;

namespace Archspace2.Web
{
    public class ShipScrapForm
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        public class Item
        {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("amount")]
            public int Amount { get; set; }
        }
    }
}
