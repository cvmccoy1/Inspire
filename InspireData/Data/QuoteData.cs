﻿using Newtonsoft.Json;

namespace InspireData
{
    /// <summary>
    /// Class containing all of the Quote data
    /// </summary>
    public class QuoteData : IQuoteData
    {
        /// <summary>
        /// The Author's Name of the Quote
        /// </summary>
        [JsonIgnore]
        public string Author
        {
            get => FormatConverter.HTMLToText(Title.Rendered);
        }

        /// <summary>
        /// The Quote (in plain text)
        /// </summary>
        [JsonIgnore]
        public string Quote
        {
            get => FormatConverter.HTMLToText(Content.Rendered);
        }


        // Generated by Xamasoft JSON Class Generator and then stripped out unneeded properties.
        // http://www.xamasoft.com/json-class-generator

        internal class InnerTitle
        {

            [JsonProperty("rendered")]
            public string Rendered { get; set; }
        }

        internal class InnerContent
        {

            [JsonProperty("rendered")]
            public string Rendered { get; set; }
        }

        [JsonProperty("title")]
        internal InnerTitle Title { get; set; }

        [JsonProperty("content")]
        internal InnerContent Content { get; set; }
    }
}
