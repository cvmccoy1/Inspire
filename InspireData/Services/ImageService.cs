using InspireData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InspireData
{
    /// <summary>
    /// Class to provide a service to retrieve Image data.
    /// At present, provides just an random image.
    /// </summary>
    public class ImageService : BaseHttpService<ImageData>
    {
        /// <summary>
        /// Method to access the splashbase website, requesting a random image.
        /// </summary>
        /// <returns>An <see cref="ImageData"/> object.</returns>
        public static async Task<ImageData> GetImageData()
        {
            return await GetDataFromService($"http://www.splashbase.co/api/v1/images/random");
        }
    }
}
