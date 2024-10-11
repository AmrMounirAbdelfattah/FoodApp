using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Common.Helpers
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService()
        {
            var account = new Account(
                Environment.GetEnvironmentVariable("CLOUD_NAME"),
                 Environment.GetEnvironmentVariable("API_SECRET"),
                  Environment.GetEnvironmentVariable("API_KEY")
            );

            _cloudinary = new Cloudinary(account);
        }
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                PublicId = Path.GetFileNameWithoutExtension(file.FileName) 
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return  uploadResult.Url.ToString();
        }
    }
}
