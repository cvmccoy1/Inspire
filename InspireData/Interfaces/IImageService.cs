using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireData
{
    public interface IImageService
    {
        ImageData GetImageData();
    }

    /// <summary>
    /// The Url to an image file.
    /// </summary>
    public interface IImageData
    {
        string Url { get;  }
    }
}
