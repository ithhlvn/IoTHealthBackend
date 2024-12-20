using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace IoTHealthBackend.SASS
{
    

    public class HttpAuthToken
    {
        public string Token;
        public HttpAuthToken(string bearerToken)
        {
            Token = bearerToken;
        }
    }

    
    public class ServiceController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private bool _isPassCheckSSL;

        public ServiceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //private readonly HttpClient _httpClient;

        //public ServiceController(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        private HttpClientHandler GetPassSslHttpClientHandler()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            return clientHandler;
        }

        private void SetBearerToken(HttpClient httpClient, string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = null;
            }
            else
            {
                if (httpClient.DefaultRequestHeaders.Contains("Authorization")) httpClient.DefaultRequestHeaders.Remove("Authorization");
                if (string.IsNullOrEmpty(token) == false) httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
        }
    }
}
