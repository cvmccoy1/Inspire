using System.Threading.Tasks;

namespace InspireData
{
    /// <summary>
    /// Class to provide a service to retrieve Image data.
    /// At present, provides just an random image.
    /// </summary>
    public class ImageService : BaseHttpService<ImageData>, IImageService
    {
        /// <summary>
        /// Method to access the splashbase website, requesting a random image.
        /// </summary>
        /// <returns>An <see cref="ImageData"/> object.</returns>
        public async Task<IImageData> GetImageData()
        {
            return await GetDataFromService($"http://www.splashbase.co/api/v1/images/random");
        }
    }
}
