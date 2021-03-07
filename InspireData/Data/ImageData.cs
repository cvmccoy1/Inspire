using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InspireData
{
    /// <summary>
    /// Class containing all of the Image data
    /// </summary>
    public class ImageData
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
