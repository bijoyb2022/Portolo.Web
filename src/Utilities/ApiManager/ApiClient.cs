using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Portolo.Utility.ApiManager
{
    public class ApiClient<T, TR>
    {
        private static readonly HttpClientHandler Handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        private static readonly HttpClient HttpClient = new HttpClient(Handler) { Timeout = TimeSpan.FromMinutes(5) };
        private static readonly JsonSerializer Serializer = new JsonSerializer();
        private static readonly Lazy<ApiClient<T, TR>> Instance = new Lazy<ApiClient<T, TR>>(() => new ApiClient<T, TR>());
        private ApiClient()
        {
        }

        public static ApiClient<T, TR> GetInstance => Instance.Value;

        public static async Task<TR> MakeHttpRequestAsync(T requestContext, string apiUrl)
        {
            TR responseContext;
            var jsonRequest = JsonConvert.SerializeObject(requestContext);
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl);
            httpRequestMessage.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            httpRequestMessage.Headers.Accept.Clear();
            httpRequestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await HttpClient.SendAsync(httpRequestMessage).ConfigureAwait(true);
            response.EnsureSuccessStatusCode();
            using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                responseContext = Serializer.Deserialize<TR>(json);
            }

            return responseContext;
        }

        public static async Task<TR> MakeHttpRequestForJsonAsync(T requestContext, string apiUrl)
        {
            TR responseContext;
            var jsonRequest = JsonConvert.SerializeObject(requestContext);
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl);
            httpRequestMessage.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            httpRequestMessage.Headers.Accept.Clear();
            httpRequestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await HttpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                responseContext = JsonConvert.DeserializeObject<TR>(result);
            }
            else
            {
                responseContext = JsonConvert.DeserializeObject<TR>(null);
            }

            return responseContext;
        }

        public static async Task<TR> MakeHttpRequestWithHeaderForJsonAsync(string authorization, T requestContext, string apiUrl)
        {
            TR responseContext;

            var jsonRequest = JsonConvert.SerializeObject(requestContext);
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl);
            httpRequestMessage.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            httpRequestMessage.Headers.Accept.Clear();
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            httpRequestMessage.Headers.Add("Authorization", authorization);
            var response = await HttpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                responseContext = JsonConvert.DeserializeObject<TR>(result);
            }
            else
            {
                responseContext = JsonConvert.DeserializeObject<TR>(null);
            }

            return responseContext;
        }
    }
}
