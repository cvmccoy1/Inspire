using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// Contains a list of available quotes to display
        /// </summary>
        private List<QuoteData> _quoteDataList;

        /// <summary>
        /// Contains the index into the list of quotes of the one currently in use
        /// </summary>
        private int _currentQuoteIndex = 0;

        /// <summary>
        /// Method to access the quotesondes website to request a list of quotes/authors.
        /// </summary>
        /// <returns>A list of <see cref="QuoteData"/> objects.</returns>
        public QuoteData GetQouteData()
        {
            GetNextAvailableQuote();
            return _quoteDataList[_currentQuoteIndex];
        }

        /// <summary>
        /// A list of quotes (about 10) is kept until all the quotes in the list have been displayed.
        /// Once all of the quotes have been used (or at start up), the list is refreshed with a new 
        /// list of quotes, retrieved from the <see cref="QuoteService"/>.
        /// </summary>
        private void GetNextAvailableQuote()
        {
            int quoteCount = _quoteDataList != null ? _quoteDataList.Count : 0;
            if (++_currentQuoteIndex >= quoteCount)
            {
                try
                {
                    _quoteDataList = GetDataFromService("https://quotesondesign.com/wp-json/wp/v2/posts/?orderby=rand");
                }
                catch (Exception exp)
                {
                    Debug.Fail($"Unable to retrieve the quote data list: {exp.Message}");
                }
                finally
                {
                    _currentQuoteIndex = 0;
                }
            }
        }

    }
}
