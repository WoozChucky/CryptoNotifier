using RestSharp;
using System;
using CryptoNotifier.Common.Model;

namespace CryptoNotifier.Common
{
    public sealed class CoinbaseAPI
    {
        public const string DEFAULT_BASE_URL = "https://api.coinbase.com/v2/";

        private string _api_key;
        private string _api_secret;
        private string _language;
        private readonly string _base_url;
        private readonly RestClient _client;
        private bool _is_ready;

        public CoinbaseAPI(string api_url = DEFAULT_BASE_URL)
        {
            _base_url = api_url;
            _client = new RestClient(_base_url);
            _is_ready = false;
        }

        private void SetupRestClient()
        {
            _client.AddDefaultHeader("CB-ACCESS-KEY", _api_key);

        }

        public void Provide(string key, string secret, Language language = Language.pt_PT)
        {
            if(string.IsNullOrEmpty(key) || string.IsNullOrEmpty(secret))
                throw new ArgumentNullException("Key and Secret must not be null.");

            _api_key = key;
            _api_secret = secret;

            _language = language.ToString();

            _is_ready = true;
        }
    }
}
