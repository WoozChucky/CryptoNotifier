using RestSharp;
using System;
using CryptoNotifier.Common.HTTP.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoNotifier.Common.Exceptions;

namespace CryptoNotifier.Common.HTTP
{
    public sealed class CoinbaseAPI
    {
        string _api_key;
        string _api_secret;
        string _language;

        bool _is_ready;

        readonly string _base_url;
        readonly string _api_version;
        readonly RestClient _client;

        #region Properties
        string UnixTimeStamp
        {
            get
            {
                return DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            }
        }
        #endregion

        public CoinbaseAPI(string api_url = CoinbaseEndpoints.DEFAULT_BASE_URL,
            string api_version = CoinbaseEndpoints.DEFAULT_API_VERSION)
        {
            _base_url = api_url;
            _api_version = api_version;
            _client = new RestClient(_base_url + api_version);
            _is_ready = false;
        }

        public void Provide(string key, string secret, Language language = Language.pt_PT)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(secret))
                throw new ArgumentNullException("key and secret must not be null.");

            _api_key = key;
            _api_secret = secret;

            _language = language.ToString();

            //NOTE: Maybe these headers should be moved to <T>ExecuteAsync
            _client.AddDefaultHeader("CB-VERSION", CoinbaseEndpoints.DEFAULT_API_DATE);
            _client.AddDefaultHeader("Content-Type", "application/json");
            _client.AddDefaultHeader("Accept-Language", _language);

            _is_ready = true;
        }

        #region AsyncRequests

        public async Task<User> GetUserAsync()
        {
            var request = new RestRequest
            {
                RootElement = "data",
                Method = Method.GET,
                Resource = CoinbaseEndpoints.User
            };

            return await ExecuteAsync<User>(request);
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            var request = new RestRequest
            {
                RootElement = "data",
                Method = Method.GET,
                Resource = CoinbaseEndpoints.Accounts
            };

            return await ExecuteAsync<List<Account>>(request);
        }

        public async Task<ExchangeRate> GetExchangeRateAsync(string currency)
        {
            var request = new RestRequest
            {
                RootElement = "data",
                Method = Method.GET,
                Resource = CoinbaseEndpoints.ExchangeRates,
            };

            request.AddParameter("currency", currency);

            return await ExecuteAsync<ExchangeRate>(request);
        }

        public async Task<Balance> GetCurrentMarketPriceAsync(string originCurrency, string destinationCurrency)
        {
            var request = new RestRequest
            {
                RootElement = "data",
                Method = Method.GET,
                Resource = CoinbaseEndpoints.SpotPrice
            };

            request.AddUrlSegment("origin", originCurrency);
            request.AddUrlSegment("destination", destinationCurrency);

            return await ExecuteAsync<Balance>(request);
        }

        #endregion

        async Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            if (!_is_ready) throw new InvalidOperationException("CoinbaseAPI.Provide() was not called.");

            var ts = UnixTimeStamp;
            // string concatenation of timestamp + method + path + body
            var preSignature = ts + request.Method.ToString() + "/" + _api_version + request.Resource;

            request.AddHeader("CB-ACCESS-KEY", _api_key);
            request.AddHeader("CB-ACCESS-TIMESTAMP", ts);
            request.AddHeader("CB-ACCESS-SIGN", CryptoUtils.GetSignature(_api_secret, preSignature));

            var response = await _client.ExecuteTaskAsync<T>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new CoinbaseTokenException(response.Content);

            throw new Exception($"Code: {response.StatusCode.ToString()}. Content: {response.Content}");
        }
    }
}
