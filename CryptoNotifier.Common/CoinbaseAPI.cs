using RestSharp;
using System;

namespace CryptoNotifier.Common
{
    public class CoinbaseAPI
    {
        public const string DEFAULT_BASE_URL = "https://api.coinbase.com/v2/";

        private string _api_key;
        private string _api_secret;
        private readonly string _base_url;
        private readonly RestClient _client;

        public CoinbaseAPI(string api_url = DEFAULT_BASE_URL)
        {

        }

        public void Provide(string key, string secret)
        {
            _api_key = key;
            _api_secret = secret;
        }
    }
}
