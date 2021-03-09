using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspireData
{    /// <summary>
     /// Class to provide a service to retrieve Quote data.
     /// The service provides a list of quotes and authors (around 10).
     /// </summary>
    public class QuoteService : BaseHttpService<List<QuoteData>>, IQuoteService
    {
        /// <summary>
        /// Method to access the quotesondes website to request a list of quotes/authors.
        /// </summary>
        /// <returns>A list of <see cref="QuoteData"/> objects.</returns>
        public async Task<List<IQuoteData>> GetQouteData()
        {
            List<QuoteData> temp = await GetDataFromService($"https://quotesondesign.com/wp-json/wp/v2/posts/?orderby=rand");
            return temp.Cast<IQuoteData>().ToList();
        }
    }
}
