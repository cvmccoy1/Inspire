using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace InspireData
{
    public abstract class BaseHttpService<T> where T : new()
    {
        protected async Task<T> GetDataFromService(string url)
        {
            T data = new T();
            using (HttpResponseMessage response = await ResponseFromHttpGetRequest(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    data = await ParseImageDataFromHttpResponseMessage(response);
                }
                else
                {
                    Debug.Fail($"The HTTP Get request failed: {response.StatusCode}");
                }
            }
            return data;
        }

        private async Task<HttpResponseMessage> ResponseFromHttpGetRequest(string url)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await ApiHelper.Instance.ApiClient.GetAsync(url);
            }
            catch (Exception exp)
            {
                Debug.Fail($"Unable to get an HTTP response from the image service: {exp.Message}");
            }
            return response;
        }

        private async Task<T> ParseImageDataFromHttpResponseMessage(HttpResponseMessage response) 
        {
            T data = new T();
            try
            {
                data = await response.Content.ReadAsAsync<T>();
            }
            catch (Exception exp)
            {
                Debug.Fail($"Unable to get the Data from the HTTP response: {exp.Message}");
            }
            return data;
        }
    }
}
