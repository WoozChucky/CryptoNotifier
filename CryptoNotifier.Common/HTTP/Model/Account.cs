using System;
using RestSharp.Deserializers;

namespace CryptoNotifier.Common.HTTP.Model
{
    public class Account : BaseResource
    {
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        [DeserializeAs(Name = "primary")]
        public bool Primary { get; set; }

        [DeserializeAs(Name = "type")]
        public string Type { get; set; }

        [DeserializeAs(Name = "currency")]
        public string Currency { get; set; }

        [DeserializeAs(Name = "balance")]
        public Balance Balance { get; set; }

        [DeserializeAs(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DeserializeAs(Name = "update_at")]
        public DateTime UpdateAt { get; set; }

        [DeserializeAs(Name = "ready")]
        public bool Ready { get; set; }
    }
}
