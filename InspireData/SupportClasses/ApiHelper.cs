using System.Net.Http;
using System.Net.Http.Headers;

namespace InspireData
{
    /// <summary>
    /// Class that provide a wrapper to the HttpClient object.
    /// This is a singleton class.
    /// </summary>
    internal sealed class ApiHelper
    {
        private static ApiHelper instance = null;
        private static readonly object padlock = new object();

        public HttpClient ApiClient { get; set; }

        private ApiHelper()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static ApiHelper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ApiHelper();
                    }
                    return instance;
                }
            }
        }
    }
}
