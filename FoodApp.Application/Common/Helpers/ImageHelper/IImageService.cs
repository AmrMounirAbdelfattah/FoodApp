using Microsoft.AspNetCore.Http;

namespace FoodApp.Application.Common.Helpers.ImageHelper
{
    public interface IImageService
    {
        Task<IEnumerable<string>> ConfigureImages(IEnumerable<IFormFile> images);
    }
}
