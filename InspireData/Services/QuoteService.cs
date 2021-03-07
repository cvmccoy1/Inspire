using InspireData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InspireData
{    /// <summary>
     /// Class to provide a service to retrieve Quote data.
     /// The service provides a list of quotes and authors (around 10).
     /// </summary>
    public class QuoteService
    {
        /// <summary>
        /// Method to access the quotesondes website to request a list of quotes/authors.
        /// </summary>
        /// <returns>A list of <see cref="QuoteData"/> objects.</returns>
        public static async Task<List<QuoteData>> GetQouteData()
        {
            string url = $"https://quotesondesign.com/wp-json/wp/v2/posts/?orderby=rand";

            using (HttpResponseMessage response = await ApiHelper.Instance.ApiClient.GetAsync(url))
            {
                List<QuoteData> QuoteData = new List<QuoteData>();
                if (response.IsSuccessStatusCode)
                {
                    QuoteData = await response.Content.ReadAsAsync<List<QuoteData>>();
                }
                return QuoteData;
            }
        }
    }
}
