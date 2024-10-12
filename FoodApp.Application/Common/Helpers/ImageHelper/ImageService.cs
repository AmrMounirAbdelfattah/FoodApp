using FoodApp.Application.Common.Helpers.CloudinaryHelper;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FoodApp.Application.Common.Helpers.ImageHelper
{
    public class ImageService:IImageService
    {
        private readonly ICloudinaryService _cloudinaryService;
        public ImageService(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }
        public async Task<IEnumerable<string>> ConfigureImages(IEnumerable<IFormFile> images)
        {
            var imageUrls = new List<string>();
            if (images != null && images.Count() > 0)
            {
                foreach (var image in images)
                {
                    if (image.Length > 0)
                    {
                        var url = await _cloudinaryService.UploadImageAsync(image);
                        imageUrls.Add(url.Url.ToString());
                    }
                }
                return imageUrls;
            }
            throw new Exception("Invalid Images");
           
        }
    }
}
