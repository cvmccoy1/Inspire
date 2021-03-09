using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireData
{
    public interface IQuoteService
    {
        Task<List<IQuoteData>> GetQouteData();
    }

    public interface IQuoteData
    {
        /// <summary>
        /// The Author's Name of the Quote
        /// </summary>
        string Author { get; }

        /// <summary>
        /// The Quote (in plain text)
        /// </summary>
        string Quote { get; }
    }
}
