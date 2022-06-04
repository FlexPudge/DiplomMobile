using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RofloBulumbula.ToolKit
{
    public class HttpRequest
    {
        public static async Task<T> GetAsync<T>(string address)
        {
            var clientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (ao, f, s, a) => true };
            var client = new HttpClient(clientHandler);
            var response = await client.GetAsync(address);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
        public static async Task<List<T>> GetListAsync<T>(string address)
        {
            var clientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (ao, f, s, a) => true };
            var client = new HttpClient(clientHandler);
            var response = await client.GetAsync(address);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<T>>(content);
        }
        public static async Task<T> DeleteAsync<T>(string address)
        {
            var clientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (ao, f, s, a) => true };
            var client = new HttpClient(clientHandler);
            var response = await client.DeleteAsync(address);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
        public static async Task<HttpResponseMessage> PostAsync<T>(string address, object content)
        {
            var clientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (ao, f, s, a) => true };
            var client = new HttpClient(clientHandler);
            var json = JsonConvert.SerializeObject(content);
            var contents = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(address, contents);
            response.EnsureSuccessStatusCode();
            return response.EnsureSuccessStatusCode();
        }
        public static async Task<List<T>> PostAsyncNotCode<T>(string address, object content)
        {
            var clientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (ao, f, s, a) => true };
            var client = new HttpClient(clientHandler);
            var json = JsonConvert.SerializeObject(content);
            var contents = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(address, contents);
            var contentes = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<T>>(contentes);
        }
        public static async Task<T> PutAsync<T>(string address, object content)
        {
            try
            {
                var clientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (ao, f, s, a) => true };
                var client = new HttpClient(clientHandler);
                var json = JsonConvert.SerializeObject(content);
                var contents = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(address, contents);
                var responseContent = await response.Content.ReadAsStringAsync();
                var isSuccess = JsonConvert.DeserializeObject<T>(responseContent);
                return isSuccess;
            }
            catch
            {
                return default(T);
            }
        }
    }
}
