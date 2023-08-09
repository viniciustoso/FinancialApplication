using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialApplication.Utility.Utilities
{
    public interface IHttpClientUtility
    {
        Task<string> DeleteAsync(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<string> DeleteAsync(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<T> DeleteAsync<T>(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<T> DeleteAsync<T>(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<string> GetAsync(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<string> GetAsync(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<T> GetAsync<T>(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<T> GetAsync<T>(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<string> PatchAsync(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<T> PatchAsync<T>(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<T> PatchAsync<T>(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<string> PostAsync(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<string> PostAsync(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<T> PostAsync<T>(string requestUri, object contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<T> PostAsync<T>(string requestUri, string contentPost, string authorization = null, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<string> PutAsync(string requestUri, object contentPost, string authorization, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<string> PutAsync(string requestUri, string contentPost, string authorization, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<T> PutAsync<T>(string requestUri, object contentPost, string authorization, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
        Task<T> PutAsync<T>(string requestUri, string contentPost, string authorization, string mediaTypeNames = "application/json", int? timeout = null, Dictionary<string, string> headers = null);
    }
}