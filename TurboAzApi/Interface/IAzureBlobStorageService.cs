using Microsoft.AspNetCore.Http;
using TurboAzApi.DTOs;

namespace TurboAzApi.Interface;

public interface IAzureBlobStorageService
{
    public Task<List<ImageResponse>> UploadImagesAsync(List<string> images, DateTime dateTime);
    public Task<List<ImageResponse>> UploadImagesAsync(List<IFormFile> images, DateTime dateTime);
}
