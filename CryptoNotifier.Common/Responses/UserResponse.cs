using CryptoNotifier.Common.Model;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoNotifier.Common.Responses
{
    public class UserResponse
    {
        [DeserializeAs(Name = "data")]
        public User Data { get; set; }
    }
}
