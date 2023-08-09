using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace FinancialApplication.Utility.Utilities
{
    public class HttpClientUtility : IHttpClientUtility
    {
        ILogger logger;

        [ActivatorUtilitiesConstructor]
        public HttpClientUtility(ILogger<HttpClientUtility> logger)
        {
            this.logger = logger;
        }

        public HttpClientUtility(ILogger logger)
        {
            this.logger = logger;
        }

        #region GET
        public async Task<T> GetAsync<T>(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string response = await GetAsync(requestUri, contentPost, authorization, mediaTypeNames, timeout, headers);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<T> GetAsync<T>(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string response = await GetAsync(requestUri, contentPost, authorization, mediaTypeNames, timeout, headers);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<string> GetAsync(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string contentPostString = JsonConvert.SerializeObject(contentPost);
            return await GetAsync(requestUri, contentPostString, authorization, mediaTypeNames, timeout, headers);
        }

        public async Task<string> GetAsync(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            HttpRequestMessage requestMessage = createMessageRequest(HttpMethod.Get, requestUri, contentPost, authorization, mediaTypeNames, headers);
            return await SendAsync(requestMessage, timeout);
        }
        #endregion GET

        #region POST
        public async Task<T> PostAsync<T>(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string response = await PostAsync(requestUri, contentPost, authorization, mediaTypeNames, timeout, headers);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<T> PostAsync<T>(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string response = await PostAsync(requestUri, contentPost, authorization, mediaTypeNames, timeout, headers);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<string> PostAsync(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string contentPostString = JsonConvert.SerializeObject(contentPost);
            return await PostAsync(requestUri, contentPostString, authorization, mediaTypeNames, timeout, headers);
        }

        public async Task<string> PostAsync(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            HttpRequestMessage requestMessage = createMessageRequest(HttpMethod.Post, requestUri, contentPost, authorization, mediaTypeNames, headers);
            return await SendAsync(requestMessage, timeout);
        }
        #endregion POST

        #region PUT
        public async Task<T> PutAsync<T>(string requestUri, object contentPost, string authorization, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string response = await PostAsync(requestUri, contentPost, authorization, mediaTypeNames, timeout, headers);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<T> PutAsync<T>(string requestUri, string contentPost, string authorization, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string response = await PutAsync(requestUri, contentPost, authorization, mediaTypeNames, timeout, headers);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<string> PutAsync(string requestUri, object contentPost, string authorization, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string contentPostString = JsonConvert.SerializeObject(contentPost);
            return await PutAsync(requestUri, contentPostString, authorization, mediaTypeNames, timeout, headers);
        }

        public async Task<string> PutAsync(string requestUri, string contentPost, string authorization, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            HttpRequestMessage requestMessage = createMessageRequest(HttpMethod.Put, requestUri, contentPost, authorization, mediaTypeNames, headers);
            return await SendAsync(requestMessage, timeout);
        }

        #endregion PUT

        #region DELETE
        public async Task<T> DeleteAsync<T>(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string response = await DeleteAsync(requestUri, contentPost, authorization, mediaTypeNames, timeout, headers);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<T> DeleteAsync<T>(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string response = await DeleteAsync(requestUri, contentPost, authorization, mediaTypeNames, timeout, headers);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<string> DeleteAsync(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string contentPostString = JsonConvert.SerializeObject(contentPost);
            return await DeleteAsync(requestUri, contentPostString, authorization, mediaTypeNames, timeout, headers);
        }

        public async Task<string> DeleteAsync(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            HttpRequestMessage requestMessage = createMessageRequest(HttpMethod.Delete, requestUri, contentPost, authorization, mediaTypeNames, headers);
            return await SendAsync(requestMessage, timeout);
        }
        #endregion DELETE

        #region PATH
        public async Task<T> PatchAsync<T>(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string response = await PatchAsync(requestUri, contentPost, authorization, mediaTypeNames, timeout, headers);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<T> PatchAsync<T>(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string response = await PatchAsync(requestUri, contentPost, authorization, mediaTypeNames, timeout, headers);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<string> PatchAsync(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            string contentPostString = JsonConvert.SerializeObject(contentPost);
            return await PatchAsync(requestUri, contentPostString, authorization, mediaTypeNames, timeout, headers);
        }

        public async Task<string> PatchAsync(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null)
        {
            HttpRequestMessage requestMessage = createMessageRequest(HttpMethod.Patch, requestUri, contentPost, authorization, mediaTypeNames, headers);
            return await SendAsync(requestMessage, timeout);
        }

        #endregion PATH

        public async Task<string> SendAsync(HttpRequestMessage httpRequestMessage, int? timeout = null)
        {
            if (timeout.HasValue && timeout.Value < 1)
                timeout = null;

            HttpClient client = new HttpClient();

            if (timeout.HasValue)
                client.Timeout = TimeSpan.FromMilliseconds(timeout.Value);
            logger?.LogInformation("REQUEST INFO --> URL CHAMADA: {url} " +
                "\n HEADER --> {header}" +
                "\n BODY --> {body}",
                httpRequestMessage.RequestUri.ToString(),
                httpRequestMessage.Headers.ToString(),
                httpRequestMessage?.Content?.ReadAsStringAsync().Result);

            HttpResponseMessage httpResponse = await client.SendAsync(httpRequestMessage);
            string response = await validateResponse(httpResponse);

            logger?.LogInformation("RESPONSE INFO --> URL CHAMADA: {url} " +
                "\n HEADER --> {header}" +
                "\n BODY --> {body}",
                 httpRequestMessage.RequestUri.ToString(),
                 httpResponse.Headers.ToString(),
                 response);

            return response;

        }

        public async Task<string> validateResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new Exception("HttpResponseMessage is null");
            }
            string responseContent = await response.Content?.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exceptions.UnauthorizedException(message: responseContent, statusCode: response.StatusCode);
                    default:
                        throw new Exceptions.HttpRequestException(responseContent, response.StatusCode);
                }
            }
            return responseContent;
        }

        public HttpRequestMessage createMessageRequest(HttpMethod httpMethod,
            string url,
            string contentPost = null,
            string authorization = null,
            string mediaTypeNames = "application/json",
            Dictionary<string, string> headers = null)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Method = httpMethod;

            Uri uri = new Uri(url);
            requestMessage.RequestUri = uri;
            requestMessage.Headers.Host = uri.Host;

            if (!string.IsNullOrEmpty(authorization))
                requestMessage.Headers.Authorization = AuthenticationHeaderValue.Parse(authorization);

            if (headers != null && headers.Any())
            {
                foreach (KeyValuePair<string, string> valuePair in headers)
                {
                    requestMessage.Headers.Add(valuePair.Key, valuePair.Value);
                }
            }

            if (!String.IsNullOrEmpty(contentPost))
                requestMessage.Content = new StringContent(contentPost, Encoding.UTF8, mediaTypeNames);

            return requestMessage;
        }

    }
}
