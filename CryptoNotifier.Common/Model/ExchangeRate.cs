using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

namespace CryptoNotifier.Common.Model
{
    public class ExchangeRate
    {
        [DeserializeAs(Name = "currency")]
        public string Currency { get; set; }

        [DeserializeAs(Name = "rates")]
        public Dictionary<string, string> Rates { get; set; }
    }
}
