using InspireData;
using System;
using System.Collections.Generic;
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
    public class ImageService
    {
        /// <summary>
        /// Method to access the splashbase website, requesting a random image.
        /// </summary>
        /// <returns>An <see cref="ImageData"/> object.</returns>
        public static async Task<ImageData> GetImageData()
        {
            string url = $"http://www.splashbase.co/api/v1/images/random";

            using (HttpResponseMessage response = await ApiHelper.Instance.ApiClient.GetAsync(url))
            {
                ImageData ImageData = new ImageData();
                if (response.IsSuccessStatusCode)
                {
                    ImageData = await response.Content.ReadAsAsync<ImageData>();
                }
                return ImageData;
            }
        }
    }
}
