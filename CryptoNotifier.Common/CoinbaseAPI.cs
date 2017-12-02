using RestSharp;
using System;
using CryptoNotifier.Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CryptoNotifier.Common
{
    public sealed class CoinbaseAPI
    {
        private string _api_key;
        private string _api_secret;
        private string _language;
        
        private bool _is_ready;

        private readonly string _base_url;
        private readonly string _api_version;
        private readonly RestClient _client;

        #region Properties
        private string UnixTimeStamp
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

        private void SetupRestClient()
        {
            _client.AddDefaultHeader("CB-ACCESS-KEY", _api_key);
            _client.AddDefaultHeader("CB-VERSION", CoinbaseEndpoints.DEFAULT_API_DATE);
            _client.AddDefaultHeader("Content-Type", "application/json");
            _client.AddDefaultHeader("Accept-Language", _language);

            _is_ready = true;
        }

        public void Provide(string key, string secret, Language language = Language.pt_PT)
        {
            if(string.IsNullOrEmpty(key) || string.IsNullOrEmpty(secret))
                throw new ArgumentNullException("Key and Secret must not be null.");

            _api_key = key;
            _api_secret = secret;

            _language = language.ToString();

            SetupRestClient();
        }

        public User GetUser()
        {
            var request = new RestRequest
            {
                RootElement = "data",
                Method = Method.GET,
                Resource = CoinbaseEndpoints.User
            };

            return Execute<User>(request);
        }

        private T Execute<T>(RestRequest request) where T : new()
        {
            if (!_is_ready) throw new InvalidOperationException("CoinbaseAPI.Provide() was not called.");

            var ts = UnixTimeStamp;
            // string concatenation of timestamp + method + path + body
            var preSignature = ts + request.Method.ToString() + "/" +_api_version + request.Resource;

            request.AddHeader("CB-ACCESS-TIMESTAMP", ts);
            request.AddHeader("CB-ACCESS-SIGN", CryptoUtils.GetSignature(_api_secret, preSignature));

            var response = _client.Execute<T>(request);

            if(response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                if(response.ErrorException != null)
                {
                    throw response.ErrorException;
                }
                else
                {
                    throw new Exception($"Code: {response.StatusCode.ToString()}. Content: {response.Content}");
                }
            }
        }


        public void SendRequests<T>()
        {

        }
    }
}
