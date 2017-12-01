using RestSharp;
using System;
using CryptoNotifier.Common.Model;
using System.Net.Security;
using CryptoNotifier.Common.Responses;

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
            /* TODO: This needs to be verified
            _client.RemoteCertificateValidationCallback += 
                new RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) =>
            {
                
                return true;
            }); */
            _client.AddDefaultHeader("CB-ACCESS-KEY", _api_key);
            _client.AddDefaultHeader("CB-VERSION", "2017-11-30");
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

        public void SendRequest()
        {
            var request = new RestRequest("user", Method.GET);

            var unixTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

            // string concatenation of timestamp + method + path + body
            var message = unixTimeStamp + request.Method.ToString() + "/v2/user" + "";

            var signature = CryptoUtils.GetSignature(_api_secret, message);

            request.AddHeader("CB-ACCESS-TIMESTAMP", unixTimeStamp);
            request.AddHeader("CB-ACCESS-SIGN", signature);

            var response = _client.Execute<UserResponse>(request);
            
            if(response.IsSuccessful)
            {
                var user = response.Data;
                if(user != null)
                {

                }
            }
        }



        public void SendRequests<T>()
        {

        }
    }
}
